using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mousse.ViewModel
{
    public class CustomEllipse
    {
        public int Radius { get; } = 50;
        public string Fill { get; set; }
        public double XPos { get; set; }
        public double YPos { get; set; }
        public int NodeNumber { get; set; }
        public List<int> SmezhLines { get; set; } = new List<int>(); // тут хранятся все линии, даже для неориентированных графов
        public Dictionary<int, int> SmezhNodes { get; set; } = new Dictionary<int, int>();
    }
}
