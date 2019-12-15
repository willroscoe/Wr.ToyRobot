using System;
using Wr.ToyRobot.CoreLib;

namespace Wr.ToyRobot.ConsoleApp
{
    class Program
    {
        /// <summary>
        /// A task item name to use.
        /// </summary>
        private static string gridItemName => "marvin";

        private static string instructions => "Type a command, and then press Enter. Type 'exit' to quit.";

        static void Main(string[] args)
        {
            // Instanciate a GridTask
            var gridTask = new TaskGrid();

            // Add a Grid Item
            gridTask.AddGridItem(gridItemName);

            // Display title as the C# console toy robot app.
            Console.WriteLine("Welcome to the Toy Robot Task\r");
            Console.WriteLine("-----------------------------\n");

            // Ask the user to type the first number.
            Console.WriteLine(instructions);

            bool exitApp = false;
            while (!exitApp)
            {
                // Set input text colour to white.
                Console.ForegroundColor = ConsoleColor.White;

                var inputtedCommand = Console.ReadLine();

                if (string.IsNullOrEmpty(inputtedCommand))
                {
                    // Show output in red.
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(instructions);
                    continue;
                }
                else if (inputtedCommand.Trim().ToLower() == "exit")
                {
                    exitApp = true;
                    continue;
                }

                var commandResult = gridTask.RunCommand(gridItemName, inputtedCommand);
                
                // If there is an output then show it.
                if (!string.IsNullOrEmpty(commandResult.Output))
                {
                    // Show output in green.
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Output:{commandResult.Output}");
                }
            }

        }
    }
}
