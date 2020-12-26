using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class LineProperties
    {
        public string color { get; set; }
        public int width { get; set; }
    }
    public class RelationshipArrowConfiguration
    {
        public string type { get; set; }
        public string path { get; set; }
        public LineProperties lineProperties { get; set; }
        //path=" M 3,7 L2,8 L2,9 L3,10, L4,10 L5,9 L5,8 L4,7 Z",
        //    fillcolor="PaleTurquoise",
        //    line_color="LightSeaGreen",
    }
}
