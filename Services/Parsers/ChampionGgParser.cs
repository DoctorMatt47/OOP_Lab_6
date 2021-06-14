using HtmlAgilityPack;
using OOP_Lab_6.Domain.Extensions;
using OOP_Lab_6.Models;
using OOP_Lab_6.Services.Downloaders;
using OOP_Lab_6.Services.StatsParser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Lab_6.Services.Parsers
{
    public class ChampionGgStatsParser : IStatsParser
    {
        private readonly IHtmlDownloader _downloader;

        public ChampionGgStatsParser(IHtmlDownloader downloader) => _downloader = downloader;

        public async Task<IEnumerable<ChampionStats>> GetChampionStatsAsync()
        {
            var doc = new HtmlDocument();
            var htmlResult = await _downloader.GetHtmlAsync(new Uri("https://champion.gg/statistics/"));
            doc.LoadHtml(htmlResult);
            var table = doc.DocumentNode.Descendants().
                Where(x => x.HasClass("table-container"))
                .First().ChildNodes[0].ChildNodes[0];
            var list = new List<ChampionStats>();
            foreach (var champ in table.ChildNodes)
            {
                var name = champ.Descendants().
                    Where(x => x.HasClass("champion-name")).First()
                    .InnerText.ParseHtmlCodes();
                var winRate = champ.Descendants().
                    Where(x => x.HasClass("champion-win-rate")).First().
                    InnerText.CleanOfNonDigits();
                var banRate = champ.Descendants().
                    Where(x => x.HasClass("champion-ban-rate")).First().
                    InnerText.CleanOfNonDigits();
                var pickRate = champ.Descendants().
                    Where(x => x.HasClass("champion-pick-rate")).First().
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
