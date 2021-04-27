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

        [TestMethod]
        public void Can_Check_Animal_Safety()
        {
            //Arrange
            wagon.AddAnimal(animalGenerator.GenerateAnimal(AnimalType.Carnivore, AnimalSize.Medium));

            //Act
            bool canAddUnsafeHerbivore = wagon.WillAnimalBeSafe(animalGenerator.GenerateAnimal(AnimalType.Herbivore, AnimalSize.Medium));
            bool canAddsafeHerbivore = wagon.WillAnimalBeSafe(animalGenerator.GenerateAnimal(AnimalType.Herbivore, AnimalSize.Large));
            bool canAddUnsafeCarnivore = wagon.WillAnimalBeSafe(animalGenerator.GenerateAnimal(AnimalType.Carnivore, AnimalSize.Medium));

            //Assert
            Assert.IsFalse(canAddUnsafeHerbivore);
            Assert.IsTrue(canAddsafeHerbivore);
            Assert.IsFalse(canAddUnsafeCarnivore);
        }

        [TestMethod]
        public void Can_Check_Wagon_Safety()
        {
            //Arrange
            wagon.AddAnimal(animalGenerator.GenerateAnimal(AnimalType.Herbivore, AnimalSize.Medium));

            //Act
            bool canAddDangerousCarnivore = wagon.WillWagonBeSafe(animalGenerator.GenerateAnimal(AnimalType.Carnivore, AnimalSize.Large));
            bool canAddSafeCarnivore = wagon.WillWagonBeSafe(animalGenerator.GenerateAnimal(AnimalType.Carnivore, AnimalSize.Small));
            bool canAddSafeHerbivore = wagon.WillWagonBeSafe(animalGenerator.GenerateAnimal(AnimalType.Herbivore, AnimalSize.Large));

            //Assert
            Assert.IsFalse(canAddDangerousCarnivore);
            Assert.IsTrue(canAddSafeHerbivore);
            Assert.IsTrue(canAddSafeCarnivore);
        }

        [TestMethod]
        public void Can_Add_Animals_Size_Based()
        {
            //Arrange
            wagon.AddAnimal(animalGenerator.GenerateAnimal(AnimalType.Herbivore, AnimalSize.Large));
            wagon.AddAnimal(animalGenerator.GenerateAnimal(AnimalType.Herbivore, AnimalSize.Medium));

            //Act
            bool canAddLargeAnimal = wagon.CanAnimalFit(animalGenerator.GenerateAnimal(AnimalType.Herbivore, AnimalSize.Large));
            bool canAddSmallAnimal = wagon.CanAnimalFit(animalGenerator.GenerateAnimal(AnimalType.Carnivore, AnimalSize.Small));

            //Assert
            Assert.IsFalse(canAddLargeAnimal);
            Assert.IsTrue(canAddSmallAnimal);
        }
    }
}
 