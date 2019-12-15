
namespace Wr.ToyRobot.CoreLib.Models
{
    /// <summary>
    /// Holder for the result of parsing a command.
    /// </summary>
    public class ParseCommandResult
    {
        public bool Success { get; set; }
        public string Comment { get; set; }
        public CommandType Command { get; set; }
        public Coordinates Coordinates { get; set; }
        public FacingDirection FacingDirection { get; set; }
    }
}
