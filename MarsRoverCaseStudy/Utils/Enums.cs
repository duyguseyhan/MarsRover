using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverCaseStudy.Utils
{
    public class StringValue : System.Attribute
    {
        public string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value => _value;
    }
    public enum Error
    {
        [StringValue("You entered invalid value for coordinates of the plateau")]
        InvalidOperationForPlateau = 1,

        [StringValue("You entered invalid value for rover's position")]
        InvalidOperationForRoverPosition = 2,

        [StringValue("You entered invalid value for direction of the rover")]
        InvalidOperationForRoverDirection = 3,

        [StringValue("You entered invalid value for rover's movement")]
        InvalidOperationForRoverMovement = 4,

        [StringValue("You entered rover position outside plateau area")]
        RoverPositionIsNotWithinPlateauArea = 5,

        [StringValue("Rover went out of plateau area")]
        RoverWentOutOfPlateauArea = 6,

        [StringValue("")]
        AllValidationsAreValid = 7,

    }

    public enum Direction
    {
        [StringValue("E")]
        E,
        [StringValue("W")]
        W,
        [StringValue("N")]
        N,
        [StringValue("S")]
        S
    }
    
}
