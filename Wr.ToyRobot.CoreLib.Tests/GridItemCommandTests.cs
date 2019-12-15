using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wr.ToyRobot.CoreLib.Tests
{
    [TestClass]
    public class GridItemCommandTests
    {
        /// <summary>
        /// Default test grid item name
        /// </summary>
        private string gridItemTestName => "joe";

        [DataTestMethod]
        [DataRow("Move")]
        [DataRow("Left")]
        [DataRow("Right")]
        [DataRow("Report")]
        public void RunCommand_InValidCommandFirst_ReturnsSuccessFalse(string command)
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);

            // Act
            var result = taskGrid.RunCommand(gridItemTestName, command);

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void RunCommand_InValidPlaceWithNoParamsCommand_ReturnsSuccessFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);

            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Place");

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void RunCommand_InValidPlaceWithMissingParamsCommand_ReturnsSuccessFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);

            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Place 1,NORTH");

            // Assert
            Assert.IsFalse(result.Success);
        }


        [TestMethod]
        public void RunCommand_ValidPlaceCommand_ReturnsSuccessTrue()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);

            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Place 1,1,EAST");

            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void RunCommand_ValidPlaceThenReportCommand_ReturnsCorrectOutput()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);
            taskGrid.RunCommand(gridItemTestName, "Place 1,1,EAST");

            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Report");

            // Assert
            Assert.AreEqual(result.Output, "1,1,EAST");
        }

        [TestMethod]
        public void RunCommand_ValidMoveCommand_ReturnsCorrectOutput()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);
            taskGrid.RunCommand(gridItemTestName, "Place 1,1,EAST");

            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Move");

            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void RunCommand_ValidLeftCommand_ReturnsSuccessTrue()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);
            taskGrid.RunCommand(gridItemTestName, "Place 1,1,EAST");

            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Left");

            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void RunCommand_ValidLeftThenReportCommand_ReturnsCorrectDirection()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);
            taskGrid.RunCommand(gridItemTestName, "Place 1,1,EAST");
            taskGrid.RunCommand(gridItemTestName, "Left");
            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Report");

            // Assert
            Assert.IsTrue(result.Output.EndsWith("NORTH", StringComparison.InvariantCultureIgnoreCase));
        }

        [TestMethod]
        public void RunCommand_ValidRightCommand_ReturnsSuccessTrue()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);
            taskGrid.RunCommand(gridItemTestName, "Place 1,1,EAST");

            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Right");

            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void RunCommand_ValidRightThenReportCommand_ReturnsCorrectDirection()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);
            taskGrid.RunCommand(gridItemTestName, "Place 1,1,EAST");
            taskGrid.RunCommand(gridItemTestName, "Right");
            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Report");

            // Assert
            Assert.IsTrue(result.Output.EndsWith("SOUTH", StringComparison.InvariantCultureIgnoreCase));
        }

        [DataTestMethod]
        [DataRow("NORTH", "1,2")]
        [DataRow("EAST", "2,1")]
        [DataRow("SOUTH", "1,0")]
        [DataRow("WEST", "0,1")]
        public void RunCommand_ValidMoveInDirectionCommand_ReturnsCorrectCoordinates(string direction, string correctResult)
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);
            taskGrid.RunCommand(gridItemTestName, $"Place 1,1,{direction}");
            taskGrid.RunCommand(gridItemTestName, "Move");
            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Report");

            // Assert
            Assert.IsTrue(result.Output.StartsWith(correctResult));
        }


        [TestMethod]
        public void RunCommand_MoveOffBoard_ReturnsSuccessFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);
            taskGrid.RunCommand(gridItemTestName, "Place 1,1,WEST");
            taskGrid.RunCommand(gridItemTestName, "Move");
            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Move");

            // Assert
            Assert.IsFalse(result.Success);
        }

    }
}
