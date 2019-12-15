using System;
using System.Collections.Generic;
using System.Linq;
using Wr.ToyRobot.CoreLib.Models;
using Wr.ToyRobot.CoreLib.Models.GridItems;

namespace Wr.ToyRobot.CoreLib
{
    /// <summary>
    /// The main task object
    /// </summary>
    public sealed class TaskGrid : ITaskGrid
    {
        /// <summary>
        /// The size of the grid
        /// </summary>
        public Coordinates GridSize { get; private set; }

        /// <summary>
        /// List of grid items present on the grid
        /// </summary>
        public List<IGridItem> GridItems { get; private set; }
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        public TaskGrid(int xSize = 5, int ySize = 5)
        {
            GridSize = new Coordinates(xSize, ySize);
            GridItems = new List<IGridItem>();
        }

        /// <summary>
        /// Accepts all commands for any grid item
        /// </summary>
        /// <param name="gridItemName">The unique name of the grid item</param>
        /// <param name="command">The command string</param>
        /// <returns>GenericResult</returns>
        public GenericResult RunCommand(string gridItemName, string command)
        {
            GenericResult result = new GenericResult();

            if (string.IsNullOrEmpty(command))
            {
                result.Comment = "No command received. Please try again.";
                return result;
            }

            // Try and find the griditem by name
            var foundGridItem = GetGridItem(gridItemName);
            if (foundGridItem == null) // not found
            {
                result.Comment = $"Grid Item called: '{gridItemName}' does not exists.";
                return result;
            }

            result = foundGridItem.RunCommand(command);

            return result;
        }

  
        /// <summary>
        /// Add a newGridItem to the GridItems list, but making sure it doesn't exist yet
        /// </summary>
        /// <param name="name">The name/identifier of the grid item</param>
        /// <param name="itemTypeName">The name of the item type, which corresponds to the GRID_ITEM_TYPE_NAME property i.e. Robot</param>
        /// <returns></returns>
        public GenericResult AddGridItem(string name, string itemTypeName = "Robot")
        {
            GenericResult result = new GenericResult();

            if (string.IsNullOrEmpty(name))
            {
                result.Comment = "No Grid Item name specified.";
                return result;
            }

            var foundGridItem = GetGridItem(name);
            if (foundGridItem != null)
            {
                result.Comment = "Grid item already exists!";
                return result;
            }

            // TODO: Use reflection to instantiate the required GridItem type 
            switch (itemTypeName)
            {
                case "Robot":
                    var newGridItem = new Robot(this, name);
                    GridItems.Add(newGridItem);
                    result.Success = true;
                    break;
                default:
                    result.Comment = "Not a valid itemTypeName";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Logic to determine if the passed-in Coordinates lie within the bounds of the grid
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns>bool</returns>
        bool ITaskGrid.IsInGridBounds(Coordinates coordinates)
        {
            bool xIsInBounds = false;
            bool yIsInBounds = false;

            if (coordinates.X >= 0 && coordinates.X < GridSize.X)
                xIsInBounds = true;

            if (coordinates.Y >= 0 && coordinates.Y < GridSize.Y)
                yIsInBounds = true;
            
            return (xIsInBounds && yIsInBounds) ? true : false;
        }

        
        /// <summary>
        /// Get a grid item with the name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The found Grid Item or null</returns>
        private IGridItem GetGridItem(string name)
        {
            return GridItems.Where(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }
    }
}
