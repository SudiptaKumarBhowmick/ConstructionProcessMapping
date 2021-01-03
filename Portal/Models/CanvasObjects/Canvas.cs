using Portal.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class Canvas
    {
        public int InnerRadius { get; set; }
        public int OuterRadius { get; set; }
        public int HeightOfHelix { get; set; }
        public string CanvasType { get; set; } = GraphConstants.DEFAULTGRAPH;

        public Canvas(int innerRadius, int outerRadius, int heightOfHelix)
        {
            InnerRadius = innerRadius;
            OuterRadius = outerRadius;
            HeightOfHelix = heightOfHelix;
        }

        public Canvas()
        {
            InnerRadius = 9000;
            OuterRadius = 85720;
            HeightOfHelix = 32000;
        }
    }
}
