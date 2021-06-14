using Microsoft.AspNetCore.Mvc;
using OOP_Lab_6.Domain.Mappers;
using OOP_Lab_6.Models;
using OOP_Lab_6.Services.Parsers;
using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;

namespace OOP_Lab_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEnumerable<IStatsParser> _parsers;

        public HomeController(IEnumerable<IStatsParser> parsers) => _parsers = parsers;

        private static IEnumerable<ChampionStats> MergeTables(IEnumerable<IEnumerable<ChampionStats>> tables)
        {
            var lists = tables as List<List<ChampionStats>>;
            var newTable = new List<ChampionStats>();
            foreach (var item in lists[0])
            {
                var list = new List<ChampionStats>();
                for (int i = 1; i < lists.Count; i++)
                {
                    list.Add(lists[i].FirstOrDefault(x => 
                        ChampionNameMapper.GetImageName(x.Name) ==
                        ChampionNameMapper.GetImageName(item.Name)));
                }
                var winRate = item.WinRate;
                var pickRate = item.PickRate;
                var banRate = item.BanRate;
                foreach (var champion in list)
                {
                    winRate += champion.WinRate;
                    pickRate += champion.PickRate;
                    banRate += champion.BanRate;
                }
                newTable.Add(new ChampionStats
                {
                    Name = item.Name,
                    WinRate = (float)Math.Round(winRate / lists.Count, 4),
                    PickRate = (float)Math.Round(pickRate / lists.Count, 4),
                    BanRate = (float)Math.Round(banRate / lists.Count, 4)
                });
            }
            return newTable;
        }

        [Route("Home/StatsParallel")]
        public IActionResult StatsParallel()
        {
            var sw = new Stopwatch();
            sw.Start();
            var parsers = new List<IStatsParser>(_parsers);
            var tables = new List<List<ChampionStats>>();
            Parallel.For(0, parsers.Count, (i, loopState) =>
            {
                tables.Add(parsers[i].GetChampionStatsAsync().Result as List<ChampionStats>);
            });
            dynamic model = new ExpandoObject();
            model.Table = MergeTables(tables);
            sw.Stop();
            model.Time = sw.ElapsedMilliseconds;
            return View("Index", model);
        }

        [Route("Home/StatsAsync")]
        public async Task<IActionResult> StatsAsync()
        {
            var sw = new Stopwatch();
            sw.Start();
            var tables = new List<List<ChampionStats>>();
            foreach (var parser in _parsers)
            {
                tables.Add(await parser.GetChampionStatsAsync() as List<ChampionStats>);
            }
            dynamic model = new ExpandoObject();
            model.Table = MergeTables(tables);
            sw.Stop();
            model.Time = sw.ElapsedMilliseconds;
            return View("Index", model);
        }

        [Route("Home/Stats")]
        public IActionResult Stats()
        {
            var sw = new Stopwatch();
            sw.Start();
            var tables = new List<List<ChampionStats>>();
            foreach (var parser in _parsers)
            {
                tables.Add(parser.GetChampionStatsAsync().Result as List<ChampionStats>);
            }
            dynamic model = new ExpandoObject();
            model.Table = MergeTables(tables);
            sw.Stop();
            model.Time = sw.ElapsedMilliseconds;
            return View("Index", model);
        }

        public IActionResult Index()
        {
            return Redirect("~/Home/StatsParallel");
        }
    }
}
