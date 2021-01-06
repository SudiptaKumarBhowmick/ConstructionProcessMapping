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
        public List<Job> JobListWithOrganisationLevelAllocation(List<Job> rawList)
        {    //inputs for rawJobList.ElementAt(0)
            List<string> in1 = new List<string>(); in1.Add("a"); in1.Add("b");
            List<string> out1 = new List<string>(); out1.Add("c"); out1.Add("d");

            List<string> in2 = new List<string>(); in2.Add("c"); in2.Add("e");
            List<string> out2 = new List<string>(); out2.Add("g");

            List<string> in3 = new List<string>(); in3.Add("c"); in3.Add("d");
            List<string> out3 = new List<string>(); out3.Add("e"); out3.Add("f");

            List<string> in4 = new List<string>(); in4.Add("e"); in4.Add("f");
            List<string> out4 = new List<string>(); out4.Add("l"); out4.Add("t");

            List<string> in5 = new List<string>(); in5.Add("e"); in5.Add("t");
            List<string> out5 = new List<string>(); out5.Add("m"); out5.Add("u");

            List<string> in6 = new List<string>(); in6.Add("e"); in6.Add("u");
            List<string> out6 = new List<string>(); out6.Add("n"); out6.Add("v");

            List<string> in7 = new List<string>(); in7.Add("e"); in7.Add("v");
            List<string> out7 = new List<string>(); out7.Add("o"); out7.Add("w");

            List<string> in8 = new List<string>(); in8.Add("e"); in8.Add("w");
            List<string> out8 = new List<string>(); out8.Add("p"); out8.Add("x");


            List<Job> rawJobList = new List<Job>();
            rawJobList.Add(new Job("ProjectBriefCreation", "ProjectOwner", "Owner", null, in1, out1));
            rawJobList.Add(new Job("ProjectManagement", "ContractsManager", "GeneralContractor", "Owner", in2, out2));
            rawJobList.Add(new Job("DesignManagement", "DesignContractsManager", "DesignContractor", "Owner", in3, out3));
            rawJobList.Add(new Job("BuildingDesign", "LeadArchitect", "ArchitecturalPractice", "DesignContractor", in4, out4));
            rawJobList.Add(new Job("StructuralDesign", "StructuralEngineer", "StructuralEngineeringPractice", "DesignContractor", in5, out5));
            rawJobList.Add(new Job("Drywalling", "Carpenter", "Carpentry", "GeneralContractor", in6, out6));
            rawJobList.Add(new Job("Plastering", "Plasterer", "PlasteringAndPainting", "GeneralContractor", in7, out7));
            rawJobList.Add(new Job("Painting", "Painter", "PlasteringAndPainting", "GeneralContractor", in8, out8));

            List<Job> levelAssignedJobList = new List<Job>();
                levelAssignedJobList = rawJobList.Select(x => new Job(x.JobName, x.JobExecutor, x.OrganisationType, x.ContractingOrganisationType, x.OrderedCustomInputName1, x.OrderedCustomOutputName1, OrgLevelInt(rawJobList, rawJobList.IndexOf(x)))).ToList();
            
            List<Job> jobLocatedOnLevelList = new List<Job>();
            foreach (Job job in levelAssignedJobList)
            {
                List<Job> levelAssignedJob = rawJobList.Select(x => new Job(x.JobName, x.JobExecutor, x.OrganisationType, x.ContractingOrganisationType, x.OrderedCustomInputName1, x.OrderedCustomOutputName1, OrgLevelInt(rawJobList, rawJobList.IndexOf(x)), JobPlacementOnLevel(levelAssignedJobList, levelAssignedJobList.IndexOf(x)))).ToList();
            }
            return levelAssignedJobList; //update to jobLocatedOnLevelList
        }

        public int OrgLevelInt(List<Job> rawList, int rawListIndexNumber)
        {
            int index = rawListIndexNumber;
            int organisationLevel = 0;
            List<Job> toBeDiscarded = new List<Job>();
            List<Job> alreadyAdded = new List<Job>();  // can probably lose this somehow
            Dictionary<Job, int> allocateLevel = new Dictionary<Job, int>();
            foreach (Job job in rawList)
            {
                if (job.ContractingOrganisationType == string.Empty && job.OrganisationType != "Owner")
                {
                    toBeDiscarded.Add(job); //job entry validation here
                }
                if (job.OrganisationType == "Owner")
                {
                    allocateLevel.Add(job, organisationLevel += 1);
                    alreadyAdded.Add(job);
                }
            }
            while (alreadyAdded.Count < rawList.Count)
            {
                foreach (Job job in rawList)
                {
                    if (allocateLevel.Keys.FirstOrDefault(x => x.OrganisationType == job.ContractingOrganisationType) is Job suitableJob
                        && !allocateLevel.ContainsKey(job))
                    {
                        allocateLevel.Add(job, allocateLevel.FirstOrDefault(x => x.Key.OrganisationType.Equals(job.ContractingOrganisationType)).Value + 1);
                        alreadyAdded.Add(job);
                    }
                }
            }
            List<int> values = new List<int>();
            foreach (var value in allocateLevel)
            {
                values.Add(value.Value);
            }
            return values[index];
        }

        public int JobPlacementOnLevel(List<Job> levelAssigned, int levelAssignedListIndexNumber)
        {
            return 1;
        }
    }
}
