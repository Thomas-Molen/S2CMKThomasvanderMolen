using ContainerVervoer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class Container
    {
        public int weight { get; private set; }
        public ContainerType type { get; private set; }

        public Container(int Weight, ContainerType Type)
        {
            weight = Weight;
            type = Type;
        }

        public override string ToString()
        {
            return type + " container , weighs: " + weight + " tons";
        }
    }
}
