using CircusTreinV2.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CircusTreinV2
{
    public class Wagon
    {
        public int capacity { get; private set; }
        private List<Animal> animals;

        public Wagon()
        {
            capacity = 10;
            animals = new List<Animal>();
        }

        public void AddAnimal(Animal animal)
        {
            if ((int)animal.animalSize <= capacity)
            {
                if (WillAnimalBeSafe(animal) && WillWagonBeSafe(animal))
                {
                    animals.Add(animal);
                    capacity -= (int)animal.animalSize;
                    return;
                }
                throw new AddAnimalException("Animal is incompatible with wagon");
            }
            throw new AddAnimalException("Animal Does not fit in wagon");
        }

        private bool WillAnimalBeSafe(Animal animal)
        {
            if (animal.animalType == Enums.AnimalType.Carnivore)
            {
                return !animals.Exists(a => a.animalType == Enums.AnimalType.Carnivore);
            }
            else
            {
                return !animals.Exists(a => a.animalType == Enums.AnimalType.Carnivore && a.animalSize >= animal.animalSize);
            }
        }

        private bool WillWagonBeSafe(Animal animal)
        {
            if (animal.animalType == Enums.AnimalType.Carnivore)
            {
                if (animals.Exists(a => a.animalSize <= animal.animalSize))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            string returnString = "Wagon contains:\n";
            foreach (Animal animal in animals)
            {
                returnString += animal.ToString() + "\n";
            }
            return returnString;
        }
    }
}
