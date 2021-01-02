using Portal.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class Line
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public Line(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    public class StraightConnectorLine
    {
        public StraightConnectorLine()
        {
            Lines = new List<Line>();
        }
        public List<Line> Lines { get; set; }
        public StraightConnectionLineViewModel GetLineViewModel ()
        {
            var model = new StraightConnectionLineViewModel();
            model.x = new List<int>();
            model.y = new List<int>();
            model.z = new List<int>();
            foreach (var line in Lines)
            {
                model.x.Add(line.x);
                model.y.Add(line.y);
                model.z.Add(line.z);
            }
            return model;
        }
    }
    public class StraightConnectionLineViewModel
    {
        public List<int> x { get; set; }
        public List<int> y { get; set; }
        public List<int> z { get; set; }
        public string type { get; set; } = GraphConstants.DEFAULTGRAPH;
    }
}