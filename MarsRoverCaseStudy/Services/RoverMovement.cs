using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRoverCaseStudy.Models;
using MarsRoverCaseStudy.Utils;

namespace MarsRoverCaseStudy.Services
{
    public class RoverMovement : IRoverMovement, IValidation
    {
        MarsRoverOutput _marsRoverOutput;
        public RoverMovement()
        {
            _marsRoverOutput = new MarsRoverOutput();
        }
        public virtual RoverPosition MoveRover(MarsRoverInput input)
        {
            var resultValidity = CheckValidity(input);

            if (resultValidity.ErrorMessages == EnumHelper.GetSringValue(Error.AllValidationsAreValid))
            {
                _marsRoverOutput.RoverPosition = resultValidity.RoverPosition;

                foreach (var move in resultValidity.MoveCommand)
                {
                    switch (move)
                    {
                        case 'L':
                            TurnLeft();
                            break;
                        case 'R':
                            TurnRight();
                            break;
                        default:
                            MoveForward();
                            break;
                    }

                    if (_marsRoverOutput.RoverPosition.CoordinateOfX < 0 || _marsRoverOutput.RoverPosition.CoordinateOfX > resultValidity.PlateauCoordinates.CoordinateOfX || _marsRoverOutput.RoverPosition.CoordinateOfY < 0 || _marsRoverOutput.RoverPosition.CoordinateOfY > resultValidity.PlateauCoordinates.CoordinateOfY)
                    {
                        return new RoverPosition
                        {
                            ErrorMessage = EnumHelper.GetSringValue(Error.RoverWentOutOfPlateauArea)
                        };
                    }
                }

            }
            else
            {
                return new RoverPosition
                {
                    ErrorMessage = resultValidity.ErrorMessages
                };
            }

            return new RoverPosition
            {
                CoordinateOfX = _marsRoverOutput.RoverPosition.CoordinateOfX,
                CoordinateOfY = _marsRoverOutput.RoverPosition.CoordinateOfY,
                Direction = _marsRoverOutput.RoverPosition.Direction,
                ErrorMessage = EnumHelper.GetSringValue(Error.AllValidationsAreValid)
            };
        }

        private void MoveForward()
        {
            switch (_marsRoverOutput.RoverPosition.Direction)
            {
                case Direction.E:
                    _marsRoverOutput.RoverPosition.CoordinateOfX++;
                    break;
                case Direction.W:
                    _marsRoverOutput.RoverPosition.CoordinateOfX--;
                    break;
                case Direction.N:
                    _marsRoverOutput.RoverPosition.CoordinateOfY++;
                    break;
                case Direction.S:
                    _marsRoverOutput.RoverPosition.CoordinateOfY--;
                    break;
                default:
                    break;
            }
        }

        private void TurnRight()
        {
            switch (_marsRoverOutput.RoverPosition.Direction)
            {
                case Direction.E:
                    _marsRoverOutput.RoverPosition.Direction = Direction.S;
                    break;
                case Direction.W:
                    _marsRoverOutput.RoverPosition.Direction = Direction.N;
                    break;
                case Direction.N:
                    _marsRoverOutput.RoverPosition.Direction = Direction.E;
                    break;
                case Direction.S:
                    _marsRoverOutput.RoverPosition.Direction = Direction.W;
                    break;
                default:
                    break;
            }
        }

        private void TurnLeft()
        {
            switch (_marsRoverOutput.RoverPosition.Direction)
            {
                case Direction.E:
                    _marsRoverOutput.RoverPosition.Direction = Direction.N;
                    break;
                case Direction.W:
                    _marsRoverOutput.RoverPosition.Direction = Direction.S;
                    break;
                case Direction.N:
                    _marsRoverOutput.RoverPosition.Direction = Direction.W;
                    break;
                case Direction.S:
                    _marsRoverOutput.RoverPosition.Direction = Direction.E;
                    break;
                default:
                    break;
            }
        }

        public ValidationResult CheckValidity(MarsRoverInput input)
        {
            int coordinatOfXPlateau, coordinateOfYPlateau;
            int coordinatOfXRover, coordinatOfYRover;
            Direction direction;

            if (input.PlateauCoordinates.Contains(' ') && input.PlateauCoordinates.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Count() == 2)
            {
                var plateauCoordinateValues = input.PlateauCoordinates.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (!ValidationHelper.CheckCoordinateValidity(plateauCoordinateValues[0], plateauCoordinateValues[1]))
                {
                    return new ValidationResult
                    {
                        ErrorMessages = EnumHelper.GetSringValue(Error.InvalidOperationForPlateau)
                    };

                }
                else
                {
                    coordinatOfXPlateau = int.Parse(plateauCoordinateValues[0]);
                    coordinateOfYPlateau = int.Parse(plateauCoordinateValues[1]);
                }
            }
            else
            {
                return new ValidationResult
                {
                    ErrorMessages = EnumHelper.GetSringValue(Error.InvalidOperationForPlateau)
                };
            }

            if (input.RoverPosition.Contains(' ') && input.RoverPosition.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Count() == 3)
            {
                var roverPositionValues = input.RoverPosition.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (!ValidationHelper.CheckCoordinateValidity(roverPositionValues[0], roverPositionValues[1]))
                {
                    return new ValidationResult
                    {
                        ErrorMessages = EnumHelper.GetSringValue(Error.InvalidOperationForRoverPosition)
                    };
                }
                else
                {
                    coordinatOfXRover = int.Parse(roverPositionValues[0]);
                    coordinatOfYRover = int.Parse(roverPositionValues[1]);
                }

                if (coordinatOfXRover > coordinatOfXPlateau || coordinatOfXRover < 0 || coordinatOfYRover > coordinateOfYPlateau || coordinatOfYRover < 0)
                {
                    return new ValidationResult
                    {
                        ErrorMessages = EnumHelper.GetSringValue(Error.RoverPositionIsNotWithinPlateauArea)
                    };
                }
                else if (roverPositionValues[2].Length > 1 || !ValidationHelper.CheckDirectionValidity(roverPositionValues[2]))
                {
                    return new ValidationResult
                    {
                        ErrorMessages = EnumHelper.GetSringValue(Error.InvalidOperationForRoverDirection)
                    };
                }

                var tryParse = (Enum.TryParse(roverPositionValues[2], out direction));
            }
            else
            {
                return new ValidationResult
                {
                    ErrorMessages = EnumHelper.GetSringValue(Error.InvalidOperationForRoverPosition)
                };
            }

            if (input.MoveCommand.Contains(' ') || !ValidationHelper.CheckMoveCommandValidity(input.MoveCommand))
            {
                return new ValidationResult
                {
                    ErrorMessages = EnumHelper.GetSringValue(Error.InvalidOperationForRoverMovement)
                };

            }

            return new ValidationResult
            {
                ErrorMessages = EnumHelper.GetSringValue(Error.AllValidationsAreValid),
                MoveCommand = input.MoveCommand,
                PlateauCoordinates = new PlateauCoordinates
                {
                    CoordinateOfX = coordinatOfXPlateau,
                    CoordinateOfY = coordinateOfYPlateau
                },
                RoverPosition = new RoverPosition
                {
                    CoordinateOfX = coordinatOfXRover,
                    CoordinateOfY = coordinatOfYRover,
                    Direction = direction
                }
            };
        }
    }
}
