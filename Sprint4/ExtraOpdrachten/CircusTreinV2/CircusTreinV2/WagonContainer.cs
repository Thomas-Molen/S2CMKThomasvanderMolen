using System;
using System.Collections.Generic;
using System.Text;

namespace CircusTreinV2
{
    public class WagonContainer
    {
        private List<Wagon> wagons;
        private List<Animal> animals;

        public WagonContainer()
        {
            animals = new List<Animal>();
            wagons = new List<Wagon>();
        }

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        public void AddAnimals(List<Animal> animalsToBeAdded)
        {
            foreach(Animal animalToBeAdded in animalsToBeAdded)
            {
                animals.Add(animalToBeAdded);
            }
        }

        public void FillWagons()
        {
            while (animals.Count != 0)
            {
                Wagon wagon = new Wagon();
                List<Animal> animalsToAdd = animals;
                List<Animal> animalsToRemove = new List<Animal>();
                
                foreach (Animal animal in animalsToAdd)
                {
                    if (wagon.CanAnimalFit(animal))
                    {
                        if (wagon.WillAnimalBeSafe(animal))
                        {
                            wagon.AddAnimal(animal);
                            animalsToRemove.Add(animal);
                        }
                    }
                }
                wagons.Add(wagon);
                foreach (Animal animal in animalsToRemove)
                {
                    animals.Remove(animal);
                }
            }
        }
    }
}
