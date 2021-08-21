using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class ContainerGenerator
    {
        //random selector for the animal's enums
        private Random R = new Random();
        private T RandomEnumValue<T>()
        {
            Array enumOptions = Enum.GetValues(typeof(T));
            return (T)enumOptions.GetValue(R.Next(enumOptions.Length));
        }

        public Container GenerateContainer()
        {
            return new Container(R.Next(4, 30), RandomEnumValue<Enums.ContainerType>());
        }

        public Container GenerateContainer(int Weight, Enums.ContainerType Type)
        {
            return new Container(Weight, Type);
        }

        public List<Container> GenerateContainers(int amountOfContainers)
        {
            List<Container> containers = new List<Container>();
            for (int i = 0; i < amountOfContainers; i++)
            {
                containers.Add(GenerateContainer());
            }
            return containers;
        }
    }
}
