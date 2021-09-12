using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverCaseStudy.Models
{
    public class ValidationResult
    {
        public RoverPosition RoverPosition { get; set; }
        public PlateauCoordinates PlateauCoordinates { get; set; }
        public string MoveCommand { get; set; }
        public string ErrorMessages { get; set; }
    }
}
