using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects;

namespace PlatformSpeedRunner.Tests
{
    [TestClass]
    public class BoundingBoxHelperTests
    {
        BoundingBoxHelper boundingBoxHelper;

        [TestInitialize]
        public void Setup()
        {
            boundingBoxHelper = new BoundingBoxHelper();
        }

        [TestMethod]
        public void Add_Bounding_Box_To_List()
        {
            //Arrange
            Vector2 bbPosition = new Vector2(100, 100);
            int bbHeight = 20;
            int bbWidth = 20;
            BoundingBoxObject bb = new BoundingBoxObject(bbPosition, bbWidth, bbHeight);
            //act
            boundingBoxHelper.AddBoundingBox(bb);
            //Assert
            Assert.AreEqual(bb, boundingBoxHelper.boundingBoxes[0]);
        }

        [TestMethod]
        public void Bounding_Boxes_Properly_Move()
        {
            //Arrange
            Vector2 bbPosition = new Vector2(100, 100);
            Vector2 PositionToAdd = new Vector2(120, 120);
            int bbHeight = 20;
            int bbWidth = 20;
            BoundingBoxObject bb = new BoundingBoxObject(bbPosition, bbWidth, bbHeight);
            //act
            boundingBoxHelper.AddBoundingBox(bb);
            boundingBoxHelper.UpdateBoundingBoxes(PositionToAdd);
            //Assert
            Assert.AreEqual(bbPosition + PositionToAdd, boundingBoxHelper.boundingBoxes[0].position);
        }
    }
}
