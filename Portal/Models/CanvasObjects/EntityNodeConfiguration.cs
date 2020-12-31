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
        public EntityNodeConfiguration(int x, int y, int z, string color , int size/*, string labels*/, string text) //this needs to go
        { //this needs to go
            this.x = new List<int> { x }; //this needs to go
            this.y = new List<int> { y }; //this needs to go
            this.z = new List<int> { z }; //this needs to go
            this.Type = GraphConstants.DEFAULTGRAPH; //this needs to go
            this.Marker = new Marker { Color = color, Size = size }; //this needs to go
            //this.Labels = labels; //this needs to go
            this.Text = text; //this needs to go
        } //this needs to go

        public EntityNodeConfiguration(int[] coords, string color, int size/*, string labels*/, string text)
        {
            Coords = coords;
            this.x = new List<int> { coords[0] };
            this.y = new List<int> { coords[1] };
            this.z = new List<int> { coords[2] };
            this.Type = GraphConstants.DEFAULTGRAPH;
            this.Marker = new Marker { Color = color, Size = size };
            //this.Labels = labels;
            this.Text = text;
        }
    } 
}
