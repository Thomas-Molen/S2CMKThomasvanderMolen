using CircusTreinV2.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CircusTreinV2
{
    public class Animal
    {
        public AnimalType animalType { get; private set; }
        public AnimalSize animalSize { get; private set; }

        public Animal(AnimalType type, AnimalSize size)
        {
            animalType = type;
            animalSize = size;
        }
        public override string ToString()
        {
            return "Animal: " + animalSize + " " + animalType;
        }

    }
}
