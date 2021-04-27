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
            animals.Add(animal);
            capacity -= (int)animal.animalSize;
        }

        public bool CanAnimalFit(Animal animal)
        {
            if ((int)animal.animalSize <= capacity)
            {
                return true;
            }
            return false;
        }

        private bool DoesWagonContainCarnivore()
        {
            return animals.Exists(a => a.animalType == Enums.AnimalType.Carnivore);
        }

        private bool HasWagonDangerousCarnivore(Animal animal)
        {
            if (animals.Exists(a => a.animalType == Enums.AnimalType.Carnivore && a.animalSize >= animal.animalSize))
            {
                return true;
            }
            return false;
        }

        public bool WillAnimalBeSafe(Animal animal)
        {
            if (animal.animalType == Enums.AnimalType.Carnivore)
            {
                return !DoesWagonContainCarnivore();
            }
            else
            {
                return !HasWagonDangerousCarnivore(animal);
            }
        }

        public bool WillWagonBeSafe(Animal animal)
        {
            if (animal.animalType == Enums.AnimalType.Carnivore)
            {
                if (animals.Exists(a => a.animalSize <= animal.animalSize))
                {
                    return false;
                }
                return true;
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
