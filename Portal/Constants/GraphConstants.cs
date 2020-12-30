using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Constants
{
    public class GraphConstants
    {
        public const string DEFAULTGRAPH = "scatter3d";
        public const string DEFAULTLINE = "line";

        public const int OWNERNODESIZE = 15; //this is not really a constant, it should be dependant on market metrics
        public const string OWNERCOLOUR = "rgb(66, 135, 245)"; //this is not really a constant, it should be dependant on market metrics - or maybe it should only be the opacity that varies?
        public const string OWNERLABEL = "Organisation";

        public const int ORGANISATIONNODESIZE = 9; //this is not really a constant, it should be dependant on market metrics
        public const string ORGANISATIONCOLOUR = "rgb(168, 29, 242)"; //this is not really a constant, it should be dependant on market metrics - or maybe it should only be the opacity that varies?
        public const string ORGANISATIONLABEL = "Organisation";

        public const int JOBEXECUTORNODESIZE = 6; //this is not really a constant, it should be dependant on market metrics
        public const string JOBEXECUTORCOLOUR = "rgb(240, 236, 41)"; //this is not really a constant, it should be dependant on market metrics - or maybe it should only be the opacity that varies?
        public const string JOBEXECUTORLABEL = "Job Executor";

        public const int JOBSTEPNODESIZE = 2; //this is not really a constant, it should be dependant on market metrics
        public const string JOBSTEPCOLOUR = "rgb(20, 163, 24)"; //this is not really a constant, it should be dependant on market metrics - or maybe it should only be the opacity that varies?
        public const string JOBSTEPLABEL = "Job Step";

        public const int CUSTOMPRODUCTNODESIZE = 2; //this is not really a constant, it should be dependant on market metrics
        public const string CUSTOMPRODUCTCOLOUR = "rgb(232, 87, 58)"; //this is not really a constant, it should be dependant on market metrics - or maybe it should only be the opacity that varies?
        public const string CUSTOMINPUTLABEL = "Custom Input";
        public const string CUSTOMOUTPUTLABEL = "Custom Output";

        public const int GENERICPRODUCTNODESIZE = 1; //this is not really a constant, it should be dependant on market metrics
        public const string GENERICPRODUCTCOLOUR = "rgb(232, 87, 58)"; //this is not really a constant, it should be dependant on market metrics - or maybe it should only be the opacity that varies?
        public const string GENERICPRODUCTLABEL = "Generic Output"; 


    }
}
