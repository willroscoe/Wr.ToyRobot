using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wr.ToyRobot.CoreLib.Models;
using Wr.ToyRobot.CoreLib.Models.GridItems;

namespace Wr.ToyRobot.CoreLib.Tests
{
    [TestClass]
    public class TaskGridTests
    {
        [TestMethod]
        public void TaskGrid_Instanciate_ReturnsDefaultGridSize()
        {
            // Arrange & Act
            var taskGrid = new TaskGrid();

            // Assert
            Assert.IsTrue(taskGrid.GridSize.X == 5 && taskGrid.GridSize.Y == 5);
        }

        [TestMethod]
        public void TaskGrid_RunCommand_NoGridItemNoCommand_ReturnsFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();

            // Act
            var result = taskGrid.RunCommand("", "");

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void TaskGrid_RunCommand_NoGridItemValidCommand_ReturnsFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();

            // Act
            var result = taskGrid.RunCommand("", "Place 1,1,North");

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void TaskGrid_RunCommand_NoGridItemWithNameValidCommand_ReturnsFalse()
        {
            // Arrange
            var taskGrid = new TaskGrid();

            // Act
            var result = taskGrid.RunCommand("mrrobot", "Place 1,1,North");

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void TaskGrid_AddGridItem_NullGridItem_ReturnsFalse()
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
        public void TaskGrid_AddValidGridItem_ReturnsTrue()
        {
            // Arrange
            var taskGrid = new TaskGrid();

            // Act
            var result = taskGrid.AddGridItem("mrrobot");

            // Assert
            Assert.IsTrue(result.Success);
        }

    }
}
