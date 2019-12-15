using System;
using Wr.ToyRobot.CoreLib.Helpers;
using static Wr.ToyRobot.CoreLib.Helpers.CustomAttributes;

namespace Wr.ToyRobot.CoreLib.Models
{
 
    /// <summary>
    /// An object to hold the grid item 'state' on the board. i.e. location, facing which direction etc.
    /// </summary>
    public partial class GridItemState : ICloneable
    {
        /// <summary>
        /// The position of the item on the grid.
        /// </summary>
        public Coordinates Coords { get; set; }

        /// <summary>
        /// The direction the item is facing.
        /// </summary>
        public FacingDirection Facing { get; set; }
        
        /// <summary>
        /// Any relevant comments about this state.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Return the coordinates for the next move.
        /// </summary>
        /// <returns>Coordinates</returns>
        public Coordinates GetCoordinatesForNextMove(int numberOfGridSquaresToMove = 1)
        {
            var getMoveForCurrentDirection = Facing.GetMovesOffset();

            // Add current coords to the (move offset coords * numberOfGridSquaresToMove)
            var newCoords = new Coordinates(Coords.X + (getMoveForCurrentDirection.X * numberOfGridSquaresToMove), Coords.Y + (getMoveForCurrentDirection.Y * numberOfGridSquaresToMove));

            return newCoords;
        }

        /// <summary>
        /// Shallow Clone the instance of this object.
        /// </summary>
        /// <returns>object</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Overrides the ToString() method to output a custom string of the object data.
        /// </summary>
        /// <returns>X,Y,Facing</returns>
        public override string ToString()
        {
            return $"{Coords.X},{Coords.Y},{Facing.ToString()}";
        }
    }
}
