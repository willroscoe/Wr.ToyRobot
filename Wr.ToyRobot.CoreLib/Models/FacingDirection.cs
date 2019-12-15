using System;
using System.Collections.Generic;
using System.Text;
using static Wr.ToyRobot.CoreLib.Helpers.CustomAttributes;

namespace Wr.ToyRobot.CoreLib.Models
{
    /// <summary>
    /// The enum int value indicates degress
    /// Note: Assumes 0,0 is in the South West Corner
    /// </summary>
    public enum FacingDirection
    {
        [MovesNextOffset(0, 1)]
        NORTH = 0,

        [MovesNextOffset(1, 0)]
        EAST = 90,

        [MovesNextOffset(0, -1)]
        SOUTH = 180,

        [MovesNextOffset(-1, 0)]
        WEST = 270
    }
}
