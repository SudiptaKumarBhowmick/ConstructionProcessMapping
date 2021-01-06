using Microsoft.EntityFrameworkCore.Internal;
using Portal.Models;
using Portal.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var service = new ContractualRelationshipTreeService2();
            //var testData = service.JobListWithOrganisationLevelAllocation(new List<Job>());

            //foreach (var item in testData)
            //{
            //    Console.WriteLine($"JobName: {item.JobName}, JobExecutor: {item.JobExecutor}, OrganisationType: {item.OrganisationType}, ContractingOrganisationType: {item.ContractingOrganisationType}, OrganisationLevelNumber: {item.OrganisationLevelNumber}");
            //}
            //Console.WriteLine();

            List<string> in1 = new List<string>(); in1.Add("a"); in1.Add("b");   //1
            List<string> out1 = new List<string>(); out1.Add("c"); out1.Add("d");//1

            List<string> in2 = new List<string>(); in2.Add("c"); in2.Add("e");   //2
            List<string> out2 = new List<string>(); out2.Add("g");               //2

            List<string> in3 = new List<string>(); in3.Add("c"); in3.Add("d");   //2
            List<string> out3 = new List<string>(); out3.Add("e"); out3.Add("f");//2

            List<string> in4 = new List<string>(); in4.Add("e"); in4.Add("f");   //3
            List<string> out4 = new List<string>(); out4.Add("l"); out4.Add("t");//3

            List<string> in5 = new List<string>(); in5.Add("e"); in5.Add("t");  //3
            List<string> out5 = new List<string>(); out5.Add("m"); out5.Add("u");  //3

            List<string> in6 = new List<string>(); in6.Add("e"); in6.Add("u");  //3
            List<string> out6 = new List<string>(); out6.Add("n"); out6.Add("v");  //3

            List<string> in7 = new List<string>(); in7.Add("e"); in7.Add("v");  //3
            List<string> out7 = new List<string>(); out7.Add("o"); out7.Add("w");  //3

            List<string> in8 = new List<string>(); in8.Add("e"); in8.Add("w");  //3
            List<string> out8 = new List<string>(); out8.Add("p"); out8.Add("x");  //3

            List<Job> rawJobList = new List<Job>();
            rawJobList.Add(new Job("ProjectBriefCreation", "ProjectOwner", "Owner", null, in1, out1, 1));
            rawJobList.Add(new Job("ProjectManagement", "ContractsManager", "GeneralContractor", "Owner", in2, out2, 2));
            rawJobList.Add(new Job("DesignManagement", "DesignContractsManager", "DesignContractor", "Owner", in3, out3, 2));
            rawJobList.Add(new Job("BuildingDesign", "LeadArchitect", "ArchitecturalPractice", "DesignContractor", in4, out4, 3));
            rawJobList.Add(new Job("StructuralDesign", "StructuralEngineer", "StructuralEngineeringPractice", "DesignContractor", in5, out5, 3));
            rawJobList.Add(new Job("Drywalling", "Carpenter", "Carpentry", "GeneralContractor", in6, out6, 3));
            rawJobList.Add(new Job("Plastering", "Plasterer", "PlasteringAndPainting", "GeneralContractor", in7, out7, 3));
            rawJobList.Add(new Job("Painting", "Painter", "PlasteringAndPainting", "GeneralContractor", in8, out8, 3));

            List<IGrouping<int, Job>> jobsByLevelNumber = rawJobList.GroupBy(j => j.OrganisationLevelNumber).ToList();

            List<Job> toBeDiscarded = new List<Job>();
            List<Job> alreadyAdded = new List<Job>();
            Dictionary<Job, int> allocateNumberOnLevel = new Dictionary<Job, int>();

            foreach (IGrouping<int, Job> item in jobsByLevelNumber)
            {
                foreach (Job subItem in item)
                {
                    if (!subItem.OrderedCustomInputName1.Any() && subItem.OrganisationType != "Owner")
                    {
                        toBeDiscarded.Add(subItem); //job entry validation here
                    }
                    if (!item.Any(x => x.OrderedCustomOutputName1.Intersect(subItem.OrderedCustomInputName1).Any()))
                    {
                        allocateNumberOnLevel.Add(subItem, 1);
                        alreadyAdded.Add(subItem);
                    }
                }
            }
            while (alreadyAdded.Count < rawJobList.Count)
            {
                foreach (IGrouping<int, Job> item in jobsByLevelNumber)
                {
                    foreach (Job subItem in item)
                    {
                        if (allocateNumberOnLevel./*Where(x => x.Value == subItem.OrganisationLevelNumber).*/Any(x => x.Key.OrderedCustomOutputName1.Intersect(subItem.OrderedCustomInputName1).Any())
                        && !allocateNumberOnLevel.ContainsKey(subItem))
                        {
                            allocateNumberOnLevel.Add(subItem, allocateNumberOnLevel.Keys.Any(x =>
                            {
                                IEnumerable<string> enumerable = x.OrderedCustomOutputName1.Intersect(subItem.OrderedCustomInputName1);
                                return enumerable;
                            }).Any());
                            allocateNumberOnLevel.Add(subItem, allocateNumberOnLevel.FirstOrDefault(x => x.Key.OrderedCustomOutputName1 == (subItem.OrderedCustomInputName1)).Value + 1);
                            alreadyAdded.Add(subItem);
                        }
                    }
                }
            }
            foreach (KeyValuePair<Job, int> item in allocateNumberOnLevel)
            {
                Console.WriteLine(allocateNumberOnLevel.Values);
            }
            Console.ReadLine();
        }
    }
}