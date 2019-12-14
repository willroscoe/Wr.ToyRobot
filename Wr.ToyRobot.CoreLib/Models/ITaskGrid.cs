using System;
using System.Collections.Generic;
using System.Text;
using Wr.ToyRobot.CoreLib.Models.GridItems;

namespace Wr.ToyRobot.CoreLib.Models
{
    public interface ITaskGrid
    {
        internal bool IsInGridBounds(Coordinates coordinates);
    }
}
