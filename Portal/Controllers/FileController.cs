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
                    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    var results = _contractualRelationshipTreeService.DefineContractualRelationshipHierarchy(FromOrganisationStructuringInformationModelToJob(jobdata));
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
                if (linecounter > 1)
                {
                    var job = new StructuringInformationModel();
                    var columns = line.Split(",");
                    job.JobName = columns[0];
                    job.JobExecutor = columns[1];
                    job.OrganisationType = columns[2];
                    job.ContractingOrganisationType = columns[3];
                    job.CustomInput = columns[4];
                    job.StepNumber = columns[5] != string.Empty ? Convert.ToInt32(columns[5]): 0;
                    job.StepName = columns[6];
                    job.GenericInputType = columns[7];
                    job.GenericInputDescription = columns[8];
                    job.CustomOutput = columns[9];
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
            return organisationStructuringInformationModelList.Select(x => new Job (x.OrganisationType, x.ContractingOrganisationType, x.JobName, x.JobExecutor)).ToList();
        }
        private List<Job> FromProductStructuringInformationModelToJob(List<StructuringInformationModel> productStructuringInformationModelList)
        {
            return productStructuringInformationModelList.Select(x => new Job(x.CustomInput, x.CustomOutput, x.JobName, x.JobExecutor)).ToList();
        }    //something is really not right with this, this should be using the constructor format
    }
}