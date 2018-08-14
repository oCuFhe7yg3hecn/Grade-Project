using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GradeProject.MVCWeb.Models;
using GradeProject.MVCWeb.Infrastruct;

namespace GradeProject.MVCWeb.Controllers
{
    public class HomeController : Controller
    {
        private WebServicesCallService _webService;

        public HomeController(WebServicesCallService webService)
        {
            _webService = webService;
        }

        public async Task <IActionResult> Index()
        {
            var games = await _webService.GetTopGames(10);
            return View(games);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
