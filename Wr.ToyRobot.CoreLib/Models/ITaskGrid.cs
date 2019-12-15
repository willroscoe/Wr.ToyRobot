
namespace Wr.ToyRobot.CoreLib.Models
{
    /// <summary>
    /// Interface foe the Task Grid.
    /// </summary>
    public interface ITaskGrid
    {
        /// <summary>
        /// Method to check if the passed-in coordinates lie within the grid boundaries.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns>bool</returns>
        internal bool IsInGridBounds(Coordinates coordinates);
    }
}
