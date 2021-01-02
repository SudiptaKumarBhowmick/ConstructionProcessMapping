using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Portal.Services;

namespace Portal.Controllers
{
    public class PlottingController : Controller
    {
        private readonly IVisualService _visualService;
        public PlottingController(IVisualService visualService)
        {
            _visualService = visualService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Canvas = (List<EntityNodeConfiguration>) await _visualService.GetNodePlottingGeometry(new Guid("822934B2-5EBE-4B47-843F-2423B20292F8"));
            //ViewBag.Canvas = (StraightConnectionLineViewModel) await _visualService.GetLinePlottingGeometry(new Guid("822934B2-5EBE-4B47-843F-2423B20292F8"));
            return View();
        }
    }
}