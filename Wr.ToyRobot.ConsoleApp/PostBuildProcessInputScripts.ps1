cd ../TestScripts
$currentDirectory = Get-Location

Remove-Item –Path "$($currentDirectory)\*.txt" -include *_Results.txt

Get-ChildItem -Path  "$($currentDirectory)\*.txt" -Exclude *_Results.txt  |

Foreach-Object {
	../Wr.ToyRobot.ConsoleApp/bin/Release/netcoreapp3.1/win-x64/Wr.ToyRobot.ConsoleApp.exe $_.FullName
}