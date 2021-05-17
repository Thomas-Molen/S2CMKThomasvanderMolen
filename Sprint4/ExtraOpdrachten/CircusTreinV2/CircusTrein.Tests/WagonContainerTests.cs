using CircusTreinV2;
using CircusTreinV2.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircusTrein.Tests
{
    [TestClass]
    public class WagonContainerTests
    {
        WagonContainer train;
        AnimalGenerator animalGenerator;

        [TestInitialize]
        public void Setup()
        {
            train = new WagonContainer();
            animalGenerator = new AnimalGenerator();
        }

        [TestMethod]
        public void Can_Add_Animal()
        {
            //Arrange
            Animal animal = animalGenerator.GenerateAnimal(AnimalType.Carnivore, AnimalSize.Medium);
            train.AddAnimal(animal);

            //Act
            bool trainHasAnimal = train.animals.Contains(animal);

            //Assert
            Assert.IsTrue(trainHasAnimal);
        }

        [TestMethod]
        public void Can_Add_Animals()
        {
            //Arrange
            Animal animal = animalGenerator.GenerateAnimal(AnimalType.Herbivore, AnimalSize.Small);
            List<Animal> animals = new List<Animal>();
            for (int i = 0; i < 3; i++)
            {
                animals.Add(animal);
            }
            train.AddAnimals(animals);

            //Act
            bool areListsEqual = train.animals.SequenceEqual(animals);

            //Assert
            Assert.IsTrue(areListsEqual);
        }
    }
}
