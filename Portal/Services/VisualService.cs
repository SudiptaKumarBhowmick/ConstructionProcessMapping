using Portal.Constants;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class VisualService : IVisualService
    {
        public List<EntityNodeConfiguration> GetNodePlottingGeometry(List<Job> customInputs)
        {
            Canvas canvas = new Canvas();
            List<EntityNodeConfiguration> EntityNodes = new List<EntityNodeConfiguration>();
            EntityNodes.Add(new EntityNodeConfiguration (0, 0, 0, GraphConstants.OWNERCOLOUR, GraphConstants.OWNERNODESIZE)); //need to incorporate this into the main algorithm, then delete

            List<Job> JobList = new List<Job>();
            JobList.Add(new Job("Owner"                        , null               , 0, "ProjectBriefCreation", 1, "ProjectOwner"          , 5, 2, 2));
            JobList.Add(new Job("GeneralContractor"            , "Owner"            , 1, "ProjectManagement"   , 1, "ContractsManager"      , 7, 2, 2));
            JobList.Add(new Job("DesignContractor"             , "Owner"            , 1, "DesignManagement"    , 2, "DesignContractsManager", 7, 2, 2));
            JobList.Add(new Job("ArchitecturalPractice"        , "DesignContractor" , 2, "BuildingDesign"      , 1, "LeadArchitect"         , 7, 2, 2));
            JobList.Add(new Job("StructuralEngineeringPractice", "DesignContractor" , 2, "StructuralDesign"    , 2, "StructuralEngineer"    , 7, 2, 2));
            JobList.Add(new Job("Carpentry"                    , "GeneralContractor", 2, "Drywalling"          , 3, "Carpenter"             , 5, 2, 2));
            JobList.Add(new Job("PlasteringAndPainting"        , "GeneralContractor", 2, "Plastering"          , 4, "Plasterer"             , 6, 2, 2));
            JobList.Add(new Job("PlasteringAndPainting"        , "GeneralContractor", 2, "Painting"            , 5, "Painter"               , 8, 2, 2));

            //var jobsByLevelNumber =            JobList.GroupBy(j => j.LevelNumber, j => j, (level, jobs) => new { LevelNumber = level, Jobs = jobs });
            var orderedJobListByLevelNumber = JobList.GroupBy(j => j.LevelNumber, j => j, (level, jobs) => new { LevelNumber = level, Jobs = jobs.OrderBy(j => j.JobNumberOnLevel) }).OrderBy(e => e.LevelNumber);
            var count = JobList.Where(j => j.LevelNumber == 1).GroupBy(j => j.OrganisationType).Where(g => g.Count() == 1).Count();
            //var jobJobCountByOrganisationType = JobList.GroupBy(j => j.LevelNumber, j => j, (level, organisation) => new { LevelNumber = level, Jobs = organisation.OrderBy(j => j.OrganisationType) }).OrderBy(e => e.LevelNumber);
            var jobCountByOrganisationType = JobList.GroupBy(j => j.LevelNumber).OrderBy(j => j.Key);

            //var jobCountByOrganisationType =
            //        from job in JobList
            //        group job by job.LevelNumber into newGroup1
            //        from newGroup2 in
            //            (from job in newGroup1
            //             group job by job.OrganisationType)
            //        group newGroup2 by newGroup1.Key;

            double jobExecutorNodePlacementDivisionRadius = (canvas.OuterRadius - canvas.InnerRadius) / (orderedJobListByLevelNumber.Count() + 1);

            for (int ii = 0; ii < orderedJobListByLevelNumber.Count(); ii++)
            {
                int organisationalNodeCountAtLevel = JobList.Where(j => j.LevelNumber == ii).GroupBy(j => j.OrganisationType).Count();
                int jobStepNodeCountAtLevel = JobList.Where(j => j.LevelNumber == ii).Sum(job => job.StepCount);
                int jobExecutorCountAtLevel = JobList.Where(j => j.LevelNumber == ii).GroupBy(j => j.JobName).Where(g => g.Count() == 1).Count(); //this is actually unique job type (job executor may have more than one job), needs more thought. not essential immediately as all job executors have one job

                double angleBetweenJobStepNodes = (Math.PI / 180) * 360 / (jobStepNodeCountAtLevel - jobStepNodeCountAtLevel + jobStepNodeCountAtLevel * 12); //this is clearly not right. This should also be made level dependant, so that there is a consistent distance between nodes rather than a consistent angle
                double angleBetweenJobExecutorNodesAtLevel = (Math.PI / 180) * 360 / (jobExecutorCountAtLevel + 1);
                double organisationNodeAngleFromStart = 0;
                int jobExecutorCountBeforeCurrentOrganisation = 0;

                for (int jj = 0; jj < organisationalNodeCountAtLevel; jj++)
                {
                    jobExecutorCountBeforeCurrentOrganisation += JobList.Where(j => j.LevelNumber == ii).GroupBy(j => j.OrganisationType).;
                    int countOfJobExecutorNodesForOrganisation = JobList.Where(j => j.LevelNumber == ii).Where(j => j.OrganisationType == jj);
                    double organisationNodePlacementRadius = jobExecutorNodePlacementDivisionRadius * (ii + 1) + canvas.InnerRadius - 7000;
                    var organisationalAngleFromEmployeeDomainStart = JobList.GroupBy(j => j.LevelNumber, j => j, (level, jobs) => new { LevelNumber = level, Jobs = jobs.OrderBy(j => j.JobNumberOnLevel) }).OrderBy(e => e.LevelNumber);
                    organisationNodeAngleFromStart += organisationNodeAngleFromStart + angleBetweenJobExecutorNodesAtLevel * (jj + 1.5) / 2;
                    xyzLocations(organisationNodePlacementRadius, organisationNodeAngleFromStart, EntityNodes, canvas.HeightOfHelix, GraphConstants.ORGANISATIONCOLOUR, GraphConstants.ORGANISATIONNODESIZE);
                    
                    for (int kk = 0; kk < jobExecutorCountAtLevel; kk++)
                    {
                        double jobExecutorNodeAngleFromStart = angleBetweenJobExecutorNodesAtLevel * (kk + 1);
                        double jobExecutorNodePlacementRadius = jobExecutorNodePlacementDivisionRadius * (ii + 1) + canvas.InnerRadius;
                        xyzLocations(jobExecutorNodePlacementRadius, jobExecutorNodeAngleFromStart, EntityNodes, canvas.HeightOfHelix, GraphConstants.JOBEXECUTORCOLOUR, GraphConstants.JOBEXECUTORNODESIZE);

                        for (int ll = 0; ll < jobStepNodeCountAtLevel; ll++)
                        {
                            double jobStepNodePlacementRadius = jobExecutorNodePlacementDivisionRadius * (ii + 1) + canvas.InnerRadius + 7000;
                            double jobStepNodeAngleFromAssociatedJobExecutorNode = (((jobStepNodeCountAtLevel - 1) * angleBetweenJobStepNodes) / -2) + angleBetweenJobStepNodes * ll;
                            double jobStepNodeAngleFromStart = jobExecutorNodeAngleFromStart - jobStepNodeAngleFromAssociatedJobExecutorNode;
                            xyzLocations(jobStepNodePlacementRadius, jobStepNodeAngleFromStart, EntityNodes, canvas.HeightOfHelix, GraphConstants.JOBSTEPCOLOUR, GraphConstants.JOBSTEPNODESIZE);
                            //Below, place the product nodes on the canvas
                        }
                    }
                }
            }
            return EntityNodes;


                //    #region oldWay
                //    int totalJobStepCount1 = 0;
                //int totalJobStepCount2 = 0;
                //List<int> PseudoJobList1 = new List<int>(); //here the jobs are ordered according to the lists order, the value refers to the number of job steps
                //PseudoJobList1.Add(2);
                //PseudoJobList1.Add(5);
                //PseudoJobList1.Add(6);
                //PseudoJobList1.Add(4);
                //for (int jjj = 0; jjj < PseudoJobList1.Count; jjj++)
                //{
                //    totalJobStepCount1 += PseudoJobList1.ElementAt(jjj);
                //}
                //List<int> jobStepCountList1 = new List<int>();
                //for (int jjj2 = 0; jjj2 < PseudoJobList1.Count; jjj2++)
                //{
                //    jobStepCountList1.Add(PseudoJobList1.ElementAt(jjj2));
                //}

                //List<int> PseudoJobList2 = new List<int>(); //here the jobs are ordered according to the lists order, the value refers to the number of job steps
                //PseudoJobList2.Add(2);
                //PseudoJobList2.Add(4);
                //for (int kkk = 0; kkk < PseudoJobList2.Count; kkk++)
                //{
                //    totalJobStepCount1 += PseudoJobList2.ElementAt(kkk);
                //}
                //List<int> jobStepCountList2 = new List<int>();
                //for (int kkk2 = 0; kkk2 < PseudoJobList2.Count; kkk2++)
                //{
                //    jobStepCountList2.Add(PseudoJobList2.ElementAt(kkk2));
                //}

                //List<int> PseudoJobList3 = new List<int>(); //here the jobs are ordered according to the lists order, the value refers to the number of job steps
                //PseudoJobList3.Add(8);
                //PseudoJobList3.Add(6);
                //PseudoJobList3.Add(7);
                //PseudoJobList3.Add(5);
                //for (int lll = 0; lll < PseudoJobList3.Count; lll++)
                //{
                //    totalJobStepCount2 += PseudoJobList3.ElementAt(lll);
                //}
                //List<int> jobStepCountList3 = new List<int>();
                //for(int lll2 = 0; lll2 < PseudoJobList3.Count; lll2++)
                //{
                //    jobStepCountList3.Add(PseudoJobList3.ElementAt(lll2));
                //}

                //List<int> jobExecutorsCountPerLevelList = new List<int>();
                //jobExecutorsCountPerLevelList.Add(PseudoJobList1.Count + PseudoJobList2.Count);
                //jobExecutorsCountPerLevelList.Add(PseudoJobList3.Count);

                //List<int> jobStepCountPerLevelList = new List<int>();
                //jobStepCountPerLevelList.Add(totalJobStepCount1);
                //jobStepCountPerLevelList.Add(totalJobStepCount2);

                //List<List<int>> uniqueJobStepCountListList = new List<List<int>>();
                //uniqueJobStepCountListList.Add(jobStepCountList1);
                //uniqueJobStepCountListList.Add(jobStepCountList2);
                //uniqueJobStepCountListList.Add(jobStepCountList3);

                //Dictionary<List<int>, int> OrgJobStepCount = new Dictionary<List<int>, int>();
                //OrgJobStepCount.Add(PseudoJobList1, 1);
                //OrgJobStepCount.Add(PseudoJobList2, 1);
                //OrgJobStepCount.Add(PseudoJobList3, 2);
                //int levelCount = 0;
                //foreach (var value in OrgJobStepCount.Values.Distinct())
                //{
                //    levelCount += 1;
                //}
                //Dictionary<int, int> valCount = new Dictionary<int, int>();
                //foreach (int value in OrgJobStepCount.Values)
                //    if (valCount.ContainsKey(value))
                //        valCount[value]++;
                //    else
                //        valCount[value] = 1;

                //double jobExecutorNodePlacementDivisionRadius = (canvas.OuterRadius - canvas.InnerRadius) / (levelCount + 1);

                //for (int ii = 0; ii < levelCount; ii++)
                //{   //Here, setting the distance of each organisational level from the centre, setting how many organisational nodes are at the current level and evaluating the angle between job executors and job steps at the current level
                //    double organisationNodePlacementRadius = jobExecutorNodePlacementDivisionRadius * (ii + 1) + canvas.InnerRadius - 7000;
                //    int organisationalNodeCount = valCount.ElementAt(index: ii).Value;
                //    double angleBetweenJobStepNodes = (Math.PI / 180) * 360 / (jobStepCountPerLevelList.ElementAt(ii) - jobStepCountPerLevelList.ElementAt(ii) + jobStepCountPerLevelList.ElementAt(ii) * 12);
                //    double angleBetweenJobExecutorNodes = (Math.PI / 180) * 360 / (jobExecutorsCountPerLevelList.ElementAt(ii) + 1);
                //    double organisationNodeAngleFromStart = 0;

                //    for (int jj = 0; jj < organisationalNodeCount; jj++)
                //    {
                //        organisationNodeAngleFromStart += organisationNodeAngleFromStart + angleBetweenJobExecutorNodes * (OrgJobStepCount.ElementAt(jj).Key.Count + 0.5) / 2;
                //        xyzLocations(organisationNodePlacementRadius, organisationNodeAngleFromStart, EntityNodes, canvas.HeightOfHelix, GraphConstants.ORGANISATIONCOLOUR, GraphConstants.ORGANISATIONNODESIZE);

                //        for (int kk = 0; kk < jobExecutorsCountPerLevelList.ElementAt(ii); kk++)
                //        {
                //            double jobExecutorNodeAngleFromStart = angleBetweenJobExecutorNodes * (kk + 1);
                //            double jobExecutorNodePlacementRadius = jobExecutorNodePlacementDivisionRadius * (ii + 1) + canvas.InnerRadius;
                //            xyzLocations(jobExecutorNodePlacementRadius, jobExecutorNodeAngleFromStart, EntityNodes, canvas.HeightOfHelix, GraphConstants.JOBEXECUTORCOLOUR, GraphConstants.JOBEXECUTORNODESIZE);

                //            for (int ll = 0; ll < jobStepCountPerLevelList.ElementAt(ii); ll++)
                //            {
                //                double jobStepNodePlacementRadius = jobExecutorNodePlacementDivisionRadius * (ii + 1) + canvas.InnerRadius + 7000;
                //                double jobStepNodeAngleFromAssociatedJobExecutorNode = (((jobStepCountPerLevelList.ElementAt(ii) - 1) * angleBetweenJobStepNodes) / -2) + angleBetweenJobStepNodes * ll;
                //                double jobStepNodeAngleFromStart = jobExecutorNodeAngleFromStart - jobStepNodeAngleFromAssociatedJobExecutorNode;
                //                xyzLocations(jobStepNodePlacementRadius, jobStepNodeAngleFromStart, EntityNodes, canvas.HeightOfHelix, GraphConstants.JOBSTEPCOLOUR, GraphConstants.JOBSTEPNODESIZE);
                //                //Below, placing the product nodes on the canvas
                //            }
                //        }
                //    }
                //}
                //return EntityNodes;
                //#endregion
            }
        private void xyzLocations(double nodePlacementRadius, double nodeAngleFromStart, List<EntityNodeConfiguration> nodeList, int heightOfHelix, string nodeColour, int nodeSize)
        {
            double NodeX = nodePlacementRadius * Math.Sin(nodeAngleFromStart);
            double NodeY = nodePlacementRadius * Math.Cos(nodeAngleFromStart);
            double proportionOfWayRoundHelix = nodeAngleFromStart / (2 * Math.PI);
            double NodeZ = (proportionOfWayRoundHelix * -heightOfHelix) - 100;
            nodeList.Add(new EntityNodeConfiguration((int)NodeX, (int)NodeY, (int)NodeZ, nodeColour, nodeSize));
        }
    }
}

    //3. Far too many nodes are being plotted?
    //4. Fix canvas size
    //5. Work out trace/legend functionality 'labels'
    //8. Discuss making the nodes fixed sizes, so they get closer as you zoom in. It looks like we have to modify the plotly codebase itself to achieve this. The alternative would be to find a different canvas
    //9. Fix what i broke and get the configuration set up for the revised CSV
    //10. Try to get some data coming through from CSV into the lists
    //11. See if we can get textbox functionality to add data to nodes within the webpage
    //12. Remove the xyz coordinates from the tooltip
    //13. Do we need to use CSV, can we just import an excel doc directly?

    //Add products/inputs/outputs
    //Need to be able to add all sub node types to the owner as well
    //Add connecting lines/arrows - straight lines will suffice for POC
