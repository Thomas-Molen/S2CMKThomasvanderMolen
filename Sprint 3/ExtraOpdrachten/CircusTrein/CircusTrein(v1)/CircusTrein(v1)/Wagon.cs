using System;
using System.Collections.Generic;
using System.Text;

namespace CircusTrein_v1_
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

        public void UpdateCapacity(int CapacityChange)
        {
            capacity += CapacityChange;
        }

        public void AddAnimal(Animal NewAnimal)
        {
            animals.Add(NewAnimal);
        }

        public List<Animal> GetAllAnimals()
        {
            return animals;
        }
    }
}
