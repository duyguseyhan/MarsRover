using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MarsRoverCaseStudy.Models;
using MarsRoverCaseStudy.Services;
using MarsRoverCaseStudy.Utils;

namespace MarsRoverCaseStudy.Test
{
    [TestClass]
    public class MarsRoverTest
    {
        private IRoverMovement roverMovement;

        public MarsRoverTest()
        {
            roverMovement = new RoverMovement();
        }

        [TestMethod]
        public void MoveRover_Input_12N_LMLMLMLMM_Output_13N()
        {
            var marsRoverInput = new MarsRoverInput()
            {
                PlateauCoordinates = "5 5",
                MoveCommand = "LMLMLMLMM",
                RoverPosition = "1 2 N"
            };

            var expectedResult = "1 3 N";

            var newPosition = roverMovement.MoveRover(marsRoverInput);
            var result = newPosition.CoordinateOfX + " " + newPosition.CoordinateOfY + " " +
                         EnumHelper.GetSringValue(newPosition.Direction);
            Assert.IsTrue(result == expectedResult);
        }

        [TestMethod]
        public void MoveRover_Input_33E_MMRMMRMRRM_Output_51E()
        {
            var marsRoverInput = new MarsRoverInput()
            {
                PlateauCoordinates = "5 5",
                MoveCommand = "MMRMMRMRRM",
                RoverPosition = "3 3 E"
            };

            var expectedResult = "5 1 E";

            var newPosition = roverMovement.MoveRover(marsRoverInput);
            var result = newPosition.CoordinateOfX + " " + newPosition.CoordinateOfY + " " +
                         EnumHelper.GetSringValue(newPosition.Direction);
            Assert.IsTrue(result == expectedResult);
        }
        [TestMethod]
        public void MoveRover_OutOfPlateauAreaMoveCommand_RoverWentOutOfPlateauArea()
        {
            var marsRoverInput = new MarsRoverInput()
            {
                PlateauCoordinates = "5 5",
                MoveCommand = "MMMRMMMMM",
                RoverPosition = "3 3 E"
            };

            var expectedResult = EnumHelper.GetSringValue(Error.RoverWentOutOfPlateauArea);

            var result = roverMovement.MoveRover(marsRoverInput).ErrorMessage;

            Assert.IsTrue(result == expectedResult);

        }
        [TestMethod]
        public void MoveRover_CorrectInputs_AllValidationsAreValid()
        {
            var marsRoverInput = new MarsRoverInput()
            {
                PlateauCoordinates = "5 5",
                MoveCommand = "MMRMMRMRRM",
                RoverPosition = "3 3 E"
            };

            var expectedResult = EnumHelper.GetSringValue(Error.AllValidationsAreValid);

            var result = roverMovement.MoveRover(marsRoverInput).ErrorMessage;

            Assert.IsTrue(result == expectedResult);

        }
        
        [TestMethod]
        public void MoveRover_OutOfPlateauAreaRoverPosition_RoverPositionIsNotWithinPlateauArea()
        {
            var marsRoverInput = new MarsRoverInput()
            {
                PlateauCoordinates = "5 5",
                MoveCommand = "MMRMMRMRRM",
                RoverPosition = "7 3 E"
            };

            var expectedResult = EnumHelper.GetSringValue(Error.RoverPositionIsNotWithinPlateauArea);

            var result = roverMovement.MoveRover(marsRoverInput).ErrorMessage;

            Assert.IsTrue(result == expectedResult);

        }
        [TestMethod]
        public void MoveRover_EmptyPlateauCoordinates_InvalidOperationForPlateau()
        {
            var marsRoverInput = new MarsRoverInput()
            {
                PlateauCoordinates = String.Empty,
                MoveCommand = "MMRMMRMRRM",
                RoverPosition = "3 3 E"
            };

            var expectedResult = EnumHelper.GetSringValue(Error.InvalidOperationForPlateau);

            var result = roverMovement.MoveRover(marsRoverInput).ErrorMessage;

            Assert.IsTrue(result == expectedResult);

        }

        [TestMethod]
        public void MoveRover_InvalidRoverPosition_InvalidOperationForRoverPosition()
        {
            var marsRoverInput = new MarsRoverInput()
            {
                PlateauCoordinates = "5 5",
                MoveCommand = "MMRMMRMRRM",
                RoverPosition = "3 3E"
            };

            var expectedResult = EnumHelper.GetSringValue(Error.InvalidOperationForRoverPosition);

            var result = roverMovement.MoveRover(marsRoverInput).ErrorMessage;

            Assert.IsTrue(result == expectedResult);
        }

        [TestMethod]
        public void MoveRover_WrongMoveCommand_InvalidOperationForRoverMovement()
        {
            var marsRoverInput = new MarsRoverInput()
            {
                PlateauCoordinates = "5 5",
                MoveCommand = "LMLMXLMLMM",
                RoverPosition = "1 2 N"
            };

            var expectedResult = EnumHelper.GetSringValue(Error.InvalidOperationForRoverMovement);

            var result = roverMovement.MoveRover(marsRoverInput).ErrorMessage;

            Assert.IsTrue(result == expectedResult);
        }

        [TestMethod]
        public void MoveRover_WrongRoverPositionDirection_InvalidOperationForRoverDirection()
        {
            var marsRoverInput = new MarsRoverInput()
            {
                PlateauCoordinates = "5 5",
                MoveCommand = "LMLMLMLMM",
                RoverPosition = "1 2 M"
            };

            var expectedResult = EnumHelper.GetSringValue(Error.InvalidOperationForRoverDirection);

            var result = roverMovement.MoveRover(marsRoverInput).ErrorMessage;
            
            Assert.IsTrue(result == expectedResult);
        }

        [TestMethod]
        public void MoveRover_PlateauMissingYCoordinate_InvalidOperationForPlateau()
        {
            var marsRoverInput = new MarsRoverInput()
            {
                PlateauCoordinates = "5",
                MoveCommand = "LMLMLMLMM",
                RoverPosition = "1 2 N"
            };

            var expectedResult = EnumHelper.GetSringValue(Error.InvalidOperationForPlateau);

            var result = roverMovement.MoveRover(marsRoverInput).ErrorMessage;

            Assert.IsTrue(result == expectedResult);
        }
    }
}
