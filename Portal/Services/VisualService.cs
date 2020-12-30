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
        Canvas canvas = new Canvas();
        List<EntityNodeConfiguration> EntityNodes = new List<EntityNodeConfiguration>();
        List<StraightConnectorLineConfiguration> RelationshipLinks = new List<StraightConnectorLineConfiguration>();
        public List<EntityNodeConfiguration> GetNodePlottingGeometry(List<Job> customInputs)
        {           
            EntityNodes.Add(new EntityNodeConfiguration (0, 0, 0, GraphConstants.OWNERCOLOUR, GraphConstants.OWNERNODESIZE, "Owner")); //need to incorporate this into the main algorithm, then delete
            RelationshipLinks.Add(new StraightConnectorLineConfiguration(0, 0, 0, 10000, 10000, 10000, "rgb(66, 135, 245)", 7));

            // this object should look more like: JobList.Add(new Job("Owner", null, 0, "ProjectBriefCreation", 1, "ProjectOwner", List<JobSteps>, List<CustomInput>, List<CustomOutput>));       ,
            List<Job> JobList = new List<Job>();
            //                 company type                  type of company who contracted company          'level'            job itself         job level order number       job executor       number of job steps   number of inputs     number of outputs
            JobList.Add(new Job("Owner"                                     , null                            , 0,       "ProjectBriefCreation",            1,                  "ProjectOwner"          , 5,                    4,                    2));
            JobList.Add(new Job("GeneralContractor"                                       , "Owner"            , 1, "ProjectManagement"   , 1, "ContractsManager"      , 7, 2, 2));
            JobList.Add(new Job("DesignContractor"             , "Owner"            , 1, "DesignManagement"    , 2, "DesignContractsManager", 7, 2, 2));
            JobList.Add(new Job("ArchitecturalPractice"        , "DesignContractor" , 2, "BuildingDesign"      , 1, "LeadArchitect"         , 7, 2, 2));
            JobList.Add(new Job("StructuralEngineeringPractice", "DesignContractor" , 2, "StructuralDesign"    , 2, "StructuralEngineer"    , 7, 2, 2));
            JobList.Add(new Job("Carpentry"                    , "GeneralContractor", 2, "Drywalling"          , 3, "Carpenter"             , 5, 2, 2));
            JobList.Add(new Job("PlasteringAndPainting"        , "GeneralContractor", 2, "Plastering"          , 4, "Plasterer"             , 6, 2, 2));
            JobList.Add(new Job("PlasteringAndPainting"        , "GeneralContractor", 2, "Painting"            , 5, "Painter"               , 8, 2, 2));

            var orderedJobListByLevelNumber = JobList.GroupBy(j => j.LevelNumber, j => j, (level, jobs) => new { LevelNumber = level, Jobs = jobs.OrderBy(j => j.JobNumberOnLevel) }).OrderBy(e => e.LevelNumber);

            double diskRadialPortionLength = (canvas.OuterRadius - canvas.InnerRadius) / (orderedJobListByLevelNumber.Count() + 1);
            int halfDistanceBetweenJobStepNodes = 1250; //Using a nominal amount here for POC, but clash detection needs to be considered beyond that

            for (int ii = 0; ii < orderedJobListByLevelNumber.Count(); ii++)
            {
                int organisationNodeCountAtLevel = JobList.Where(j => j.LevelNumber == ii).GroupBy(j => j.OrganisationType).Count();
                int jobExecutorNodeCountAtLevel = JobList.Where(j => j.LevelNumber == ii).GroupBy(j => j.JobName).Where(g => g.Count() == 1).Count(); //this should be unique job type (job executor may have more than one job), needs more thought. not essential immediately as all job executors have one only job
                int jobStepNodeCountAtLevel = JobList.Where(j => j.LevelNumber == ii).Sum(job => job.StepCount);

                double angleBetweenJobStepNodesAtLevel = Math.Atan(halfDistanceBetweenJobStepNodes/(diskRadialPortionLength * ii + canvas.InnerRadius + 5000));
                double angleBetweenJobExecutorNodesAtLevel = Math.PI*2 / (jobExecutorNodeCountAtLevel + 1);
                int jobExecutorNodeSum = 0;

                for (int jj = 0; jj < organisationNodeCountAtLevel; jj++)
                {
                    var orderedJobsForLevel = JobList.Where(j => j.LevelNumber == ii).OrderBy(j => j.JobNumberOnLevel).Select(j => j.OrganisationType);
                    var organisationAggregate = orderedJobsForLevel.Aggregate(new List<(string organisationType, int count)> { }, (soFar, next) =>
                    {
                        if (soFar.Count > 0 && soFar.Last().organisationType != string.Empty && soFar.Last().organisationType == next)
                        {
                            var newCount = soFar.Last().count + 1;
                            soFar.RemoveAt(soFar.Count - 1);
                            soFar.Add((next, newCount));
                        }
                        else
                        {
                            soFar.Add((next, 1));
                        }
                        return soFar;
                    });
                    jobExecutorNodeSum += organisationAggregate.ElementAt(jj).count;
                    double previousOrganisationLastJobExecutorAngleFromStart = (jobExecutorNodeSum - organisationAggregate.ElementAt(jj).count)*angleBetweenJobExecutorNodesAtLevel;
                    double organisationNodeAngleFromStart = (double)(previousOrganisationLastJobExecutorAngleFromStart + angleBetweenJobExecutorNodesAtLevel + angleBetweenJobExecutorNodesAtLevel * (organisationAggregate.ElementAt(jj).count - 1 )/2);
                    //string organisationInformation = organisationAggregate.ElementAt(jj).Where(j => j.ElementAt(jj)).Select(j => j.JobExecutor).FirstOrDefault();
                    var organisationCoordArray = xyzArray(diskRadialPortionLength, ii, -5000, organisationNodeAngleFromStart);
                    nodePlacement(ii, (int[])organisationCoordArray, GraphConstants.ORGANISATIONCOLOUR, GraphConstants.ORGANISATIONNODESIZE, "O");
                }
                for (int kk = 0; kk < jobExecutorNodeCountAtLevel; kk++)
                {
                    var jobStepNodeCountForJob = JobList.Where(j => j.LevelNumber == ii && j.JobNumberOnLevel == kk +1).Select(j => j.StepCount).FirstOrDefault();
                    var customInputNodeCountForJob = JobList.Where(j => j.LevelNumber == ii && j.JobNumberOnLevel == kk + 1).Select(j => j.CustomInputCount).FirstOrDefault();
                    var customOutputNodeCountForJob = JobList.Where(j => j.LevelNumber == ii && j.JobNumberOnLevel == kk + 1).Select(j => j.CustomOutputCount).FirstOrDefault();
                    double jobExecutorNodeAngleFromStart = angleBetweenJobExecutorNodesAtLevel * (kk + 1);
                    string jobExecutorInformation = JobList.Where(j => j.LevelNumber == ii && j.JobNumberOnLevel == kk + 1).Select(j => j.JobExecutor).FirstOrDefault();
                    var jobExecutorCoordArray = xyzArray(diskRadialPortionLength, ii, 0, jobExecutorNodeAngleFromStart);
                    nodePlacement(ii, (int[])jobExecutorCoordArray, GraphConstants.JOBEXECUTORCOLOUR, GraphConstants.JOBEXECUTORNODESIZE, jobExecutorInformation);

                    for (int ll = 0; ll < jobStepNodeCountForJob; ll++)
                    {
                        double jobStepNodeAngleFromAssociatedJobExecutorNode = ((jobStepNodeCountForJob - 1) * angleBetweenJobStepNodesAtLevel / -2) + angleBetweenJobStepNodesAtLevel * ll;
                        double jobStepNodeAngleFromStart = jobExecutorNodeAngleFromStart - jobStepNodeAngleFromAssociatedJobExecutorNode;
                        var jobStepsCoordArray = xyzArray(diskRadialPortionLength, ii, 5000, jobStepNodeAngleFromStart);
                        nodePlacement(ii, (int[])jobStepsCoordArray, GraphConstants.JOBSTEPCOLOUR, GraphConstants.JOBSTEPNODESIZE, "O");
                    }
                    for (int mm = 0; mm < customInputNodeCountForJob; mm++)
                    {
                        double customInputNodeAngleFromAssociatedJobExecutorNode = ((jobStepNodeCountForJob - 1) * angleBetweenJobStepNodesAtLevel / -2) - angleBetweenJobStepNodesAtLevel; //plotting 2x (in the correct location) - this is simply because it's plotting all the custom inputs in the same place
                        double customInputNodeAngleFromStart = jobExecutorNodeAngleFromStart - customInputNodeAngleFromAssociatedJobExecutorNode;
                        var inputsCoordArray = xyzArray(diskRadialPortionLength, ii, 5000, customInputNodeAngleFromStart);
                        nodePlacement(ii, (int[])inputsCoordArray, GraphConstants.CUSTOMPRODUCTCOLOUR, GraphConstants.CUSTOMPRODUCTNODESIZE, "O");
                    }
                    for (int nn = 0; nn < customOutputNodeCountForJob; nn++)
                    {
                        double customOutputNodeAngleFromAssociatedJobExecutorNode = ((jobStepNodeCountForJob - 1) * angleBetweenJobStepNodesAtLevel / -2) + angleBetweenJobStepNodesAtLevel * (jobStepNodeCountForJob); //plotting 2x (in the correct location) - this is simply because it's plotting all the custom outputs in the same place
                        double customOutputNodeAngleFromStart = jobExecutorNodeAngleFromStart - customOutputNodeAngleFromAssociatedJobExecutorNode;
                        var outputsCoordArray = xyzArray(diskRadialPortionLength, ii, 5000, customOutputNodeAngleFromStart);
                        nodePlacement(ii, (int[])outputsCoordArray, GraphConstants.CUSTOMPRODUCTCOLOUR, GraphConstants.CUSTOMPRODUCTNODESIZE, "O");
                    }
                }
            }
            return EntityNodes;
        }
        private Array xyzArray(double radialPortion, int currentLevel, int offset, double nodeAngleFromStart)
        {
            double NodePlacementRadius = radialPortion * currentLevel + canvas.InnerRadius + offset;
            double x = 0;
            if (currentLevel != 0) { x += NodePlacementRadius * Math.Sin(nodeAngleFromStart); }
            double y = 0;
            if (currentLevel != 0) { y = NodePlacementRadius * Math.Cos(nodeAngleFromStart); }
            double proportionOfWayRoundHelix = nodeAngleFromStart / (2 * Math.PI);
            double z = 0;
            if (currentLevel != 0) { z = (proportionOfWayRoundHelix * - canvas.HeightOfHelix) - 100; }
            int[] coords = { (int)x, (int)y, (int)z };
            return coords.ToArray();
        }
        private void nodePlacement(int currentLevel, int[] coords, string nodeColour, int nodeSize, string toolTipText)
        {
            if (currentLevel == 0)
            { EntityNodes.Add(new EntityNodeConfiguration(coords, GraphConstants.OWNERCOLOUR, GraphConstants.OWNERNODESIZE, toolTipText)); }
            else
            { EntityNodes.Add(new EntityNodeConfiguration(coords, nodeColour, nodeSize, toolTipText)); }
        }
    }
}
    //Need to start turning the nodes into the actual 'objects' - start by adding a list of all the employees employed by each company into the tooltips
    //Add connecting lines/arrows - straight lines will suffice for POC - we need to be able to retrieve the co-ordinates to do this. Seems like the key here is to define the relationship types and cross them off one by one. Might be tricky to do before we have the data organised
    //Sort out this owner situation - the key is to draw up the actual required visual here, the rest should be easy from there - perhaps think about losing the dividing up a circle approach and just make it additive

    //Why is custom inputs a parameter above?
    //Priority 1: Can't get lines to plot
    //Priority 2: Upload an xlsx file, parse it to an object as above - the tree and network are hard, the counts are easier
    //Priority 3: Work out trace/legend functionality 'labels'
    //1. Do we need to use CSV, this was originally because of Neo4j, can we just import an excel doc directly?
    //2. Fix canvas size
    //4. Discuss making the nodes fixed sizes, so they get closer as you zoom in. It looks like we have to modify the plotly codebase itself to achieve this. The alternative would be to find a different canvas
    //5. Fix what i broke and get the configuration set up for the revised CSV
    //6. Try to get some data coming through from CSV into the lists
    //7. See if we can get textbox functionality to add data to nodes within the webpage
    //8. Remove the xyz coordinates from the tooltip

    //Need to be able to add all sub node types to the owner as well

