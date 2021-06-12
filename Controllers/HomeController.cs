using Microsoft.AspNetCore.Mvc;

namespace OOP_Lab_6.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
