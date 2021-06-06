using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Graphics;
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
        public void X_Velocity_Does_Not_Go_Under_Max_Player_Speed()
        {
            //Arrange
            int maxXVelocity = -3;
            playerMovement.xVelocity = maxXVelocity - 1;
            //act
            playerMovement.MoveLeft();
            //Assert
            Assert.AreEqual(maxXVelocity - 1, playerMovement.xVelocity);
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

        [TestMethod]
        public void X_Velocity_Does_Not_Go_Over_Max_Player_Speed()
        {
            //Arrange
            int maxXVelocity = 3;
            playerMovement.xVelocity = maxXVelocity + 1;
            //act
            playerMovement.MoveRight();
            //Assert
            Assert.AreEqual(maxXVelocity + 1, playerMovement.xVelocity);
        }

        [TestMethod]
        public void Reset_Velocity_Sets_Velocity_To_0()
        {
            //Arrange
            int xVelocity = 5;
            int yVelocity = 5;
            playerMovement.xVelocity = xVelocity;
            playerMovement.yVelocity = yVelocity;
            //act
            playerMovement.ResetVelocity();
            //Assert
            Assert.AreEqual(0, playerMovement.xVelocity);
            Assert.AreEqual(0, playerMovement.yVelocity);
        }

        [TestMethod]
        public void X_Velocity_Will_Go_Down_To_Zero_When_No_Direction()
        {
            //Arrange
            float xVelocity = 5;
            float velocityDecrease = 3f / 15;
            playerMovement.xVelocity = xVelocity;
            //act
            playerMovement.NoDirection();
            //Assert
            Assert.AreEqual(xVelocity-velocityDecrease, playerMovement.xVelocity);
        }

        [TestMethod]
        public void X_Velocity_Will_Go_Up_To_Zero_When_No_Direction()
        {
            //Arrange
            float xVelocity = -5;
            float velocityDecrease = 3f / 15;
            playerMovement.xVelocity = xVelocity;
            //act
            playerMovement.NoDirection();
            //Assert
            Assert.AreEqual(xVelocity + velocityDecrease, playerMovement.xVelocity);
        }

        [TestMethod]
        public void X_Velocity_Round_Down_To_Zero_When_No_Direction()
        {
            //Arrange
            float xVelocity = 0.5f;
            playerMovement.xVelocity = xVelocity;
            //act
            playerMovement.NoDirection();
            //Assert
            Assert.AreEqual(0, playerMovement.xVelocity);
        }

        [TestMethod]
        public void Y_Velocity_Goes_Up_By_Gravity()
        {
            //Arrange
            float yVelocity = 10;
            float gravity = 0.3f;
            playerMovement.yVelocity = yVelocity;
            //act
            playerMovement.PlayerPhysics(player);
            //Assert
            Assert.AreEqual(10+gravity, playerMovement.yVelocity);
        }

        [TestMethod]
        public void Y_Velocity_Stays_At_Max_Y_Velocity()
        {
            //Arrange
            int maxYVelocity = 15;
            playerMovement.yVelocity = maxYVelocity;
            //act
            playerMovement.PlayerPhysics(player);
            //Assert
            Assert.AreEqual(maxYVelocity, playerMovement.yVelocity);
        }

        [TestMethod]
        public void Y_Velocity_Can_Not_Go_Over_Max_Y_Velocity()
        {
            //Arrange
            int maxYVelocity = 15;
            playerMovement.yVelocity = maxYVelocity + 1;
            //act
            playerMovement.PlayerPhysics(player);
            //Assert
            Assert.AreEqual(maxYVelocity, playerMovement.yVelocity);
        }

        [TestMethod]
        public void Y_Velocity_Can_Not_Go_Under_Max_Y_Velocity()
        {
            //Arrange
            int maxYVelocity = 15;
            float gravity = 0.3f;
            playerMovement.yVelocity = -maxYVelocity - 1;
            //act
            playerMovement.PlayerPhysics(player);
            //Assert
            Assert.AreEqual(-maxYVelocity+gravity, playerMovement.yVelocity);
        }

        [TestMethod]
        public void X_Velocity_Can_Not_Go_Over_Max_X_Velocity()
        {
            //Arrange
            int maxXVelocity = 10;
            playerMovement.xVelocity = maxXVelocity+1;
            //act
            playerMovement.PlayerPhysics(player);
            //Assert
            Assert.AreEqual(maxXVelocity, playerMovement.xVelocity);
        }

        [TestMethod]
        public void X_Velocity_Can_Not_Go_Under_Max_X_Velocity()
        {
            //Arrange
            int maxXVelocity = 10;
            playerMovement.xVelocity = -maxXVelocity - 1;
            //act
            playerMovement.PlayerPhysics(player);
            //Assert
            Assert.AreEqual(-maxXVelocity, playerMovement.xVelocity);
        }
    }
}
