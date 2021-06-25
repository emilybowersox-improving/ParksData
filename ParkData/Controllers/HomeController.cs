using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ParkData.Models;
using ParkData.ViewModels;

namespace ParkData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _cache;
        private ParkAPI _parkAPI;

        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache, ParkAPI parkAPI)
        {
            _logger = logger;
            _cache = memoryCache;
            _parkAPI = parkAPI;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllParks()
        {
          /*  var apiData = new ParkAPI(_cache);*/
/*            var myData = await apiData.GetParks();*/

            var vm = new ParkViewModel
            {
                Parks = _parkAPI.CheckCache()
            };

            return View(vm);
        }


/*        public async Task<IActionResult> AllParks()
        {
            var apiData = new ParkAPI();
            *//*            var myData = await apiData.GetParks();*//*

            var vm = new ParkViewModel
            {
                Parks = await apiData.GetParks()
            };

            return View(vm);
        }
*/

        public async Task<IActionResult> ParkSearch(string search)
        {
           /* var apiData = new ParkAPI();*/
            /*       var myData = await apiData.GetParks();*/

            var vm = new ParkViewModel
            {
                Parks = await _parkAPI.GetParksWhere(search)
            };

            return View(vm);
        }

        /*      public async Task<IAsyncResult> ParkInfo(string search)
      {
          var data = await_parkDataClient.Get(search);

          return Json(data);
      }
*/

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
