using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using OOP_Lab_6.Models;
using OOP_Lab_6.Domain.Extensions;
using OOP_Lab_6.Services.Downloaders;
using System.Threading.Tasks;

namespace OOP_Lab_6.Services.StatsParser
{
    public class LeagueOfGraphsStatsParser : IStatsParser
    {
        private readonly IHtmlDownloader _downloader;

        public LeagueOfGraphsStatsParser(IHtmlDownloader downloader) => _downloader = downloader;

        public IEnumerable<ChampionStats> GetChampionStats()
        {
            var doc = new HtmlDocument();
            var htmlResult = _downloader.GetHtml(new Uri("https://www.leagueofgraphs.com/champions/builds"));
            doc.LoadHtml(htmlResult);
            var trNodes = doc.GetElementbyId("mainContent").ChildNodes[1].ChildNodes[1].ChildNodes[1]
                .ChildNodes[1].ChildNodes.Where(x => x.Name == "tr");

            var stats =
               from trNode in trNodes
               select trNode.ChildNodes.Where(x => x.Name == "td").ToArray()
               into tdNodes
               where tdNodes.Length >= 3
               let name = tdNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[1].InnerText.Trim()
               let pickRate = float.Parse(tdNodes[2].InnerHtml.Trim().
                Substring(25, 6).CleanOfNonDigits(), new CultureInfo("en-US"))
               let winRate = float.Parse(tdNodes[3].InnerHtml.Trim().
                Substring(25, 6).CleanOfNonDigits(), new CultureInfo("en-US"))
               let banRate = float.Parse(tdNodes[4].InnerHtml.Trim().
                Substring(25, 6).CleanOfNonDigits(), new CultureInfo("en-US"))
               select new ChampionStats
               {
                   Name = name,
                   PickRate = pickRate,
                   WinRate = winRate,
                   BanRate = banRate
               };
            return stats.ToList();
        }

        public async Task<IEnumerable<ChampionStats>> GetChampionStatsAsync()
        {
            var doc = new HtmlDocument();
            var htmlResult = await _downloader.GetHtmlAsync(new Uri("https://www.leagueofgraphs.com/champions/builds"));
            doc.LoadHtml(htmlResult);
            var trNodes = doc.GetElementbyId("mainContent").ChildNodes[1].ChildNodes[1].ChildNodes[1]
                .ChildNodes[1].ChildNodes.Where(x => x.Name == "tr");

            var stats =
               from trNode in trNodes
               select trNode.ChildNodes.Where(x => x.Name == "td").ToArray()
               into tdNodes
               where tdNodes.Length >= 3
               let name = tdNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[1].InnerText.Trim()
               let pickRate = float.Parse(tdNodes[2].InnerHtml.Trim().
                Substring(25, 6).CleanOfNonDigits(), new CultureInfo("en-US"))
               let winRate = float.Parse(tdNodes[3].InnerHtml.Trim().
                Substring(25, 6).CleanOfNonDigits(), new CultureInfo("en-US"))
               let banRate = float.Parse(tdNodes[4].InnerHtml.Trim().
                Substring(25, 6).CleanOfNonDigits(), new CultureInfo("en-US"))
               select new ChampionStats
               {
                   Name = name,
                   PickRate = pickRate,
                   WinRate = winRate,
                   BanRate = banRate
               };
            return stats.ToList();
        }
    }
}
