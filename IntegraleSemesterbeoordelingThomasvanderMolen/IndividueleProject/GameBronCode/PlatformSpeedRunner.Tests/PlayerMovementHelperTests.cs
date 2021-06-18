using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects;

namespace PlatformSpeedRunner.Tests
{
    [TestClass]
    public class PlayerMovementHelperTests
    {
        PlayerMovementHelper playerMovement;
        Player player;

        [TestInitialize]
        public void Setup()
        {
            playerMovement = new PlayerMovementHelper();
            player = new Player();
        }

        [TestMethod]
        public void X_Velocity_Added_When_Removed_Left()
        {
            //Arrange
            int xVelocity = -1;
            //act
            playerMovement.MoveLeft();
            //Assert
            Assert.AreEqual(xVelocity, playerMovement.xVelocity);
        }

        [TestMethod]
        public void X_Velocity_Added_When_Movement_Right()
        {
            //Arrange
            int xVelocity = 1;
            //act
            playerMovement.MoveRight();
            //Assert
            Assert.AreEqual(xVelocity, playerMovement.xVelocity);
        }
    }
}
