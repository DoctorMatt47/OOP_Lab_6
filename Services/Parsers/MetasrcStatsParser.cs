using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using OOP_Lab_6.Models;
using OOP_Lab_6.Domain.Extensions;
using OOP_Lab_6.Services.Downloaders;
using System.Threading.Tasks;

namespace OOP_Lab_6.Services.Parsers
{
    public class MetasrcStatsParser : IStatsParser
    {
        private readonly IHtmlDownloader _downloader;

        public MetasrcStatsParser(IHtmlDownloader downloader) => _downloader = downloader;

        public async Task<IEnumerable<ChampionStats>> GetChampionStatsAsync()
        {
            var doc = new HtmlDocument();
            var htmlResult = await _downloader.GetHtmlAsync(new Uri("https://www.metasrc.com/5v5/stats"));
            doc.LoadHtml(htmlResult);
            var table = doc.GetElementbyId("content").
                ChildNodes[0].ChildNodes[1].ChildNodes[0].ChildNodes[1];
            var list = new List<ChampionStats>();
            foreach (var record in table.ChildNodes)
            {
                if (record.HasClass("static")) continue;
                var name = record.ChildNodes[0].ChildNodes[0].
                    InnerText;
                var winRate = record.ChildNodes[5].
                    InnerText.CleanOfNonDigits();
                var pickRate = record.ChildNodes[7].
                    InnerText.CleanOfNonDigits();
                var banRate = record.ChildNodes[8].
                    InnerText.CleanOfNonDigits();
                var found = list.Find(x => x.Name == name);
                if (found is null)
                {
                    list.Add(new ChampionStats
                    {
                        Name = name,
                        WinRate = float.Parse(winRate, new CultureInfo("en-US")) / 100,
                        PickRate = float.Parse(pickRate, new CultureInfo("en-US")) / 100,
                        BanRate = float.Parse(banRate, new CultureInfo("en-US")) / 100
                    });
                }
                else
                {
                    var winRateFloat = float.Parse(winRate, new CultureInfo("en-US")) / 100;
                    var pickRateFloat = float.Parse(pickRate, new CultureInfo("en-US")) / 100;
                    var ratio = found.PickRate / pickRateFloat;
                    found.WinRate = (float)Math.Round((ratio * found.WinRate + winRateFloat) / (1 + ratio), 3);
                }
            }
            return list;
        }
    }
}
