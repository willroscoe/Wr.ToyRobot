using System;
using System.Collections.Generic;
using System.Text;

namespace Wr.ToyRobot.CoreLib.Models.GridItems
{
    /// <summary>
    /// Interface for a grid item.
    /// </summary>
    public interface IGridItem
    {
        /// <summary>
        /// Name/identifier of the item.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The method which runs the command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>GenericResult</returns>
        public GenericResult RunCommand(string command);

        /// <summary>
        /// A method to get the current state of the item.
        /// </summary>
        /// <returns></returns>
        public GridItemState GetCurrentState();
    }
}
