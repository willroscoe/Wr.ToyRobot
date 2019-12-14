using System;
using System.Collections.Generic;
using System.Text;
using Wr.ToyRobot.CoreLib.Models;

namespace Wr.ToyRobot.CoreLib.Helpers
{
    public static class CustomAttributes
    {
        /// <summary>
        /// MovesNext Custom Attribute
        /// </summary>
        public class MovesNextOffsetAttribute : Attribute
        {
            public int _x { get; private set; }
            public int _y { get; private set; }

            public MovesNextOffsetAttribute(int x, int y)
            {
                this._x = x;
                this._y = y;
            }
        }


        /// <summary>
        /// Get the coordinates to add to the current position to move in this direction
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Coordinates</returns>
        public static Coordinates GetMovesOffset(this Enum value)
        {
            var attribute = value.GetAttribute<MovesNextOffsetAttribute>();
            if (attribute != null)
                return new Coordinates(attribute._x, attribute._y);

            return null;
        }

    }
}
