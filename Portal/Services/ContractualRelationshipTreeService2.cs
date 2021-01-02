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
        public List<Job> OrganisationLevelAllocation(List<Job> rawList)
        {
            List<Job> rawJobList = new List<Job>();
            rawJobList.Add(new Job("ProjectBriefCreation", "ProjectOwner", "Owner", null));
            rawJobList.Add(new Job("ProjectManagement", "ContractsManager", "GeneralContractor", "Owner"));
            rawJobList.Add(new Job("DesignManagement", "DesignContractsManager", "DesignContractor", "Owner"));
            rawJobList.Add(new Job("BuildingDesign", "LeadArchitect", "ArchitecturalPractice", "DesignContractor"));
            rawJobList.Add(new Job("StructuralDesign", "StructuralEngineer", "StructuralEngineeringPractice", "DesignContractor"));
            rawJobList.Add(new Job("Drywalling", "Carpenter", "Carpentry", "GeneralContractor"));
            rawJobList.Add(new Job("Plastering", "Plasterer", "PlasteringAndPainting", "GeneralContractor"));
            rawJobList.Add(new Job("Painting", "Painter", "PlasteringAndPainting", "GeneralContractor"));

            List<Job> levelAssignedJobList = new List<Job>();
            foreach (var job in rawJobList)
            {
                var levelAssignedJob = rawJobList.Select(x => new Job(x.JobName, x.JobExecutor, x.OrganisationType, x.ContractingOrganisationType, OrgLevelInt(rawJobList, rawJobList.IndexOf(x))));
            }
            return levelAssignedJobList;
        }

        public int OrgLevelInt(List<Job> rawList, int rawListIndexNumber)
        {
            int index = rawListIndexNumber;
            int organisationLevel = 0;
            List<Job> toBeDiscarded = new List<Job>();
            List<Job> toBeRemoved = new List<Job>();
            Dictionary<Job, int> allocateLevel = new Dictionary<Job, int>();
            foreach (var job in rawList)
            {
                if (job.ContractingOrganisationType == string.Empty && job.OrganisationType != "Owner")
                {
                    toBeDiscarded.Add(job); //job entry validation here
                }
                if (job.OrganisationType == "Owner")
                {
                    allocateLevel.Add(job, organisationLevel += 1);
                    toBeRemoved.Add(job);
                }
            }
            foreach (var job in toBeRemoved)
            {
                rawList.Remove(job);
            }
            toBeRemoved.Clear();
            while (rawList.Count > 0)
            {
                foreach (var job in rawList)
                {
                    if (allocateLevel.Keys.FirstOrDefault(x => x.OrganisationType == job.ContractingOrganisationType) is Job suitableJob)
                    {
                        allocateLevel.Add(job, allocateLevel.FirstOrDefault(x => x.Key.OrganisationType.Equals(job.ContractingOrganisationType)).Value + 1);
                        toBeRemoved.Add(job);
                    }
                }
            }
            return allocateLevel.Values.IndexOf(index);
        }
    }
}
