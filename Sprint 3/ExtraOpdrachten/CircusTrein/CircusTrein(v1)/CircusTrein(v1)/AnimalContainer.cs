using System;
using System.Collections.Generic;
using System.Text;

namespace CircusTrein_v1_
{
    public class AnimalContainer
    {
        private List<Animal> Animals;

        public AnimalContainer()
        {
            Animals = new List<Animal>();
        }
        public void AddAnimal(Animal NewAnimal)
        {
            if (Animals.Contains(NewAnimal))
            {
                throw new ArgumentOutOfRangeException("Animal already exists.");
            }
            Animals.Add(NewAnimal);
        }

        public List<Animal> GetAllAnimals()
        {
            return Animals;
        }
    }
}
