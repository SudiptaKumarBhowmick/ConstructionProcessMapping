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
        public IJobDataService _jobDataService { get; set; } //do wee need this? (no references)
        Canvas canvas = new Canvas();
        List<EntityNodeConfiguration> EntityNodes = new List<EntityNodeConfiguration>();
        public VisualService(IJobDataService jobDataService) //do wee need this? (no references)
        {
            _jobDataService = jobDataService;
        }

        public async Task<StraightConnectionLineViewModel> GetLinePlottingGeometry(Guid recordID)
        {
            var result = new StraightConnectorLine();
            result.Lines.Add(new Line(0,0,0));
            result.Lines.Add(new Line(1000,1000,1000));
            return result.GetLineViewModel();
        }
        public async Task<List<EntityNodeConfiguration>> GetNodePlottingGeometry(Guid recordId)
        {
            List<Job> JobList = new List<Job>();
            JobList.Add(new Job("Owner", null, 0, "ProjectBriefCreation", 1, "ProjectOwner", 5, 4, 2));
            JobList.Add(new Job("GeneralContractor", "Owner", 1, "ProjectManagement", 1, "ContractsManager", 7, 2, 2));
            JobList.Add(new Job("DesignContractor", "Owner", 1, "DesignManagement", 2, "DesignContractsManager", 7, 2, 2));
            JobList.Add(new Job("ArchitecturalPractice", "DesignContractor", 2, "BuildingDesign", 1, "LeadArchitect", 7, 2, 2));
            JobList.Add(new Job("StructuralEngineeringPractice", "DesignContractor", 2, "StructuralDesign", 2, "StructuralEngineer", 7, 2, 2));
            JobList.Add(new Job("Carpentry", "GeneralContractor", 2, "Drywalling", 3, "Carpenter", 5, 2, 2));
            JobList.Add(new Job("PlasteringAndPainting", "GeneralContractor", 2, "Plastering", 4, "Plasterer", 6, 2, 2));
            JobList.Add(new Job("PlasteringAndPainting", "GeneralContractor", 2, "Painting", 5, "Painter", 8, 2, 2));

            var orderedJobListByLevelNumber = JobList.GroupBy(job => job.OrganisationLevelNumber, job => job, (level, jobs) => new { LevelNumber = level, Jobs = jobs.OrderBy(job => job.JobNumberOnLevel) }).OrderBy(e => e.LevelNumber);

            double diskRadialPortionLength = (canvas.OuterRadius - canvas.InnerRadius) / (orderedJobListByLevelNumber.Count() + 1);
            int halfDistanceBetweenJobStepNodes = 1250; //Using a nominal amount here for POC, but clash detection needs to be considered beyond that

            for (int ii = 0; ii < orderedJobListByLevelNumber.Count(); ii++)
            {
                int organisationNodeCountAtLevel = JobList.Where(job => job.OrganisationLevelNumber == ii).GroupBy(job => job.OrganisationType).Count();
                int jobExecutorNodeCountAtLevel = JobList.Where(job => job.OrganisationLevelNumber == ii).GroupBy(job => job.JobName).Where(g => g.Count() == 1).Count(); //this should be unique job type (job executor may have more than one job), needs more thought. not essential immediately as all job executors have one only job
                int jobStepNodeCountAtLevel = JobList.Where(job => job.OrganisationLevelNumber == ii).Sum(job => job.StepCount);

                double angleBetweenJobStepNodesAtLevel = Math.Atan(halfDistanceBetweenJobStepNodes/(diskRadialPortionLength * ii + canvas.InnerRadius + 5000));
                double angleBetweenJobExecutorNodesAtLevel = Math.PI*2 / (jobExecutorNodeCountAtLevel + 1);
                int jobExecutorNodeSum = 0;

                for (int jj = 0; jj < organisationNodeCountAtLevel; jj++)
                {
                    IEnumerable<string> orderedJobsForLevel = JobList.Where(job => job.OrganisationLevelNumber == ii).OrderBy(job => job.JobNumberOnLevel).Select(job => job.OrganisationType);
                    List<(string organisationType, int count)> organisationAggregate = orderedJobsForLevel.Aggregate(new List<(string organisationType, int count)> { }, (soFar, next) =>
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
                    string organisationInformation = organisationAggregate.ElementAt(jj).organisationType;
                    Array organisationCoordArray = xyzArray(diskRadialPortionLength, ii, -5000, organisationNodeAngleFromStart, "organisation");
                    nodePlacement(ii, (int[])organisationCoordArray, GraphConstants.ORGANISATIONCOLOUR, GraphConstants.ORGANISATIONNODESIZE, organisationInformation, "organisation", 1);
                }
                for (int kk = 0; kk < jobExecutorNodeCountAtLevel; kk++)
                {
                    int jobStepNodeCountForJob = JobList.Where(job => job.OrganisationLevelNumber == ii && job.JobNumberOnLevel == kk +1).Select(job => job.StepCount).FirstOrDefault();
                    int customInputNodeCountForJob = JobList.Where(job => job.OrganisationLevelNumber == ii && job.JobNumberOnLevel == kk + 1).Select(job => job.CustomInputCount).FirstOrDefault();
                    int customOutputNodeCountForJob = JobList.Where(job => job.OrganisationLevelNumber == ii && job.JobNumberOnLevel == kk + 1).Select(job => job.CustomOutputCount).FirstOrDefault();
                    double jobExecutorNodeAngleFromStart = angleBetweenJobExecutorNodesAtLevel * (kk + 1);
                    string jobExecutorInformation = JobList.Where(job => job.OrganisationLevelNumber == ii && job.JobNumberOnLevel == kk + 1).Select(job => job.JobExecutor).FirstOrDefault();
                    Array jobExecutorCoordArray = xyzArray(diskRadialPortionLength, ii, 0, jobExecutorNodeAngleFromStart, "jobExecutor");
                    nodePlacement(ii, (int[])jobExecutorCoordArray, GraphConstants.JOBEXECUTORCOLOUR, GraphConstants.JOBEXECUTORNODESIZE, jobExecutorInformation, "jobExecutor", 1);

                    for (int ll = 0; ll < jobStepNodeCountForJob; ll++)
                    {
                        double jobStepNodeAngleFromAssociatedJobExecutorNode = ((jobStepNodeCountForJob - 1) * angleBetweenJobStepNodesAtLevel / -2) + angleBetweenJobStepNodesAtLevel * ll;
                        double jobStepNodeAngleFromStart = jobExecutorNodeAngleFromStart - jobStepNodeAngleFromAssociatedJobExecutorNode;
                        string jobStepInformation = ""; //this needs to draw on information within the job spet, which isn't here yet
                        Array jobStepsCoordArray = xyzArray(diskRadialPortionLength, ii, 5000, jobStepNodeAngleFromStart, "jobStep");
                        nodePlacement(ii, (int[])jobStepsCoordArray, GraphConstants.JOBSTEPCOLOUR, GraphConstants.JOBSTEPNODESIZE, "O", "jobStep", 1);
                    }
                    for (int mm = 0; mm < customInputNodeCountForJob; mm++)
                    {
                        double customInputNodeAngleFromAssociatedJobExecutorNode = ((jobStepNodeCountForJob - 1) * angleBetweenJobStepNodesAtLevel / -2) - angleBetweenJobStepNodesAtLevel; //plotting 2x (in the correct location) - this is simply because it's plotting all the custom inputs in the same place
                        double customInputNodeAngleFromStart = jobExecutorNodeAngleFromStart - customInputNodeAngleFromAssociatedJobExecutorNode;
                        Array inputsCoordArray = xyzArray(diskRadialPortionLength, ii, 5000, customInputNodeAngleFromStart, "customInput");
                        nodePlacement(ii, (int[])inputsCoordArray, GraphConstants.CUSTOMPRODUCTCOLOUR, GraphConstants.CUSTOMPRODUCTNODESIZE, "O", "customInput", 1);
                    }
                    for (int nn = 0; nn < customOutputNodeCountForJob; nn++)
                    {
                        double customOutputNodeAngleFromAssociatedJobExecutorNode = ((jobStepNodeCountForJob - 1) * angleBetweenJobStepNodesAtLevel / -2) + angleBetweenJobStepNodesAtLevel * jobStepNodeCountForJob; //plotting 2x (in the correct location) - this is simply because it's plotting all the custom outputs in the same place
                        double customOutputNodeAngleFromStart = jobExecutorNodeAngleFromStart - customOutputNodeAngleFromAssociatedJobExecutorNode;
                        Array outputsCoordArray = xyzArray(diskRadialPortionLength, ii, 5000, customOutputNodeAngleFromStart, "customOutput");
                        nodePlacement(ii, (int[])outputsCoordArray, GraphConstants.CUSTOMPRODUCTCOLOUR, GraphConstants.CUSTOMPRODUCTNODESIZE, "O", "customOutput", 1);
                    }
                }
            }
            return EntityNodes;
        }
        private Array xyzArray(double radialPortion, int currentLevel, int offset, double nodeAngleFromStart, string type)
        {
            double NodePlacementRadius = radialPortion * currentLevel + canvas.InnerRadius + offset;
            double x = 0;
            if (currentLevel == 0 && type == "organisation") { } else{ x += NodePlacementRadius * Math.Sin(nodeAngleFromStart); }
            double y = 0;
            if (currentLevel == 0 && type == "organisation") { } else { y = NodePlacementRadius * Math.Cos(nodeAngleFromStart); }
            double proportionOfWayRoundHelix = nodeAngleFromStart / (2 * Math.PI);
            double z = 0;
            if (currentLevel == 0 && type == "organisation") { z -= canvas.HeightOfHelix / 2; } else { z = (proportionOfWayRoundHelix * - canvas.HeightOfHelix) - 100; }
            int[] coords = { (int)x, (int)y, (int)z };
            return coords.ToArray();
        }
        private void nodePlacement(int currentLevel, int[] coords, string nodeColour, int nodeSize, string toolTipText, string type, double opacity)
        {
            if (currentLevel == 0 && type == "organisation")
            { EntityNodes.Add(new EntityNodeConfiguration(coords, GraphConstants.OWNERCOLOUR, GraphConstants.OWNERNODESIZE, toolTipText, opacity)); }
            else
            { EntityNodes.Add(new EntityNodeConfiguration(coords, nodeColour, nodeSize, toolTipText, opacity)); }
        }
    }
}   //Lets get a new canvas set up on another page of the website - so that both plotly and the new canvas plot at the same time, for starters
    //1. Get a file coming in from a CSV
    //2. Get lines and nodes plotting on the same canvas
    //3. Get trace functionality working

    //Lets try to make it so that the coordinates become a part of the job object itself, including lists of coords for lists of job steps and so on
    //Need to start turning the nodes into the actual 'objects' - start by adding a list of all the employees employed by each company into the tooltips
    //Priority 3: Work out trace/legend functionality 'labels'
    //2. Fix canvas size
    //7. See if we can get textbox functionality to add data to nodes within the webpage
    //8. Remove the xyz coordinates from the tooltip

