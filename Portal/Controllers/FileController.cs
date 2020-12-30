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
        private readonly IContractualRelationshipTreeService _contractualRelationshipTreeService;
        private readonly IProductRelationshipNetworkService _productRelationshipNetworkService;
        public FileController(ApplicationDbContext context, IContractualRelationshipTreeService contractualRelationshipTreeService, IProductRelationshipNetworkService productRelationshipNetworkService)
        {
            _context = context;
            _contractualRelationshipTreeService = contractualRelationshipTreeService;
            _productRelationshipNetworkService = productRelationshipNetworkService;
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
                    var results = _contractualRelationshipTreeService.DefineContractualRelationshipHierarchy(FromOrganisationStructuringInformationModelToJob(jobdata));
                }
            }
            return RedirectToAction("Index", "Home");
        }

        private List<StructuringInformationModel> ProcessFile(byte[] filestream)
        {
            List<string> masterNameList = new List<string>();
            List<int> jobStepNumberList = new List<int>();
            List<string> jobStepDescriptionList = new List<string>();
            var strinfo = new List<StructuringInformationModel>();
            var uniqueRecordId = Guid.NewGuid();
            var streamReader = new StreamReader(new MemoryStream(filestream), System.Text.Encoding.UTF8, true );
            DateTime creationDate = DateTime.Now;

            string line;
            int linecounter = 0;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (linecounter > 1)
                {
                    var job = new StructuringInformationModel();
                    var columns = line.Split(",");
                    job.MasterName = columns[0];
                    job.JobName = columns[1];
                    job.JobExecutor = columns[2];
                    job.OrganisationType = columns[3];
                    job.ContractingOrganisationType = columns[4];
                    //job.StepNumber = columns[5] != string.Empty ? Convert.ToInt32(columns[5]) : 0);
                    //job.StepName = columns[6];
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
        private List<Job> FromOrganisationStructuringInformationModelToJob(List<StructuringInformationModel> organisationStructuringInformationModelList)
        {
            var rawJob = organisationStructuringInformationModelList.Select(x => new Job (x.JobExecutor, x.ContractingOrganisationType, x.OrganisationType, x.JobName)).ToList();
            return rawJob;
        }
        //private List<Job> FromProductStructuringInformationModelToJob(List<StructuringInformationModel> productStructuringInformationModelList)
        //{
        //    return productStructuringInformationModelList.Select(x => new Job(x.StepNumber, x.CustomOutput, x.MasterName, x.JobName)).ToList();
        //}    //something is not right with this, this should be using the constructor format

        private string[] MasterNameFinder(string[] findTrue)
        {
            if (findTrue != null)
            {
                return findTrue;
            }
            else { return null; }
        }
    }
}