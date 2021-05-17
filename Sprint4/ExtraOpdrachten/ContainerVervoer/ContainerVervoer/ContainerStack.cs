using ContainerVervoer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerVervoer
{
    public class ContainerStack
    {
        public List<Container> containers { get; private set; }
        public int totalWeight { get; private set; }
        private Enums.ShipPosition position;

        public ContainerStack(Enums.ShipPosition Position)
        {
            containers = new List<Container>();
            position = Position;
            totalWeight = 0;
        }

        public void AddContainer(Container container)
        {
            if (IsStackSuitableForContainer(container))
            {
                try
                {
                    if (WillStackBeSafe(container))
                    {
                        containers.Add(container);
                        totalWeight += container.weight;
                        return;
                    }
                }
                catch (ValueableContainerException)
                {
                    if (!IsStackTooHeavy() && container.type != Enums.ContainerType.Valueable)
                    {
                        containers.Insert(0, container);
                        totalWeight += container.weight;
                        return;
                    }
                }
                throw new AddContainerException("Container could not be added to stack");
            }
            throw new ValidPositionException("Container can not be added in this stack"); 
        }

        private bool IsContainerTooHeavy(Container container)
        {
            if (GetWeightHeadroom() >= container.weight)
            {
                return false;
            }
            return true;
        }

        private bool IsStackTooHeavy()
        {
            if (totalWeight >= 120)
            {
                return true;
            }
            return false;
        }

        private int GetWeightHeadroom()
        {
            if (containers.Count != 0)
            {
                return 120 - (totalWeight + -containers.FirstOrDefault().weight);
            }
            return 120;
        }

        private bool DoesStackHaveValueableContainer()
        {
            return containers.Exists(c => c.type == Enums.ContainerType.Valueable);
        }

        private bool WillStackBeSafe(Container container)
        {
            if (!IsContainerTooHeavy(container))
            {
                if (!DoesStackHaveValueableContainer())
                {
                    return true;
                }
                throw new ValueableContainerException("Can not add container on top of valueable container");
            }
            throw new AddContainerException("Container is too heavy to add");
        }

        private bool IsStackSuitableForContainer(Container container)
        {
            switch (container.type)
            {
                case Enums.ContainerType.Cooled:
                    if (position == Enums.ShipPosition.First)
                    {
                        return true;
                    }
                    break;
                case Enums.ContainerType.Valueable:
                    if (position == Enums.ShipPosition.First || position == Enums.ShipPosition.Last)
                    {
                        return true;
                    }
                    break;
                default:
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            string returnString = "Stack contains: " + containers.Count + " containers and weighs: " + totalWeight + "\n\n";
            List<Container> reversedContainers = containers;
            reversedContainers.Reverse();
            foreach (Container container in reversedContainers)
            {
                returnString += container.ToString() + " | ";
            }
            return returnString;
        }
    }
}
