using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyCV_App.Models;

namespace MyCV_App.Controllers
{
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

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Education()
        {
            return View();
        }

        public IActionResult Experience()
        {
            return View();
        }

        public IActionResult Skills()
        {
            return View();
        }

        public IActionResult TaxCalculator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TaxCalculator(double income)
        {
            double tax = 0;

            if (income <= 300000)
                tax = income * 0.07;
            else if (income <= 600000)
                tax = income * 0.11;
            else
                tax = income * 0.15;

            ViewBag.Tax = tax;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
