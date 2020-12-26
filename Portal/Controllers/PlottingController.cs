using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers
{
    public class PlottingController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Canvas = (List<EntityNodeConfiguration>)NodePlottingGeometry(null) /*+ (List<RelationshipArrowConfiguration >)RelationshipArrowPlottingGeometry(null)*/;
            return View();
        }
        //What if we find a better visualisation library?
        //Allocate a proportionate slot for the job according to how many steps, inputs and outputs it has, the organisation slot then wraps around all the job executors and jobs in its domain. If an organistation contains no jobs, presume a simgle job executor and a job with a single input, a single output and 6 job steps (MAKE THESE NODES INVISIBLE)
        private List<EntityNodeConfiguration> NodePlottingGeometry(List<Job> customInputs)
        {
            List<EntityNodeConfiguration> EntityNodes = new List<EntityNodeConfiguration>();
            EntityNodes.Add(new EntityNodeConfiguration {marker = new Marker {color = "rgb(66, 135, 245)", size = 20}, type = "scatter3d", x = new List<int> {0}, y = new List<int> {0}, z = new List<int> {0}});

            int innerRadius = 9000; //this info belongs in CanvasViewLayout
            int outerRadius = 85720; //this info belongs in CanvasViewLayout
            int heightOfHelix = 22000; //this info belongs in CanvasViewLayout

            List<Job> JobList = new List<Job>();
            JobList.Add(new Job("Carpentry", "GeneralContractor", "Drywalling", "Carpenter", 5, 2, 2)); 
            JobList.Add(new Job("PlasteringAndPainting", "GeneralContractor", "Plastering", "Plasterer", 6, 2, 2));
            JobList.Add(new Job("PlasteringAndPainting", "GeneralContractor", "Painting", "Painter", 8, 2, 2)); 
            JobList.Add(new Job("GeneralContractor", "Owner", "ProjectManagement", "ContractsManager", 7, 2, 2));

            List<Job> OrganistationTree = JobList;
            OrganistationTree.Sort((x, y) => x.ContractingOrganisationType.CompareTo(y.OrganisationType));

            //1  = owner developer
            //2  = general contractor
            //3a = Carpentry
            //3b = PlasteringAndPainting

            int totalJobStepCount1 = 0;
            int totalJobStepCount2 = 0;
            List<int> PseudoJobList1 = new List<int>(); //here the jobs are ordered according to the lists order, the value refers to the number of job steps
            PseudoJobList1.Add(2);
            PseudoJobList1.Add(5);
            PseudoJobList1.Add(6);
            PseudoJobList1.Add(4);
            for (int jjj = 0; jjj < PseudoJobList1.Count; jjj++)
            {
                totalJobStepCount1 += PseudoJobList1.ElementAt(jjj);
            }
            List<int> PseudoJobList2 = new List<int>(); //here the jobs are ordered according to the lists order, the value refers to the number of job steps
            PseudoJobList2.Add(2);
            PseudoJobList2.Add(4);
            for (int kkk = 0; kkk < PseudoJobList2.Count; kkk++)
            {
                totalJobStepCount1 += PseudoJobList2.ElementAt(kkk);
            }
            List<int> PseudoJobList3 = new List<int>(); //here the jobs are ordered according to the lists order, the value refers to the number of job steps
            PseudoJobList3.Add(8);
            PseudoJobList3.Add(6);
            PseudoJobList3.Add(7);
            PseudoJobList3.Add(5);
            for (int lll = 0; lll < PseudoJobList3.Count; lll++)
            {
                totalJobStepCount2 += PseudoJobList3.ElementAt(lll);
            }

            //1. Build node constructor in EntityNodeConfiguration
            //2. Get 'xyzlocations' method working
            //3. Far too many nodes are being plotted?
            //4. Fix canvas size
            //5. Work out trace/legend functionality 'labels'
            //6. Do we need both the StructuringInformationModel and the Job?
            //7. Build the CanvasViewLayout - we just need to declare the sizes for now
            //8. Discuss making the nodes fixed sizes, so they get closer as you zoom in. It looks like we have to modify the plotly codebase itself to achieve this. The alternative would be to find a different canvas
            //9. Fix what i broke and get the configuration set up for the revised CSV
            //10. Try to get some data coming through from CSV into the lists
            //11. See if we can get textbox functionality to add data to nodes within the webpage
            //12. Remove the xyz coordinates from the tooltip

            //Add products/inputs/outputs
            //Need to be able to add all sub node types to the owner as well
            //Add connecting lines/arrows - straight lines will suffice for POC

            List<int> jobExecutorsCountPerLevelList = new List<int>();
            jobExecutorsCountPerLevelList.Add(PseudoJobList1.Count + PseudoJobList2.Count);
            jobExecutorsCountPerLevelList.Add(PseudoJobList3.Count);

            List<int> jobStepCountPerLevelList = new List<int>();
            jobStepCountPerLevelList.Add(totalJobStepCount1);
            jobStepCountPerLevelList.Add(totalJobStepCount2);

            Dictionary<List<int>, int> OrgJobStepCount = new Dictionary<List<int>, int>();
            OrgJobStepCount.Add(PseudoJobList1, 1);
            OrgJobStepCount.Add(PseudoJobList2, 1);
            OrgJobStepCount.Add(PseudoJobList3, 2);
            int levelCount = 0;
            foreach (var value in OrgJobStepCount.Values.Distinct())
            {                
                levelCount += 1;
            }
            Dictionary<int, int> valCount = new Dictionary<int, int>();
            foreach (int value in OrgJobStepCount.Values)
                if (valCount.ContainsKey(value))
                    valCount[value]++;
                else
                    valCount[value] = 1;

            double jobExecutorNodePlacementDivisionRadius = (outerRadius - innerRadius) / (levelCount + 1);

            for (int ii = 0; ii < levelCount; ii++)
            {   //Here, setting the distance of each organisational level from the centre, setting how many organisational nodes are at the current level and evaluating the angle between job executors and job steps at the current level
                double organisationNodePlacementRadius = jobExecutorNodePlacementDivisionRadius * (ii + 1) + innerRadius - 7000;
                int organisationalNodeCount = valCount.ElementAt(index: ii).Value;
                double angleBetweenJobStepNodes = (Math.PI / 180) * 360 / (jobStepCountPerLevelList.ElementAt(ii) - jobStepCountPerLevelList.ElementAt(ii) + jobStepCountPerLevelList.ElementAt(ii) * 12);
                double angleBetweenJobExecutorNodes = (Math.PI / 180) *360 / (jobExecutorsCountPerLevelList.ElementAt(ii)+1);
                double organisationNodeAngleFromStart = 0;

                for (int jj = 0; jj < organisationalNodeCount; jj++)
                {   //Below, placing the organisation nodes on the canvas (still to add connector lines)
                    organisationNodeAngleFromStart += organisationNodeAngleFromStart + angleBetweenJobExecutorNodes * (OrgJobStepCount.ElementAt(jj).Key.Count + 0.5) / 2;
                    xyzLocations(organisationNodePlacementRadius, organisationNodeAngleFromStart, EntityNodes, heightOfHelix, "rgb(168, 29, 242)", 9);
                    //double organisationNodeX = organisationNodePlacementRadius * Math.Sin(organisationNodeAngleFromStart); //extract to method
                    //double organisationNodeY = organisationNodePlacementRadius * Math.Cos(organisationNodeAngleFromStart); //extract to method
                    //double proportionOfWayRoundHelix1 = organisationNodeAngleFromStart / (2 * Math.PI); //extract to method
                    //double organisationNodeZ = (proportionOfWayRoundHelix1 * -heightOfHelix); //extract to method
                    //EntityNodes.Add(new EntityNodeConfiguration { marker = new Marker { color = "rgb(168, 29, 242)", size = 9 }, type = "scatter3d", x = new List<int> { (int)organisationNodeX }, y = new List<int> { (int)organisationNodeY }, z = new List<int> { (int)organisationNodeZ } }); //extract to method

                    for (int kk = 0; kk < jobExecutorsCountPerLevelList.ElementAt(ii); kk++)
                    {   //Below, placing the job executor nodes on the canvas (still to add connector lines)
                        double jobExecutorNodeAngleFromStart = angleBetweenJobExecutorNodes * (kk + 1);
                        double jobExecutorNodePlacementRadius = jobExecutorNodePlacementDivisionRadius*(ii+1) + innerRadius;
                        xyzLocations(jobExecutorNodePlacementRadius, jobExecutorNodeAngleFromStart, EntityNodes, heightOfHelix, "rgb(240, 236, 41)", 6);
                        //double jobExecutorNodeX = jobExecutorNodePlacementRadius * Math.Sin(jobExecutorNodeAngleFromStart); //extract to method
                        //double jobExecutorNodeY = jobExecutorNodePlacementRadius * Math.Cos(jobExecutorNodeAngleFromStart); //extract to method
                        //double proportionOfWayRoundHelix2 = jobExecutorNodeAngleFromStart / (2 * Math.PI); //extract to method
                        //double jobExecutorNodeZ = (proportionOfWayRoundHelix2 * -heightOfHelix) - 50; //extract to method
                        //EntityNodes.Add(new EntityNodeConfiguration { marker = new Marker { color = "rgb(240, 236, 41)", size = 6 }, type = "scatter3d", x = new List<int> { (int)jobExecutorNodeX }, y = new List<int> { (int)jobExecutorNodeY }, z = new List<int> { (int)jobExecutorNodeZ } }); //extract to method

                        for (int ll = 0; ll < jobStepCountPerLevelList.ElementAt(ii); ll++)
                        {   //Below, placing the job step nodes on the canvas (still to add connector lines)
                            double jobStepNodePlacementRadius = jobExecutorNodePlacementDivisionRadius * (ii + 1) + innerRadius + 7000;
                            double jobStepNodeAngleFromAssociatedJobExecutorNode = (((jobStepCountPerLevelList.ElementAt(ii) - 1) * angleBetweenJobStepNodes) / -2) + angleBetweenJobStepNodes * ll;
                            double jobStepNodeAngleFromStart = jobExecutorNodeAngleFromStart - jobStepNodeAngleFromAssociatedJobExecutorNode;
                            xyzLocations(jobStepNodePlacementRadius, jobStepNodeAngleFromStart, EntityNodes, heightOfHelix, "rgb(20, 163, 24)", 2);
                            //double jobStepNodeX = jobStepNodePlacementRadius * Math.Sin(jobStepNodeAngleFromStart); //extract to method
                            //double jobStepNodeY = jobStepNodePlacementRadius * Math.Cos(jobStepNodeAngleFromStart); //extract to method
                            //double proportionOfWayRoundHelix3 = jobStepNodeAngleFromStart / (2 * Math.PI); //extract to method
                            //double jobStepNodeZ = (proportionOfWayRoundHelix3 * -heightOfHelix) - 100; //extract to method
                            //EntityNodes.Add(new EntityNodeConfiguration { marker = new Marker { color = "rgb(20, 163, 24)", size = 2 }, type = "scatter3d", x = new List<int> { (int)jobStepNodeX }, y = new List<int> { (int)jobStepNodeY }, z = new List<int> { (int)jobStepNodeZ }, /*labels = "Job Steps"*/ }); //extract to method
                            //Below, placing the product nodes on the canvas

                        }
                    }
                }
            }
            return EntityNodes;
        }  
        private void xyzLocations(double nodePlacementRadius, double nodeAngleFromStart, List<EntityNodeConfiguration> nodeList, int heightOfHelix, string nodeColour, int nodeSize)
        {
            double NodeX = nodePlacementRadius * Math.Sin(nodeAngleFromStart);
            double NodeY = nodePlacementRadius * Math.Cos(nodeAngleFromStart);
            double proportionOfWayRoundHelix = nodeAngleFromStart / (2 * Math.PI);
            double NodeZ = (proportionOfWayRoundHelix * -heightOfHelix) - 100;
            nodeList.Add(new EntityNodeConfiguration { marker = new Marker { color = nodeColour, size = nodeSize }, type = "scatter3d", x = new List<int> { (int)NodeX }, y = new List<int> { (int)NodeY }, z = new List<int> { (int)NodeZ } });
        }
    }
}