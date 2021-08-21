using System;
using System.Collections.Generic;
using System.Text;

namespace CircusTreinV2
{
    public class AnimalGenerator
    {
        //random selector for the animal's enums
        private Random R = new Random();
        private T RandomEnumValue<T>()
        {
            Array enumOptions = Enum.GetValues(typeof(T));
            return (T)enumOptions.GetValue(R.Next(enumOptions.Length));
        }

        public Animal GenerateAnimal()
        {
            return new Animal(RandomEnumValue<Enums.AnimalType>(), RandomEnumValue<Enums.AnimalSize>());
        }

        public Animal GenerateAnimal(Enums.AnimalType animalType, Enums.AnimalSize animalSize)
        {
            return new Animal(animalType, animalSize);
        }

        public List<Animal> GenerateAnimals(int amountOfAnimals)
        {
            List<Animal> animals = new List<Animal>();
            for (int i = 0; i < amountOfAnimals; i++)
            {
                animals.Add(new Animal(RandomEnumValue<Enums.AnimalType>(), RandomEnumValue<Enums.AnimalSize>()));
            }
            return animals;
        }
    }
}
