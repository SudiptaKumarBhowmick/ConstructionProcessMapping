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
        public IActionResult Index()
        {
            ViewBag.Canvas = (List<EntityNodeConfiguration>)_visualService.GetNodePlottingGeometry(null) /*+ (List <StraightConnectorLineConfiguration>_visualService.GetNodePlottingGeometry(null)*/;
            return View();
        }
    }
}