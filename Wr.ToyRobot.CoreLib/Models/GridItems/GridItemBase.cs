using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Wr.ToyRobot.CoreLib.Helpers;

namespace Wr.ToyRobot.CoreLib.Models.GridItems
{
    /// <summary>
    /// Abstract base class of a Grid Item
    /// </summary>
    public abstract partial class GridItemBase : IGridItem
    {
        /// <summary>
        /// A reference to the TaskGrid parent object
        /// </summary>
        protected ITaskGrid _taskGrid;

        /// <summary>
        /// Delegate for Do Commands
        /// </summary>
        /// <param name="parseCommandResult"></param>
        /// <returns></returns>
        protected delegate GenericResult DelegateDoCommand(ParseCommandResult parseCommandResult);

        /// <summary>
        /// Sets the GridItem 'Type' Name
        /// </summary>
        public abstract string GRID_ITEM_TYPE_NAME { get; }

        /// <summary>
        /// List of all valid change of direction commands
        /// </summary>
        protected List<string> VALID_CHANGE_DIRECTION_COMMANDS = new List<string>() { "LEFT", "RIGHT" };

        /// <summary>
        /// Privately set property for Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// List of the history of all previous valid states. The last item in the list is the current state.
        /// </summary>
        public List<GridItemState> _validStateHistory { get; set; }

        /// <summary>
        /// Holds the current state during the RunCommand methods
        /// </summary>
        private GridItemState _currentState { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="taskGrid">A reference to the TaskGrid parent object</param>
        /// <param name="name">The identifier/name of the grid item</param>
        public GridItemBase(ITaskGrid taskGrid, string name)
        {
            _taskGrid = taskGrid;
            Name = name;
            _validStateHistory = new List<GridItemState>();
        }

        /// <summary>
        /// Run/process the command
        /// </summary>
        /// <param name="command"></param>
        /// <returns>GenericResult</returns>
        public virtual GenericResult RunCommand(string command)
        {
            GenericResult result = new GenericResult();

            // parse the command
            var parseResult = ParseCommand(command);

            if (!parseResult.Success)
            {
                result.Comment = parseResult.Comment;
                return result;
            }

            var canRunCommand = CheckRunCommandRules(parseResult.Command);

            if (!canRunCommand.Success) // Command can't be run, due to a rule
            {
                result.Comment = canRunCommand.Comment;
                return result;
            }

            _currentState = GetCurrentState();
            
            DelegateDoCommand _delegateDoCommand = null;

            switch (parseResult.Command)
            {
                case CommandType.PLACE:
                    _delegateDoCommand = DoPlace;
                    break;
                case CommandType.MOVE:
                    _delegateDoCommand = DoMove;
                    break;
                case CommandType.LEFT:
                    _delegateDoCommand = DoLeft;
                    break;
                case CommandType.RIGHT:
                    _delegateDoCommand = DoRight;
                    break;
                case CommandType.REPORT:
                    _delegateDoCommand = DoReport;
                    break;
                default:
                    result.Comment = "Not a valid command. Please try again.";
                    return result;
            }

            if (_delegateDoCommand != null)
            {
                var doResult = _delegateDoCommand(parseResult);
                result = doResult;
            }
            else
            {
                result.Comment = "Unable to run the command. Please try again.";
            }

            return result;
        }

        /// <summary>
        /// Run the Place command. Can be overridden.
        /// </summary>
        /// <param name="parseCommandResult"></param>
        /// <returns>CommandResult</returns>
        protected virtual GenericResult DoPlace(ParseCommandResult parseCommandResult)
        {
            GenericResult result = new GenericResult();

            // Check if the new coords are within the boundary of the grid.
            if (_taskGrid.IsInGridBounds(parseCommandResult.Coordinates))
            {
                var newState = new GridItemState() { Coords = parseCommandResult.Coordinates, Facing = parseCommandResult.FacingDirection };
                AddItemStateToHistory(newState);
                result.Success = true;
            }
            else
            {
                result.Comment = $"That command would position the {GRID_ITEM_TYPE_NAME} off the grid. Try again.";
            }

            return result;
        }

        /// <summary>
        /// Run the Move command. Can be overridden.
        /// </summary>
        /// <returns>CommandResult</returns>
        protected virtual GenericResult DoMove(ParseCommandResult parseCommandResult)
        {
            GenericResult result = new GenericResult();

            // Get direction of item, and how to calculate the move.
            var newCoordsForMove = _currentState.GetCoordinatesForNextMove();

            // check if the new coords are within the boundary of the grid.
            if (_taskGrid.IsInGridBounds(newCoordsForMove))
            {
                var newState = CloneGridItem(_currentState);
                newState.Coords = newCoordsForMove;

                AddItemStateToHistory(newState);
                result.Success = true;
            }
            else
            {
                result.Comment = $"That move would position the {GRID_ITEM_TYPE_NAME} off the grid. Try another command. Current position: {_currentState.ToString()}";
            }

            return result;
        }

        /// <summary>
        /// Run the Left command. Can be overridden.
        /// </summary>
        /// <param name="parseCommandResult"></param>
        /// <returns>CommandResult</returns>
        protected virtual GenericResult DoLeft(ParseCommandResult parseCommandResult)
        {
            GenericResult result = new GenericResult();

            var newDirection = RotateDirection(-90);

            var newState = CloneGridItem(_currentState);
            newState.Facing = newDirection;

            AddItemStateToHistory(newState);

            result.Success = true;
            return result;
        }

        /// <summary>
        /// Run the Right command. Can be overridden.
        /// </summary>
        /// <param name="parseCommandResult"></param>
        /// <returns>CommandResult</returns>
        protected virtual GenericResult DoRight(ParseCommandResult parseCommandResult)
        {
            GenericResult result = new GenericResult();

            var newDirection = RotateDirection(90);

            var newState = CloneGridItem(_currentState);
            newState.Facing = newDirection;

            AddItemStateToHistory(newState);

            result.Success = true;
            return result;
        }

        /// <summary>
        /// Run the Report command. Can be overridden.
        /// </summary>
        /// <returns>CommandResult</returns>
        protected virtual GenericResult DoReport(ParseCommandResult parseCommandResult)
        {
            GenericResult result = new GenericResult();

            result.Output = _currentState.ToString();
            result.Success = true;
            return result;
        }

        /// <summary>
        /// Parse the command string to ParseCommandResult
        /// </summary>
        /// <param name="command"></param>
        /// <returns>ParseCommandResult</returns>
        protected ParseCommandResult ParseCommand(string command)
        {
            ParseCommandResult result = new ParseCommandResult();

            // Regex match for at least 1 whole word. Group the first word, and the rest (if there is any other text) in another group.
            Match match = Regex.Match(command.ToUpper(), @"([A-Z]+)(.*)");
            if (match.Success)
            {
                // The first group is the base command.
                var commandWord = match.Groups[1].Value;
                
                // Any parameters will be in the 2nd group, if present.
                var parameters = string.Empty;
                if (match.Groups.Count == 3)
                {
                    parameters = match.Groups[2].Value;
                }

                if (Enum.TryParse(commandWord, out CommandType thisCommand))
                {
                    // Command found.
                    result.Command = thisCommand;

                    switch (thisCommand)
                    {
                        case CommandType.PLACE:
                            // Regex match for '[digit],[digit],[WHOLEWORD]' i.e. 1,2, NORTH - ignoring any white spaces.
                            Match matchParams = Regex.Match(parameters, @"\W+(\d+)\W*,\W*(\d+)\W*,\W*([A-Z]+)\w*$");
                            if (matchParams.Success)
                            {
                                if (matchParams.Groups.Count == 4)
                                {
                                    var x = Convert.ToInt32(matchParams.Groups[1].Value);
                                    var y = Convert.ToInt32(matchParams.Groups[2].Value);
                                    var direction = matchParams.Groups[3].Value;

                                    // Try and parse the direction to FacingDirection enum
                                    if (Enum.TryParse(direction, out FacingDirection facingDirection))
                                    {
                                        result.Coordinates = new Coordinates(x, y);
                                        result.FacingDirection = facingDirection;
                                        result.Success = true;
                                    }
                                }
                            }
                            break;

                        default:
                            // Check if any additioanl parameters have been given.
                            // Note: We could decide to ignore these if we wanted to.
                            if (string.IsNullOrEmpty(parameters))
                            {
                                result.Success = true;
                                
                            }
                            else
                            {
                                result.Comment = $"The {thisCommand.ToString()} command does not have any parameters. Please try again.";
                                return result;
                            }
                            break;
                    }
                }
                else
                {
                    result.Comment = "No valid command received";
                }
            }
            else
            {
                result.Comment = "No commands found. Please try again.";
            }

            return result;
        }

        /// <summary>
        /// Calculate the new direction given the passed-in degrees
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        private FacingDirection RotateDirection(int degrees)
        {
            var currentOrientationInDegrees = (int)_currentState.Facing;
            var newOrientationInDegrees = RotateByDegrees(currentOrientationInDegrees, degrees);

            // Try and convert int degrees to FacingDirection.
            try
            {
                var sucessfulResult = (FacingDirection)newOrientationInDegrees;
                return sucessfulResult;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Rotate an exisiting orientation.
        /// Method based on Stackoverflow answer <see href="https://stackoverflow.com/questions/1628386/normalise-orientation-between-0-and-360#answer-1628401">HERE</see>
        /// </summary>
        /// <param name="currentOrientationInDegrees">The current orientation/direction</param>
        /// <param name="degrees"></param>
        /// <returns>Updated Orientation In Degrees</returns>
        private int RotateByDegrees(int currentOrientationInDegrees, int degrees)
        {
            currentOrientationInDegrees = (currentOrientationInDegrees + degrees) % 360;
            if (currentOrientationInDegrees < 0) currentOrientationInDegrees += 360;

            return currentOrientationInDegrees;
        }

        /// <summary>
        /// Gets the last item in the state history list
        /// </summary>
        /// <returns></returns>
        public GridItemState GetCurrentState()
        {
            return _validStateHistory.LastOrDefault();
        }

        /// <summary>
        /// Deep clone the GridItemState
        /// </summary>
        /// <param name="gridItem"></param>
        /// <returns>A deep clone of the item</returns>
        private GridItemState CloneGridItem(GridItemState gridItem)
        {
            return (GridItemState)gridItem.Clone();
        }

        /// <summary>
        /// Add itemState to the state history list at the end 
        /// </summary>
        /// <param name="itemState"></param>
        private void AddItemStateToHistory(GridItemState itemState)
        {
            _validStateHistory.Add(itemState);
        }

        /// <summary>
        /// Check if a command can be run. Can be overidden
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns>bool</returns>
        protected virtual GenericResult CheckRunCommandRules(CommandType commandType)
        {
            GenericResult result = new GenericResult();
            switch (commandType)
            {
                case CommandType.PLACE:
                    result.Success = true;
                    break;

                default: // All other commands

                    if (_validStateHistory.Count > 0) // Only allow other commands if there is at least one valid command in the state history list
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Comment = "Can't run that command yet. You must first use the 'Place' command.";
                    }

                    break;
            }

            return result;
        }

    }
}
