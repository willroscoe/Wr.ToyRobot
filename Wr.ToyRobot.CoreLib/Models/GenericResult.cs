using System;
using System.Collections.Generic;
using System.Text;

namespace Wr.ToyRobot.CoreLib.Models
{
    public class GenericResult
    {
        public bool Success { get; set; }
        public string Comment { get; set; }
        public string Output { get; set; }
    }
}
