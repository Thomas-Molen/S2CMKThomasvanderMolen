using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects;

namespace PlatformSpeedRunner.Tests
{
    [TestClass]
    public class PositionHelperTests
    {
        PositionHelper positionHelper;

        [TestInitialize]
        public void Setup()
        {
            positionHelper = new PositionHelper();
        }

        [TestMethod]
        public void Helper_Sets_Position_To_Zero_By_Default()
        {
            //Arrange
            //act
            //Assert
            Assert.AreEqual(Vector2.Zero, positionHelper.position);
        }

        [TestMethod]
        public void Helper_Sets_Position_Properly()
        {
            //Arrange
            Vector2 newPosition = new Vector2(100, 100);
            //act
            positionHelper.SetPosition(newPosition);
            //Assert
            Assert.AreEqual(newPosition, positionHelper.position);
        }
    }
}
