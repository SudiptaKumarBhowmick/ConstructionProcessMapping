using Microsoft.EntityFrameworkCore.Internal;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class ContractualRelationshipTreeService2
    {
        public Dictionary<Job, int> OrganisationLevelAllocation(List<Job> rawJobList)
        {
            int organisationLevel = 0;
            Dictionary<Job, int> allocateLevel = new Dictionary<Job, int>();
            foreach (var job in rawJobList)
            {
                if (job.ContractingOrganisationType == string.Empty && job.ContractingOrganisationType != "Owner")
                {
                    rawJobList.Remove(job); //job validation here
                }
                else
                {
                    allocateLevel.Add(job, organisationLevel += 1);
                    rawJobList.Remove(job);
                }
            }
            foreach (var job in rawJobList)
            {
                if (allocateLevel.Keys.FirstOrDefault(x => x.ContractingOrganisationType.Equals(job.OrganisationType)) is Job suitableJob)
                {
                    allocateLevel.Add(job, allocateLevel.Values.FirstOrDefault(x => x.ContractingOrganisationType.Equals(job.OrganisationType)) + 1);
                }
            }

            return allocateLevel;
        }
    }
}
