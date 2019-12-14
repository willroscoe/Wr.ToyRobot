using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wr.ToyRobot.CoreLib.Models.GridItems
{
    /// <summary>
    /// Robot 'Grid Item'
    /// </summary>
    public class Robot : GridItemBase
    {
        /// <summary>
        /// Sets the GridItem 'Type' Name
        /// </summary>
        public override string GRID_ITEM_TYPE_NAME { get => "Robot"; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public Robot(ITaskGrid taskGrid, string name) : base (taskGrid, name) { }

    }
}
