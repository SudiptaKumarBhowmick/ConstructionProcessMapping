using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services
{
    public interface IJobDataService
    {
        Task AddJobData(List<StructuringInformationModel> jobData);
        Task AddJobData(byte[] jobData);
        Task<List<StructuringInformationModel>> GetJobDataByGUID(Guid guid);
        Task<List<Job>> GetJobDataListByGUID(Guid guid);
    }
}
