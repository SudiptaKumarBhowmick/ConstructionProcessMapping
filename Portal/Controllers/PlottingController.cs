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
            ViewBag.Canvas = (List<EntityNodeConfiguration>)_visualService.GetNodePlottingGeometry(null);
            return View();
        }
        //Allocate a proportionate slot for the job according to how many steps, inputs and outputs it has, the organisation slot then wraps around all the job executors and jobs in its domain. If an organistation contains no jobs, presume a simgle job executor and a job with a single input, a single output and 6 job steps (MAKE THESE NODES INVISIBLE)         
    }
}