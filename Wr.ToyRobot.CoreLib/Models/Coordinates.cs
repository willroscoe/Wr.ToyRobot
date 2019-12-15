using System;
using System.Collections.Generic;
using System.Text;

namespace Wr.ToyRobot.CoreLib.Models
{
    /// <summary>
    /// Holds Coordinates.
    /// </summary>
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
