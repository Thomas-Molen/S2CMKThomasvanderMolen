using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerVervoer.Tests
{
    [TestClass]
    public class ContainerTests
    {
        private Container container;
        [TestMethod]
        public void Container_Has_Set_Weight_When_Constructed()
        {
            //Arrange
            int containerWeight = 100;
            //Act
            container = new Container(containerWeight, Enums.ContainerType.Normal);
            //Assert
            Assert.AreEqual(100, container.weight);
        }
        [TestMethod]
        public void Container_Has_Set_Type_When_Constructed()
        {
            //Arrange
            Enums.ContainerType containerType = Enums.ContainerType.Cooled;
            //Act
            container = new Container(50, containerType);
            //Assert
            Assert.AreEqual(Enums.ContainerType.Cooled, container.type);
        }
    }
}
