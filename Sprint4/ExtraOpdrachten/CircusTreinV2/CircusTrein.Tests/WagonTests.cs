using CircusTreinV2;
using CircusTreinV2.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircusTrein.Tests
{
    [TestClass]
    public class WagonTests
    {
        private Wagon wagon;
        private AnimalGenerator animalGenerator;

        [TestInitialize]
        public void Setup()
        {
            wagon = new Wagon();
            animalGenerator = new AnimalGenerator();
        }

        [TestMethod]
        public void Wagon_Has_Default_Capacity_When_Constructed()
        {
            //Arrange

            //Act

            //Assert
            Assert.AreEqual(10, wagon.capacity);
        }

        [TestMethod]
        public void Capacity_Lowered_When_Animal_Added()
        {
            //Arrange
            Animal animal = animalGenerator.GenerateAnimal(AnimalType.Herbivore, AnimalSize.Large);
            int originalCapacity = wagon.capacity;
            int animalSize = (int)animal.animalSize;
            int expectedCapacity = originalCapacity - animalSize;

            //Act
            wagon.AddAnimal(animal);

            //Assert
            Assert.AreEqual(expectedCapacity, wagon.capacity);
        }
    }
}
 