using System;
using System.Collections.Generic;
using System.Text;

namespace Wr.ToyRobot.CoreLib.Models
{

    public enum CommandType
    {
       PLACE,
       MOVE,
       LEFT,
       RIGHT,
       REPORT
    }

    public class CommandHolder
    {
        public string Command { get; set; }

        public CommandType CommandType { get; set; }

        public string Message { get; set; }
    }
}
