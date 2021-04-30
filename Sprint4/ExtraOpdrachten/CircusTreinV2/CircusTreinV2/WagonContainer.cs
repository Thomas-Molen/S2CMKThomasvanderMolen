using CircusTreinV2.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CircusTreinV2
{
    public class WagonContainer
    {
        private List<Wagon> wagons;
        public List<Animal> animals { get; private set; }

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
                
                foreach (Animal animal in animals.ToArray())
                {
                    try
                    {
                        wagon.AddAnimal(animal);
                    }
                    catch (AddAnimalException)
                    {
                        continue;
                    }
                    animals.Remove(animal);
                }
                wagons.Add(wagon);
            }
        }

        public override string ToString()
        {
            string returnString = "Train contains: " + wagons.Count + " Wagons\n\n";
            foreach (Wagon wagon in wagons)
            {
                returnString += wagon.ToString() + "\n";
            }
            return returnString;
        }
    }
}
