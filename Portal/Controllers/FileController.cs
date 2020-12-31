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
        //private readonly IProductRelationshipNetworkService _productRelationshipNetworkService;
        public FileController(ApplicationDbContext context, IContractualRelationshipTreeService contractualRelationshipTreeService/*, IProductRelationshipNetworkService productRelationshipNetworkService*/)
        {
            _context = context;
            _contractualRelationshipTreeService = contractualRelationshipTreeService;
            //_productRelationshipNetworkService = productRelationshipNetworkService;
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

                    //_context.Add<FileModel>(filemodel);

                    var jobdata = ProcessFile(bytearray);

                    _context.Jobdata.AddRange(jobdata);

                    _context.SaveChanges();
                    //var results = _contractualRelationshipTreeService.DefineContractualRelationshipHierarchy(FromOrganisationStructuringInformationModelToJob(jobdata));
                }
            }
            return RedirectToAction("Index", "Home");
        }

        private List<StructuringInformationModel> ProcessFile(byte[] filestream)  //changed this from a list to a single 'Job' now - we now need to add this to a list of jobs somewhere
        {
            List<CustomInputModel> inputList = new List<CustomInputModel>();
            List<CustomOutputModel> outputList = new List<CustomOutputModel>();
            List<OrderedStepNameAndStepNumber> jobStepKeysAndValues = new List<OrderedStepNameAndStepNumber>();
            List<OrderedGenericInputNameAndStepNumber> genericInputKeysAndValues = new List<OrderedGenericInputNameAndStepNumber>();
            var strinfo = new List<StructuringInformationModel>();
            var uniqueRecordId = Guid.NewGuid();
            var streamReader = new StreamReader(new MemoryStream(filestream), System.Text.Encoding.UTF8, true );
            DateTime creationDate = DateTime.Now;

            string line;
            int linecounter = 0;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (linecounter > 1) //could we make this an && in the while above?
                {
                    var job = new StructuringInformationModel();
                    var columns = line.Split(",");
                    job.JobName = NameFinder(columns[1]);
                    job.JobExecutor = NameFinder(columns[2]);
                    job.OrganisationType = NameFinder(columns[3]);
                    job.ContractingOrganisationType = NameFinder(columns[4]);
                    job.OrderedStepNameAndStepNumber = StringListEntryAdder<OrderedStepNameAndStepNumber>(jobStepKeysAndValues, columns[0], "Job Step", new OrderedStepNameAndStepNumber { key = columns[6], value = columns[5] != string.Empty ? Convert.ToInt32(columns[5]) : 0 });
                    jobStepKeysAndValues = job.OrderedStepNameAndStepNumber;
                    job.CustomInputModels = StringListEntryAdder<CustomInputModel>(inputList, columns[0], "Custom Input", new CustomInputModel { InputName = columns[6] });
                    inputList = job.CustomInputModels;
                    job.CustomOutputModels = StringListEntryAdder<CustomOutputModel>(outputList, columns[0], "Custom Output", new CustomOutputModel { OutputName = columns[6] });
                    outputList = job.CustomOutputModels;
                    job.OrderedGenericInputNameAndStepNumber = StringListEntryAdder<OrderedGenericInputNameAndStepNumber>(genericInputKeysAndValues, columns[0], "Generic Input", new OrderedGenericInputNameAndStepNumber { key = columns[6], value = columns[5] != string.Empty ? Convert.ToInt32(columns[5]) : 0 });
                    genericInputKeysAndValues = job.OrderedGenericInputNameAndStepNumber;
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
            var rawJob = organisationStructuringInformationModelList.Select(x => new Job(x.JobName, x.JobExecutor, x.OrganisationType, x.ContractingOrganisationType, x.OrderedStepNameAndStepNumber, x.CustomInputModels, x.CustomOutputModels, x.OrderedGenericInputNameAndStepNumber)).ToList();
            return rawJob;
        }
        //private List<Job> FromProductStructuringInformationModelToJob(List<StructuringInformationModel> productStructuringInformationModelList)
        //{
        //    return productStructuringInformationModelList.Select(x => new Job(x.StepNumber, x.CustomOutput, x.MasterName, x.JobName)).ToList();
        //}    //something is not right with this, this should be using the constructor format
        private string NameFinder(string findTrue) => findTrue != "" ? findTrue : "";
        //{
        //    string jobNameFinder = "";
        //    if (findTrue != "")
        //    {
        //        jobNameFinder += findTrue;
        //    }
        //    return jobNameFinder;
        //}
        private List<T> StringListEntryAdder<T>(List<T> names, string master, string requiredShape, T current) where T : class, new()
        {
            if (master == requiredShape)
            {
                names.Add(current);
            }
            return names;
        }
    }
}