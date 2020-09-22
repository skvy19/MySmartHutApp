using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHut.Mvc.Models;
using SmartHut.Mvc.Models.Services;
using SmartHut.Mvc.ViewModels;

namespace SmartHut.Mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISmartHutService _smartHutService;

        public HomeController(ISmartHutService smartHutService)
        {
            _smartHutService = smartHutService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/rooms")]
        public async Task<IActionResult> Privacy()
        {
            RoomsViewModel roomsViewModel = new RoomsViewModel
            {
                Building = await _smartHutService.GetBuilding(),
                Devices = await _smartHutService.GetDevices(),
                Units = await _smartHutService.GetUnits()
            };

            return View(roomsViewModel);
        }

    }
}
