using Microsoft.EntityFrameworkCore;
using Portal.Data;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class JobDataService : IJobDataService
    {
        private ApplicationDbContext _context;
        public JobDataService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddJobData(List<StructuringInformationModel> jobData)
        {
            _context.Jobdata.AddRange(jobData);
            var result = _context.SaveChangesAsync().Result;
        }
        public async Task<List<StructuringInformationModel>> GetJobDataByGUID(Guid guid)
        {
            var result = await _context.Jobdata
                .Include(y => y.OrderedGenericInputNameAndStepNumber)
                .Include(y => y.OrderedStepNameAndStepNumber)
                .Include(y => y.CustomInputModels)
                .Include(y => y.CustomOutputModels)
                .Where(x => x.Record == guid)
                .ToListAsync();
            return result;
        }
        public async Task<List<Job>> GetJobDataListByGUID(Guid guid)
        {
            var result = await GetJobDataByGUID(guid);
            return FromOrganisationStructuringInformationModelToJob(result);
        }
        public async Task AddJobData(byte[] jobData)
        {
            var result = ProcessFile(jobData);
            await AddJobData(result);
        }
        private List<StructuringInformationModel> ProcessFile(byte[] filestream)  //changed this from a list to a single 'Job' now - we now need to add this to a list of jobs somewhere
        {
            List<CustomInputModel> inputList = new List<CustomInputModel>();
            List<CustomOutputModel> outputList = new List<CustomOutputModel>();
            List<OrderedStepNameAndStepNumber> jobStepKeysAndValues = new List<OrderedStepNameAndStepNumber>();
            List<OrderedGenericInputNameAndStepNumber> genericInputKeysAndValues = new List<OrderedGenericInputNameAndStepNumber>();
            var strinfo = new List<StructuringInformationModel>();
            var uniqueRecordId = Guid.NewGuid();
            var streamReader = new StreamReader(new MemoryStream(filestream), System.Text.Encoding.UTF8, true);
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
        public List<Job> FromOrganisationStructuringInformationModelToJob(List<StructuringInformationModel> organisationStructuringInformationModelList)
        {
            var rawJob = organisationStructuringInformationModelList.Select(x => new Job(x.JobName, x.JobExecutor, x.OrganisationType, x.ContractingOrganisationType, x.OrderedStepNameAndStepNumber, x.CustomInputModels, x.CustomOutputModels, x.OrderedGenericInputNameAndStepNumber)).ToList();
            return rawJob;
        }
        //private List<Job> FromProductStructuringInformationModelToJob(List<StructuringInformationModel> productStructuringInformationModelList)
        //{
        //    return productStructuringInformationModelList.Select(x => new Job(x.StepNumber, x.CustomOutput, x.MasterName, x.JobName)).ToList();
        //}    //something is not right with this, this should be using the constructor format
        private string NameFinder(string findTrue) => findTrue != "" ? findTrue : ""; //not certain this is correct
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
