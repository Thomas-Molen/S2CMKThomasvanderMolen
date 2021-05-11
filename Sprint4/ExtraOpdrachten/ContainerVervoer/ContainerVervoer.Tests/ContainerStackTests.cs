using ContainerVervoer.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerVervoer.Tests
{
    [TestClass]
    public class ContainerStackTests
    {
        ContainerStack stack;
        ContainerStack middleStack;

        [TestInitialize]
        public void Setup()
        {
            stack = new ContainerStack(Enums.ShipPosition.First);
            middleStack = new ContainerStack(Enums.ShipPosition.Middle);
        }

        [TestMethod]
        public void Stack_Has_Default_Weight_When_Constructed()
        {
            //Arrange
            int defaultWeight = 0;
            //Act
            //Assert
            Assert.AreEqual(defaultWeight, stack.totalWeight);
        }

        [TestMethod]
        public void When_Adding_Container_Weight_Gets_Added_To_Stack()
        {
            //Arrange
            int containerWeight = 50;
            Container container = new Container(containerWeight, Enums.ContainerType.Cooled);
            //Act
            stack.AddContainer(container);
            //Assert
            Assert.AreEqual(containerWeight, stack.totalWeight);
        }

        [TestMethod]
        [ExpectedException(typeof(AddContainerException), "Container could not be added to stack")]
        public void Can_Not_Add_Two_Valuable_Containers()
        {
            //Arrange
            Container container1 = new Container(0, Enums.ContainerType.Valueable);
            stack.AddContainer(container1);

            Container container2 = new Container(0, Enums.ContainerType.Valueable);
            //Act
            stack.AddContainer(container2);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ValidPositionException), "Container can not be added in this stack")]
        public void Can_Not_Add_Valuable_Container_In_Middle_Of_Ship()
        {
            //Arrange
            Container container = new Container(0, Enums.ContainerType.Valueable);

            //Act
            middleStack.AddContainer(container);
            //Assert
        }

        [TestMethod]
        public void Will_Add_Container_Under_Valueable_Container_If_Needed()
        {
            //Arrange
            Container container1 = new Container(0, Enums.ContainerType.Valueable);
            stack.AddContainer(container1);

            Container container2 = new Container(0, Enums.ContainerType.Normal);
            //Act
            stack.AddContainer(container2);
            //Assert
            Assert.AreEqual(container2, stack.containers[0]);
        }
    }
}
