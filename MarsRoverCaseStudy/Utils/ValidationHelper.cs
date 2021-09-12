using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MarsRoverCaseStudy.Models;
using MarsRoverCaseStudy.Utils;

namespace MarsRoverCaseStudy.Utils
{
    public class ValidationHelper 
    {
        public static bool CheckCoordinateValidity(string coordinateOfX, string coordinateOfY)
        {
            return int.TryParse(coordinateOfX, out _) && int.TryParse(coordinateOfY, out _);
        }
        public static bool CheckDirectionValidity(string direction)
        {
            return Constants.PossibleDirections.Contains(direction);
        }
        public static bool CheckMoveCommandValidity(string commandValue)
        {
            const string commandRegex = "^[LRM]+$";
            return Regex.IsMatch(commandValue, commandRegex);
        }
        public static bool DoesValueInclueNullOrWhiteSpace(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
