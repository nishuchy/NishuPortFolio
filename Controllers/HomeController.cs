using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NishuPortFolio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NishuPortFolio.Controllers
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
            ViewData["ActiveMenu"] = "Home";
           
            return View();
        }
        public IActionResult ProjectDetails()
        {
            ViewData["ActiveMenu"] = "Projects";

            return View();

        }
        public IActionResult Contact()
        {
            ViewData["ActiveMenu"] = "Contact";
            return View();

        }
        
        public IActionResult Research()
        {
            ViewData["ActiveMenu"] = "Research";
            return View();
        }
        public IActionResult Projects()
        {
            ViewData["ActiveMenu"] = "Projects";
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
