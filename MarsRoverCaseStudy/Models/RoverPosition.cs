using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRoverCaseStudy.Utils;

namespace MarsRoverCaseStudy.Models
{
    public class RoverPosition
    {
        public int CoordinateOfX { get; set; }
        public int CoordinateOfY { get; set; }
        public Direction Direction { get; set; }

        public string ErrorMessage { get; set; }
    }
}
