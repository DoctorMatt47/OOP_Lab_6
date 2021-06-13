using Microsoft.AspNetCore.Mvc;
using OOP_Lab_6.Services.StatsParser;
using System.Threading.Tasks;

namespace OOP_Lab_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatsParser _parser;

        public HomeController(IStatsParser parser) => _parser = parser;

        [Route("Home/StatsParallel")]
        public async Task<IActionResult> StatsParallel()
        {
            dynamic model = await _parser.GetChampionStatsAsync();
            return View("Index", model);
        }

        [Route("Home/StatsAsync")]
        public async Task<IActionResult> StatsAsync()
        {
            dynamic model = await _parser.GetChampionStatsAsync();
            return View("Index", model);
        }

        [Route("Home/Stats")]
        public IActionResult Stats()
        {
            dynamic model = _parser.GetChampionStats();
            return View("Index", model);
        }

        public IActionResult Index()
        {
            return Redirect("~/Home/StatsAsync");
        }
    }
}
