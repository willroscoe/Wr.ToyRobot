
namespace Wr.ToyRobot.CoreLib.Models
{
    /// <summary>
    /// Holder for any method results.
    /// </summary>
    public class GenericResult
    {
        /// <summary>
        /// The method result was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Any relevant comment about the success or failure of the result.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Any required output from the result.
        /// </summary>
        public string Output { get; set; }
    }
}
