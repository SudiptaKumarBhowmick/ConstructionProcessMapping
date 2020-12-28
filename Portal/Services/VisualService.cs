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
        public List<EntityNodeConfiguration> GetNodePlottingGeometry(List<Job> customInputs)
        {           
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

            var orderedJobListByLevelNumber = JobList.GroupBy(j => j.LevelNumber, j => j, (level, jobs) => new { LevelNumber = level, Jobs = jobs.OrderBy(j => j.JobNumberOnLevel) }).OrderBy(e => e.LevelNumber);
            //var jobCountByOrganisationType = JobList.GroupBy(j => j.LevelNumber, j => j, (level, jobs) => new { JobNumberOnLevel = level, Jobs = jobs.GroupBy(j => j.OrganisationType) }).Count();

            double diskRadialPortionLength = (canvas.OuterRadius - canvas.InnerRadius) / (orderedJobListByLevelNumber.Count() + 1); //checked
            int halfDistanceBetweenJobStepNodes = 1250; //going to just use a nominal amount here for POC, but clash detection needs to be considered beyond that

            for (int ii = 0; ii < orderedJobListByLevelNumber.Count(); ii++) //checked
            {
                int organisationNodeCountAtLevel = JobList.Where(j => j.LevelNumber == ii).GroupBy(j => j.OrganisationType).Count(); //checked
                int jobExecutorNodeCountAtLevel = JobList.Where(j => j.LevelNumber == ii).GroupBy(j => j.JobName).Where(g => g.Count() == 1).Count(); //this is actually unique job type (job executor may have more than one job), needs more thought. not essential immediately as all job executors have one job //checked
                int jobStepNodeCountAtLevel = JobList.Where(j => j.LevelNumber == ii).Sum(job => job.StepCount); //checked

                double angleBetweenJobStepNodesAtLevel = Math.Atan(halfDistanceBetweenJobStepNodes/(diskRadialPortionLength * ii + canvas.InnerRadius + 7000));
                double angleBetweenJobExecutorNodesAtLevel = Math.PI*2 / (jobExecutorNodeCountAtLevel + 1); //checked
                double organisationNodeAngleFromStart = 0;
                int jobExecutorCountBeforeCurrentOrganisation = 0;

                for (int jj = 0; jj < organisationNodeCountAtLevel; jj++)
                {
                    var orderJobCountByOrganisation = ;
                    //jobExecutorCountBeforeCurrentOrganisation += JobList.Where(j => j.LevelNumber == ii).GroupBy(j => j.OrganisationType).;
                    var jobExecutorNodeCountForOrganisation = JobList.Where(j => j.LevelNumber == ii).GroupBy(j => j.OrganisationType).Count();
                    var organisationalAngleFromEmployeeDomainStart = JobList.GroupBy(j => j.LevelNumber, j => j, (level, jobs) => new { LevelNumber = level, Jobs = jobs.OrderBy(j => j.JobNumberOnLevel) }).OrderBy(e => e.LevelNumber);
                    organisationNodeAngleFromStart += organisationNodeAngleFromStart + angleBetweenJobExecutorNodesAtLevel * (jj + 1.5) / 2;
                    xyzLocations(diskRadialPortionLength, ii, -7000, organisationNodeAngleFromStart, GraphConstants.ORGANISATIONCOLOUR, GraphConstants.ORGANISATIONNODESIZE);
                }  
                for (int kk = 0; kk < jobExecutorNodeCountAtLevel; kk++)
                {
                    var jobStepNodeCountForJob = JobList.Where(j => j.LevelNumber == ii && j.JobNumberOnLevel == kk +1).Select(j => j.StepCount); //checked - something not quite right here, should be outputting the firstordefualt val
                    var customInputNodeCountForJob = JobList.Where(j => j.LevelNumber == ii && j.JobNumberOnLevel == kk + 1).Select(j => j.CustomInputCount);
                    var customOutputNodeCountForJob = JobList.Where(j => j.LevelNumber == ii && j.JobNumberOnLevel == kk + 1).Select(j => j.CustomOutputCount);
                    double jobExecutorNodeAngleFromStart = angleBetweenJobExecutorNodesAtLevel * (kk + 1); //checked
                    xyzLocations(diskRadialPortionLength, ii, 0, jobExecutorNodeAngleFromStart, GraphConstants.JOBEXECUTORCOLOUR, GraphConstants.JOBEXECUTORNODESIZE); //checked

                    for (int ll = 0; ll < jobStepNodeCountForJob.FirstOrDefault(); ll++) //should not need to apply firstordefault at this point
                    {
                        double jobStepNodeAngleFromAssociatedJobExecutorNode = ((jobStepNodeCountForJob.FirstOrDefault() - 1) * angleBetweenJobStepNodesAtLevel / -2) + angleBetweenJobStepNodesAtLevel * ll;
                        double jobStepNodeAngleFromStart = jobExecutorNodeAngleFromStart - jobStepNodeAngleFromAssociatedJobExecutorNode;
                        xyzLocations(diskRadialPortionLength, ii, 7000, jobStepNodeAngleFromStart, GraphConstants.JOBSTEPCOLOUR, GraphConstants.JOBSTEPNODESIZE);
                    }
                    for (int mm = 0; mm < customInputNodeCountForJob.FirstOrDefault(); mm++)
                    {
                        double customInputNodeAngleFromAssociatedJobExecutorNode = ((jobStepNodeCountForJob.FirstOrDefault() - 1) * angleBetweenJobStepNodesAtLevel / -2) - angleBetweenJobStepNodesAtLevel; //plotting 2x (in the correct location)
                        double customInputNodeAngleFromStart = jobExecutorNodeAngleFromStart - customInputNodeAngleFromAssociatedJobExecutorNode;
                        xyzLocations(diskRadialPortionLength, ii, 7000, customInputNodeAngleFromStart, GraphConstants.CUSTOMPRODUCTCOLOUR, GraphConstants.CUSTOMPRODUCTNODESIZE);
                    }
                    for (int nn = 0; nn < customOutputNodeCountForJob.FirstOrDefault(); nn++)
                    {
                        double customOutputNodeAngleFromAssociatedJobExecutorNode = ((jobStepNodeCountForJob.FirstOrDefault() - 1) * angleBetweenJobStepNodesAtLevel / -2) + angleBetweenJobStepNodesAtLevel * (jobStepNodeCountForJob.FirstOrDefault());
                        double customOutputNodeAngleFromStart = jobExecutorNodeAngleFromStart - customOutputNodeAngleFromAssociatedJobExecutorNode;
                        xyzLocations(diskRadialPortionLength, ii, 7000, customOutputNodeAngleFromStart, GraphConstants.CUSTOMPRODUCTCOLOUR, GraphConstants.CUSTOMPRODUCTNODESIZE);
                    }
                }
            }
            return EntityNodes;
        }
        private void xyzLocations(double radialPortion, int currentLevel, int offset, double nodeAngleFromStart, string nodeColour, int nodeSize)
        {
            double NodePlacementRadius = radialPortion * currentLevel + canvas.InnerRadius + offset;
            double NodeX = NodePlacementRadius * Math.Sin(nodeAngleFromStart);
            double NodeY = NodePlacementRadius * Math.Cos(nodeAngleFromStart);
            double proportionOfWayRoundHelix = nodeAngleFromStart / (2 * Math.PI);
            double NodeZ = (proportionOfWayRoundHelix * -canvas.HeightOfHelix) - 100;
            EntityNodes.Add(new EntityNodeConfiguration((int)NodeX, (int)NodeY, (int)NodeZ, nodeColour, nodeSize));
        }
    }
}
    //4. Fix canvas size
    //5. Work out trace/legend functionality 'labels'
    //8. Discuss making the nodes fixed sizes, so they get closer as you zoom in. It looks like we have to modify the plotly codebase itself to achieve this. The alternative would be to find a different canvas
    //9. Fix what i broke and get the configuration set up for the revised CSV
    //10. Try to get some data coming through from CSV into the lists
    //11. See if we can get textbox functionality to add data to nodes within the webpage
    //12. Remove the xyz coordinates from the tooltip
    //13. Do we need to use CSV, can we just import an excel doc directly?

    //Need to be able to add all sub node types to the owner as well
    //Add connecting lines/arrows - straight lines will suffice for POC
