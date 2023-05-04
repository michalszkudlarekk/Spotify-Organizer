using Microsoft.AspNetCore.Mvc;
using SpotifyOrganizer.Models;
using System.Diagnostics;

namespace SpotifyOrganizer.Controllers
{
    /// <summary>
    /// Class <c>HomeController</c> is responsible for handling requests related to
    /// the application's home page and privacy policy. It includes three action methods: Index, Privacy, and Error. 
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}