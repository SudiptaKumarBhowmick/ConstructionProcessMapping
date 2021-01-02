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
        public Dictionary<Job, int> OrganisationLevelAllocation(/*List<Job> rawJobList*/)
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

            int organisationLevel = 0;
            Dictionary<Job, int> allocateLevel = new Dictionary<Job, int>();
            Console.WriteLine(allocateLevel);
            foreach (var job in rawJobList)
            {
                if (job.ContractingOrganisationType == string.Empty && job.OrganisationType != "Owner")
                {
                    rawJobList.Remove(job); //job entry validation here
                }
                else
                {
                    allocateLevel.Add(job, organisationLevel += 1);
                    rawJobList.Remove(job);
                }
            }
            while (rawJobList.Count > 0)
            {
                foreach (var job in rawJobList)
                {
                    if (allocateLevel.Keys.FirstOrDefault(x => x.ContractingOrganisationType.Equals(job.OrganisationType)) is Job suitableJob)
                    {
                        allocateLevel.Add(job, allocateLevel.FirstOrDefault(x => x.Key.ContractingOrganisationType.Equals(job.OrganisationType)).Value + 1);
                        rawJobList.Remove(job);
                    }
                }
            }
            return allocateLevel;
        }
    }
}
