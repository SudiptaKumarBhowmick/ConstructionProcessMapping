using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class Marker //how do we turn this into an object?
    {
        public string color { get; set; }
        public int size { get; set; }
    }

    public class EntityNodeConfiguration
    {
        public List<int> x { get; set; } //Why did we make these lists?
        public List<int> y { get; set; }
        public List<int> z { get; set; }
        public string type { get; set; } //The type will always be "scatter3d"
        public Marker marker { get; set; }
        public string labels { get; set; }

        //public EntityNodeConfiguration(List<int> x, List<int> y, List<int> z, Marker marker, string labels)
        //{
        //    X = x;
        //    Y = y;
        //    Z = z;
        //    Type = "scatter3d";
        //    Marker = marker;
        //    Labels = labels;
        //}
    } 
}
