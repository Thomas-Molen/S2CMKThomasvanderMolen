using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerVervoer.Tests
{
    [TestClass]
    public class ShipTests
    {
        Ship ship;

        [TestInitialize]
        public void Setup()
        {
            ship = new Ship(100, 3, 3);
        }

        [TestMethod]
        public void Ship_Will_Let_Container_In_If_Possible()
        {
            //Arrange
            Container container = new Container(1, Enums.ContainerType.Normal);
            //Act
            bool containerFits = ship.WillContainerFit(container);
            //Assert
            Assert.IsTrue(containerFits);
        }
        [TestMethod]
        public void Ship_Will_Block_Container_If_Capacity_Too_Low()
        {
            //Arrange
            Container container = new Container(101, Enums.ContainerType.Normal);
            //Act
            bool containerFits = ship.WillContainerFit(container);
            //Assert
            Assert.IsFalse(containerFits);
        }
    }
}
