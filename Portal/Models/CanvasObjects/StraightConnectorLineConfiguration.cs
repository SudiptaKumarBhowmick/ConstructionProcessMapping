using Portal.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class LineProperties
    {
        public string Color { get; set; }
        public int Width { get; set; }
    }
    public class StraightConnectorLineConfiguration //RelationshipArrowConfiguration
    {
        public string Type { get; set; }
        public string Mode { get; set; }
        public List<int> x { get; set; }
        public List<int> y { get; set; }
        public List<int> z { get; set; }
        public List<int> x1 { get; set; }
        public List<int> y1 { get; set; }
        public List<int> z1 { get; set; }
        public int[] xCoords { get; set; }
        public int[] yCoords { get; set; }
        public int[] zCoords { get; set; }
        public LineProperties LineProperties { get; set; }
        public StraightConnectorLineConfiguration(int[] xcoords, int[] ycoords, int[] zcoords/*, string color, int width*/)
        {
            Type = GraphConstants.DEFAULTGRAPH;
            //Mode = "lines";
            xCoords = xcoords;
            this.x = new List<int> { xcoords[0] };
            this.x1 = new List<int> { xcoords[1] };
            yCoords = ycoords;
            this.y = new List<int> { ycoords[0] };
            this.y1 = new List<int> { ycoords[1] };
            zCoords = zcoords;
            this.z = new List<int> { zcoords[0] };
            this.z1 = new List<int> { zcoords[1] };
            //LineProperties = new LineProperties { Color = color, Width = width };
        }
    }
}