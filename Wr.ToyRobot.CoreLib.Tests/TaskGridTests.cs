using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wr.ToyRobot.CoreLib.Models;
using Wr.ToyRobot.CoreLib.Models.GridItems;

namespace Wr.ToyRobot.CoreLib.Tests
{
    [TestClass]
    public class TaskGridTests
    {
        private string gridItemTestName => "joe";

        [TestMethod]
        public void TaskGrid_Instanciate_ReturnsDefaultGridSize()
        {
            // Arrange & Act
            var taskGrid = new TaskGrid();

            // Assert
            Assert.IsTrue(taskGrid.GridSize.X == 5 && taskGrid.GridSize.Y == 5);
        }

        [TestMethod]
        public void TaskGrid_RunCommand_NoGridItemNoCommand_ReturnsSuccessFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();

            // Act
            var result = taskGrid.RunCommand("", "");

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void TaskGrid_RunCommand_NoGridItemValidCommand_ReturnsSuccessFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();

            // Act
            var result = taskGrid.RunCommand("", "Place 1,1,North");

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void TaskGrid_RunCommand_NoGridItemWithNameValidCommand_ReturnsSuccessFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();

            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Place 1,1,North");

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void TaskGrid_AddGridItem_NullGridItem_ReturnsSuccessFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(null);

            // Act
            var result = taskGrid.AddGridItem(null);

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void TaskGrid_AddValidGridItem_ReturnsSuccessTrue()
        {
            // Arrange
            var taskGrid = new TaskGrid();

            // Act
            var result = taskGrid.AddGridItem(gridItemTestName);

            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void TaskGrid_PlaceItemOutsideGridBoundsX_ReturnsSuccessFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);

            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Place 5,1,North");

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void TaskGrid_PlaceItemOutsideGridBoundsY_ReturnsSuccessFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();
            taskGrid.AddGridItem(gridItemTestName);

            // Act
            var result = taskGrid.RunCommand(gridItemTestName, "Place 1,5,North");

            // Assert
            Assert.IsFalse(result.Success);
        }

    }
}
