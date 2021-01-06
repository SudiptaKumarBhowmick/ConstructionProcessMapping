using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class MarkerVisualDepiction // needs to interface with 'EntityNodeConfiguration'
    {
        public double JobStepNodeSize(double selectedSizingProperty, double valueOfSelectedNodeSizeForCurrent, double lowestValueOfSelectProperty, double highestValueOfSelectproperty) //or default (all nodes sized by type)
        {
            //might need to need to know the total node count here - if clash prevention is important at this level
            //we will certainly need to know the highest and lowest values of the select metric
            int minimumNodeSize = 1;
            int maximumNodeSize = 10;
            double currentNodeSize = (maximumNodeSize-minimumNodeSize)/(highestValueOfSelectproperty-lowestValueOfSelectProperty)*valueOfSelectedNodeSizeForCurrent;
            return currentNodeSize;
        }
        public double JobStepNodeOpacity(double selectedOpacityControlProperty, double valueOfSelectedNodeSizeForCurrent, double lowestValueOfSelectProperty, double highestValueOfSelectproperty) //or default (all nodes opacity of 1) //opacity might be better as depth of colour, tbc
        {
            //might need to need to know the total node count here - if clash prevention is important at this level
            //we will certainly need to know the highest and lowest values of the select metric
            double currentNodeOpacity = 1 / (highestValueOfSelectproperty - lowestValueOfSelectProperty) * valueOfSelectedNodeSizeForCurrent;
            return currentNodeOpacity;
        }

        public string TagWhichSetsColour(string selectedTagGroup) //or default
        {
            //the entity type will be set as the default here
            //other tags will be added at UI level
            //there is challenge here in that some tags will represent segments of a whole, whereas some will only represent subsets, with no equivelant for other subsets
            //in this case, all other nodes will be set to a default, perhaps grey, colour
            //these tags then become the 'traces' to be switched on and off as required
            return "";
        }
    }
}
