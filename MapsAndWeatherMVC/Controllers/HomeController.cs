using MapsAndWeatherMVC.Models;
using MapsAndWeatherService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MapsAndWeatherMVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger, ISettingService settingService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            string? key = await settingService.GetSettingValueAsync("GoogleAPIKey");
            ViewBag.Key = key ?? throw new Exception("google api key not found");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
