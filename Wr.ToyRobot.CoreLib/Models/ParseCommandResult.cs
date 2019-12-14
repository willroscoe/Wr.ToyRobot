using System;
using System.Collections.Generic;
using System.Text;

namespace Wr.ToyRobot.CoreLib.Models
{
    public class ParseCommandResult
    {
        public bool Success { get; set; }
        public string Comment { get; set; }
        public CommandType Command { get; set; }
        public Coordinates Coordinates { get; set; }
        public FacingDirection FacingDirection { get; set; }
    }
}
