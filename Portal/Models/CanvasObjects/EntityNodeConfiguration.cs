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
        public double Opacity { get; set; }
    }

    public class EntityNodeConfiguration //need to rethink these to allow for integration of properties specific to the individual node types
    {
        public List<int> x { get; set; }
        public List<int> y { get; set; }
        public List<int> z { get; set; }
        public string Type { get; set; } = GraphConstants.DEFAULTGRAPH;
        public Marker Marker { get; set; }
        public string Labels { get; set; }
        public string Text { get; set; }
        public int[] Coords { get; set; }
        public EntityNodeConfiguration(int[] coords, string color, int size/*, string labels*/, string text, double opacity)
        {
            Coords = coords;
            this.x = new List<int> { coords[0] };
            this.y = new List<int> { coords[1] };
            this.z = new List<int> { coords[2] };
            this.Type = GraphConstants.DEFAULTGRAPH;
            this.Marker = new Marker { Color = color, Size = size, Opacity = opacity };
            //this.Labels = labels;
            this.Text = text;
        }
    } 
}
