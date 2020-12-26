using Portal.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class Marker
    {
        public string Color { get; set; }
        public int Size { get; set; }
    }

    public class EntityNodeConfiguration
    {
        public List<int> x { get; set; } //Why did we make these lists?
        public List<int> y { get; set; }
        public List<int> z { get; set; }
        public string Type { get; set; } = GraphConstants.DEFAULTGRAPH;
        public Marker Marker { get; set; }
        public string Labels { get; set; }

        public EntityNodeConfiguration(int x, int y, int z, string color , int size/*, string labels*/)
        {
            this.x = new List<int> { x };
            this.y = new List<int> { y };
            this.z = new List<int> { z };
            this.Type = GraphConstants.DEFAULTGRAPH;
            this.Marker = new Marker { Color = color, Size = size };
            //this.Labels = labels;
        }
    } 
}
