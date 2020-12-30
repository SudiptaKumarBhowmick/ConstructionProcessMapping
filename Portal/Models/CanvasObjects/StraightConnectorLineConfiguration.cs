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
        public List<int> X0 { get; set; }
        public List<int> Y0 { get; set; }
        public List<int> Z0 { get; set; }
        public List<int> X1 { get; set; }
        public List<int> Y1 { get; set; }
        public List<int> Z1 { get; set; }
        public LineProperties LineProperties { get; set; }
        public StraightConnectorLineConfiguration(int x0, int y0, int z0, int x1, int y1, int z1, string color, int width)
        {
            //Type = "line";
            X0 = new List<int> { x0 };
            Y0 = new List<int> { y0 };
            Z0 = new List<int> { z0 };
            X1 = new List<int> { x1 };
            Y1 = new List<int> { y1 };
            Z1 = new List<int> { z1 };
            LineProperties = new LineProperties { Color = color, Width = width };
        }
    }
}