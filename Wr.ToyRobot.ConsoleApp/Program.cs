using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Wr.ToyRobot.CoreLib;
using Wr.ToyRobot.CoreLib.Models.GridItems;

namespace Wr.ToyRobot.ConsoleApp
{
    class Program
    {
        /// <summary>
        /// A task item name to use.
        /// </summary>
        private static string gridItemName => "marvin";

        /// <summary>
        /// General instructions.
        /// </summary>
        private static string instructions => "Type a command, and then press Enter. Type 'show comments' to display any comments that the commands return. Type 'save outputs' to save the outputs to a file. Type 'exit' to quit.";

        /// <summary>
        /// Valid commands. IDEA: Dynamically get these from the TaskGrid object?
        /// </summary>
        private static string validCommands => "Possible commands are: Place X,Y,[NORTH|SOUTH|EAST|WEST]; LEFT; RIGHT; MOVE; REPORT";


        static void Main(string[] args)
        {
            List<string> commandsFromFile = new List<string>();
            StringBuilder outputLog = new StringBuilder();

            bool IsInputFromFile = false;
            bool IsOutputToFile = false;

            string outputFilename = "Results.txt";

            // Instanciate a TaskGrid
            var gridTask = new TaskGrid();

            // Add a Grid Item
            gridTask.AddGridItem<Robot>(gridItemName);

            // Display title as the C# console toy robot app.
            Console.WriteLine("Welcome to the Toy Robot Task\n");
            Console.WriteLine("-----------------------------\n");

            // Ask the user to type the first number.
            Console.WriteLine(instructions);
            Console.WriteLine(validCommands);

            if (args.Length > 0)
            {
                // An input file has been specified
                var inputFilePath = args[0];
                if (File.Exists(inputFilePath))
                {
                    // Load all commands. Ignore any empty lines.
                    commandsFromFile = File.ReadAllLines(inputFilePath).Where(x => !string.IsNullOrEmpty(x)).ToList();
                    if (commandsFromFile.Count > 0)
                    {
                        IsInputFromFile = true;

                        // If there is an input file then automatically save the results to an output file.
                        IsOutputToFile = true;

                        // Base the default output filename in the input filename.
                        var fileInfo = new FileInfo(inputFilePath);
                        outputFilename = $"{fileInfo.Name.Replace(fileInfo.Extension, "")}_Results.txt";
                    }
                }
            }

            bool exitApp = false;
            bool showComments = false;

            while (!exitApp)
            {
                // Set input text colour to white.
                Console.ForegroundColor = ConsoleColor.White;
                var inputtedCommand = string.Empty;

                if (IsInputFromFile)
                {
                    // Get the command at the top of the list.
                    inputtedCommand = commandsFromFile.FirstOrDefault();
                    if (string.IsNullOrEmpty(inputtedCommand))
                    {
                        // No commands left to process, so allow standard input now.
                        IsInputFromFile = false;
                        Console.WriteLine("You can type additional commands if you want, or type 'exit' to exit.");
                        continue;
                    }

                    // Remove this command from the list
                    commandsFromFile.RemoveAt(0);

                    // Output the command to the console too.
                    Console.WriteLine(inputtedCommand);
                }
                else
                {
                    inputtedCommand = Console.ReadLine();
                }

                // Store all outputs.
                outputLog.AppendLine(inputtedCommand);

                if (string.IsNullOrEmpty(inputtedCommand))
                {
                    // Show output in red.
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(instructions);
                    continue;
                }
                else if (inputtedCommand.Trim().ToLower() == "show comments")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Type 'hide comments' to stop showing comments.");
                    showComments = true;
                    continue;
                }
                else if (inputtedCommand.Trim().ToLower() == "hide comments")
                {
                    showComments = false;
                    continue;
                }
                else if (inputtedCommand.Trim().ToLower() == "save outputs")
                {
                    IsOutputToFile = true;
                    continue;
                }
                else if (inputtedCommand.Trim().ToLower() == "exit")
                {
                    break;
                }

                var commandResult = gridTask.RunCommand(gridItemName, inputtedCommand);

                // If there is an output then show it.
                if (!string.IsNullOrEmpty(commandResult.Output))
                {
                    var output = $"Output: {commandResult.Output}";

                    outputLog.AppendLine(output);

                    // Show output in green.
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(output);
                }

                if (showComments && !string.IsNullOrEmpty(commandResult.Comment))
                {
                    var comment = $"Comment: {commandResult.Comment}";

                    outputLog.AppendLine(comment);

                    // Show output in green.
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(comment);
                }
            } // While

            if (IsOutputToFile)
            {
                if (outputLog.Length > 0 && !string.IsNullOrEmpty(outputFilename))
                {
                    // Save output to file.
                    File.WriteAllText(outputFilename, outputLog.ToString());
                    Console.WriteLine($"Results file saved: {outputFilename}");
                }
            }
        }
    }
}
