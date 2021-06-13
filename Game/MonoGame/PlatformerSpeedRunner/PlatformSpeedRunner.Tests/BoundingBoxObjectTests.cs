using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects;

namespace PlatformSpeedRunner.Tests
{
    [TestClass]
    public class BoundingBoxObjectTests
    {
        BoundingBoxObject boundingBox;

        [TestInitialize]
        public void Setup()
        {
            boundingBox = new BoundingBoxObject(new Vector2(100, 100), 100, 100);
        }

        [TestMethod]
        public void Bounding_Box_Position_Set_Properly()
        {
            //Arrange
            //act
            //Assert
            Assert.AreEqual(new Vector2(100, 100), boundingBox.Position.position);
        }

        [TestMethod]
        public void Bounding_Box_Returns_True_When_Colliding()
        {
            //Arrange
            BoundingBoxObject boundingBox2 = new BoundingBoxObject(new Vector2(110, 110), 50, 50);
            //act
            bool boundingBoxesCollide = boundingBox.CollidesWith(boundingBox2);
            //Assert
            Assert.IsTrue(boundingBoxesCollide);
        }

        [TestMethod]
        public void Bounding_Box_Returns_False_When_Not_Colliding()
        {
            //Arrange
            BoundingBoxObject boundingBox2 = new BoundingBoxObject(new Vector2(10, 10), 50, 50);
            //act
            bool boundingBoxesDoNotCollide = boundingBox.CollidesWith(boundingBox2);
            //Assert
            Assert.IsFalse(boundingBoxesDoNotCollide);
        }
    }
}
