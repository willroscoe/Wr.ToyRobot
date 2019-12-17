[![Build status](https://ci.appveyor.com/api/projects/status/d68cf9e4mqy49iap?svg=true)](https://ci.appveyor.com/project/willroscoe/wr-toyrobot)
# Toy Robot Challenge
*"You have a toy robot on a table top, a grid of 5 x 5 units, there are no obstructions. You can issue commands to your robot allowing it to roam around the table top. But be careful, don't let it fall off!"*

**Available commands** are:
- `Place [X],[Y],[FACING]` (Where FACING can be NORTH|SOUTH|EAST|WEST). E.g. `Place 1,1,NORTH`
- `Move` - Moves in the facing direction 1 grid.
- `Left` - Rotate the facing direction anti-clockwise 90 deg.
- `Right` - Rotate the facing direction clockwise 90 deg.
- `Report` - Outputs the current state of the robot i.e. [X],[Y],[FACING]

This .NET Core 3.1 cross-platform solution consists of a class library, which holds all the task logic, and a console application, which acts as an example interactive interface to the class library.

The console application can be run interactively, or multiple input commands can be supplied in a text file (see [Input Scripts][INPUTSCRIPTSLINK] section). The application automatically creates a 5 x 5 grid. Just start typing commands to control the robot.

[INPUTSCRIPTSLINK]: https://github.com/willroscoe/Wr.ToyRobot/blob/master/Wr.ToyRobot.CoreLib/Models/GridItems/GridItemBase.cs
Test scripts (inputs & outputs) for the latest build of the application can be found in the [TestScripts][TESTSCRIPTSLINK] folder. The results files can be automatically generated after a Release build by running the `Wr.ToyRobot.ConsoleApp/PostBuildProcessInputScripts.bat` script (only for Windows), assuming you have cloned the repo to your local machine. This script deletes any results files in the TestScripts folder and processes each .txt file in the folder.

## Additional commands
- `show comments` - Display any comments returned from running a command.
- `hide comments` - Hide comments.
- `save outputs` - Save the console input/outputs to a text file.
- `exit` - Quit the console applciation.

## Task Assumptions
- The grid is 5 x 5
- Only commands which place or move the robot within the grid are valid.
- No other commands will work until a valid `Place` command has been run.

## To run the Console app
Either, use one of the latest pre-built packages from the [**Release**][RELEASELINK] area. There are builds for Windows, MacOs and Linux.

...or, build the solution from Visual Studio, or the dotnet cli, by first cloning this repo. Note: You will need the [.NET Core 3.1 SDK][DOTNETCOREDOWNLOAD] installed in order to build this solution.

Windows: `dotnet publish ./Wr.ToyRobot.ConsoleApp/Wr.ToyRobot.ConsoleApp.csproj -f netcoreapp3.1 -r win-x64 -c Release --self-contained -o artifacts/win-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true /p:PublishReadyToRun=true`

MacOs: `dotnet publish ./Wr.ToyRobot.ConsoleApp/Wr.ToyRobot.ConsoleApp.csproj -f netcoreapp3.1 -r osx-x64 -c Release --self-contained -o artifacts/osx-x64 /p:PublishTrimmed=true`

Linux: `dotnet publish ./Wr.ToyRobot.ConsoleApp/Wr.ToyRobot.ConsoleApp.csproj -f netcoreapp3.1 -r linux-x64 -c Release --self-contained -o artifacts/linux-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true`

### Then running...
**On Windows:**
The Windows package consists of an executable .exe file which can be run by double clicking, or from the command line.

**On MacOs:**
To run you will need either [.NET Core 3.1 SDK or Runtime][DOTNETCOREDOWNLOAD] installed. 
Then from a Terminal prompt in the release folder type: `dotnet Wr.ToyRobot.ConsoleApp.dll`

**On Linux: (Main distros only)**
From a Terminal prompt in the release folder type: `./Wr.ToyRobot.ConsoleApp`

**Then, just start typing commands...**

[RELEASELINK]: https://github.com/willroscoe/Wr.ToyRobot/releases
[DOTNETCOREDOWNLOAD]: https://dotnet.microsoft.com/download

## Input Scripts
You can use a script file as input to the console application if required. The file must to be a .txt file, with one command per line. See the [TestScripts][TESTSCRIPTSLINK] folder for examples.

To run the console app with a test script, run the console app from the command line using the input script filename as an argument, e.g. `Wr.ToyRobot.ConsoleApp.exe MyTestInputs.txt` (Windows example).

When run this will generate a results file with a _Results.txt suffix.

[TESTSCRIPTSLINK]: https://github.com/willroscoe/Wr.ToyRobot/tree/master/TestScripts

## Extending
- It is possible to extend the type and number of grid item's (i.e. Robots) available to add to the grid. They just need to inherit from the [`GridItemBase`][GRIDITEMBASELINK] class. Note: This functionality has not been tested.
- The grid can be any size, but for the purposes of this task the grid is set to 5 x 5 in the console application.

[GRIDITEMBASELINK]: https://github.com/willroscoe/Wr.ToyRobot/blob/master/Wr.ToyRobot.CoreLib/Models/GridItems/GridItemBase.cs

## Version history
- 1.0.0
    - Initial release
