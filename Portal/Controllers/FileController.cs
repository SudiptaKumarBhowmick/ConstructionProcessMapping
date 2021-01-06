using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Portal.Data;
using Portal.Models;
using Portal.Services;

namespace Portal.Controllers
{
    public class FileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IJobDataService _jobDataService;
        private readonly IFileService _fileService;
        private readonly IContractualRelationshipTreeService _contractualRelationshipTreeService;
        public FileController(ApplicationDbContext context, IJobDataService jobDataService, IFileService fileService, IContractualRelationshipTreeService contractualRelationshipTreeService)
        {
            _context = context;
            _jobDataService = jobDataService;
            _fileService = fileService;
            _contractualRelationshipTreeService = contractualRelationshipTreeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFile(IFormFile csvfile)
        {
            var bytearray = _fileService.AddFile(csvfile);
            _jobDataService.AddJobData(bytearray);
            return RedirectToAction("Index", "Home");
        }
    }
}