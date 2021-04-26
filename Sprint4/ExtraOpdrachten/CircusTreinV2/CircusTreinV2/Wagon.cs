using System;
using System.Collections.Generic;
using System.Text;

namespace CircusTreinV2
{
    public class Wagon
    {
        private List<Animal> animals;
        public int capacity { get; private set; }

        public Wagon()
        {
            animals = new List<Animal>();
            capacity = 10;
        }

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        public bool CanAnimalFit(Animal animal)
        {
            if ((int)animal.animalSize <= capacity)
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

        public override string ToString()
        {
            return "Wagon Contains: " + animals.ToString();
        }
    }
}
