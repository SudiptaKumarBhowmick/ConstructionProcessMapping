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

namespace Portal.Controllers
{
    public class FileController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FileController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFile(IFormFile csvfile)
        {
            FileModel filemodel = new FileModel();

            
            if (csvfile != null)
            {
                //Set Key Name
                string csvname = Guid.NewGuid().ToString() + Path.GetExtension(csvfile.FileName);
              
                using (var ms = new MemoryStream())
                {

                    //TODO UPLOAD INTO DATABASE
                    csvfile.CopyTo(ms);

                    
                    var bytearray = ms.ToArray();

                 

                    filemodel.Extension = csvfile.FileName.Split(".")[1];
                    filemodel.Data = bytearray;
                    filemodel.FileName = csvfile.FileName;
                    filemodel.Length = csvfile.Length;
                    filemodel.CreationDate = DateTime.Now;
                    filemodel.ModificationDate = DateTime.Now;
                    filemodel.CreateBy = "System";
                    filemodel.ModifyBy = "System";

                    _context.Add<FileModel>(filemodel);

                    var jobdata = ProcessFile(bytearray);

                    _context.Jobdata.AddRange(jobdata);

                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        private List<StructuringInformationModel> ProcessFile(byte[] filestream)
        {
            var strinfo = new List<StructuringInformationModel>();
            var uniqueRecordId = Guid.NewGuid();
            var streamReader = new StreamReader(new MemoryStream(filestream), System.Text.Encoding.UTF8, true );
            DateTime creationDate = DateTime.Now;

            string line;
            int linecounter = 0;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (linecounter != 0)
                {
                    var job = new StructuringInformationModel();
                    var columns = line.Split(",");
                    job.JobName = columns[0];
                    job.StepNumber = Convert.ToInt32(columns[1]);
                    job.JobExecutor = columns[2];
                    job.StepName = columns[3];
                    job.Input = columns[4];
                    job.PrimaryPrecedingJob = columns[5];
                    job.PrimarySubsequentJob = columns[6];
                    job.SecondaryPrecedingJob1 = columns[7];
                    job.SecondaryPrecedingJob2 = columns[8];
                    job.SecondarySubsequentJob1 = columns[9];
                    job.SecondarySubsequentJob2 = columns[10];
                    job.Record = uniqueRecordId;
                    job.CreationDate = creationDate;
                    job.ModificationDate = creationDate;
                    job.CreateBy = "System";
                    job.ModifyBy = "System";
                    strinfo.Add(job);
                }
                linecounter++;
            }

            return strinfo;
        }
    }
}