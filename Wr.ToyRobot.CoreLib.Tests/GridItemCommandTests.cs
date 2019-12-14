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
        private string gridItemName => "mrrobot";

        [DataTestMethod]
        [DataRow("Move")]
        [DataRow("Left")]
        [DataRow("Right")]
        [DataRow("Report")]
        public void RunCommand_InValidCommandFirst_ReturnsFalse(string command)
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemName);

            // Act
            var result = taskGrid.RunCommand(gridItemName, command);

            // Assert
            Assert.IsFalse(result.Success);
        }


        [TestMethod]
        public void RunCommand_ValidPlaceCommand_ReturnsTrue()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemName);

            // Act
            var result = taskGrid.RunCommand(gridItemName, "Place 1,1,EAST");

            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void RunCommand_ValidPlaceThenReportCommand_ReturnsCorrectOutput()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemName);
            taskGrid.RunCommand(gridItemName, "Place 1,1,EAST");

            // Act
            var result = taskGrid.RunCommand(gridItemName, "Report");

            // Assert
            Assert.AreEqual(result.Output, "1,1,EAST");
        }

    }
}
