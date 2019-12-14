using System;
using System.Collections.Generic;
using System.Text;

namespace Wr.ToyRobot.CoreLib.Models.GridItems
{
    public interface IGridItem
    {
        public string Name { get; }

        public GenericResult RunCommand(string command);

        public GridItemState GetCurrentState();

        /*public CommandResult Place();
        public CommandResult Move();
        public CommandResult Rotate(string rotateCommand);
        public CommandResult Report();

        public GridItemState GridItemState();*/

    }
}
