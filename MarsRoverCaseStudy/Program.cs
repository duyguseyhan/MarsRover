using System;
using System.Collections.Generic;
using System.Deployment.Internal.Isolation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRoverCaseStudy.Models;
using MarsRoverCaseStudy.Services;
using MarsRoverCaseStudy.Utils;

namespace MarsRoverCaseStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            var plateausCoordinatesInput = Console.ReadLine()?.Trim();
            var roverPositionInput = Console.ReadLine()?.Trim();
            var moveCommandInput = Console.ReadLine()?.Trim();
            var secondRoverPositionInput = Console.ReadLine()?.Trim();
            var secondMoveCommandInput = Console.ReadLine()?.Trim();


            IRoverMovement roverMovement = new RoverMovement();

            var moveRoverRequest = new MarsRoverInput
            {
                MoveCommand = moveCommandInput,
                PlateauCoordinates = plateausCoordinatesInput,
                RoverPosition = roverPositionInput
            };

            var roverOutput = roverMovement.MoveRover(moveRoverRequest);

            if (roverOutput.ErrorMessage == EnumHelper.GetSringValue(Error.AllValidationsAreValid))
            {
                var secondMoveRoverRequest = new MarsRoverInput
                {
                    MoveCommand = secondMoveCommandInput,
                    PlateauCoordinates = plateausCoordinatesInput,
                    RoverPosition = secondRoverPositionInput
                };

                var secondRoverOutput = roverMovement.MoveRover(secondMoveRoverRequest);
                if (roverOutput.ErrorMessage == EnumHelper.GetSringValue(Error.AllValidationsAreValid))
                {
                    Console.WriteLine(roverOutput.CoordinateOfX.ToString() + " " + roverOutput.CoordinateOfY.ToString() + " " + EnumHelper.GetSringValue(roverOutput.Direction));
                    Console.WriteLine(secondRoverOutput.CoordinateOfX.ToString() + " " + secondRoverOutput.CoordinateOfY.ToString() + " " + EnumHelper.GetSringValue(secondRoverOutput.Direction));
                }
                else
                {
                    Console.WriteLine(secondRoverOutput.ErrorMessage);
                }
            }
            else
            {
                Console.WriteLine(roverOutput.ErrorMessage);
            }


            Console.ReadLine();
        }

    }
}
