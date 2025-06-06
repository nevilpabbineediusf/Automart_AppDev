using System.Diagnostics;
using Automart.Models;
using Microsoft.AspNetCore.Mvc;

namespace Automart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() => View();
        public IActionResult Privacy() => View();
        public IActionResult Read() => View();
        public IActionResult Create() => View();
        public IActionResult Update() => View();
        public IActionResult Delete() => View();
        public IActionResult About() => View();
        public IActionResult Data() => View();
        public IActionResult SignIn() => View();
        public IActionResult SignUp() => View();
        public IActionResult UserHome() => View();
        public IActionResult viewcars() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
