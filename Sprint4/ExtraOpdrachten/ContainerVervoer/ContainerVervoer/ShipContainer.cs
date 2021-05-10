using ContainerVervoer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class ShipContainer
    {
        private Ship ship;
        private List<Container> containers;
        private List<Container> containersLeftOver;
        public ShipContainer()
        {
            containers = new List<Container>();
        }

        public void CreateShip(int Capacity, int Length, int Width)
        {
            ship = new Ship(Capacity, Length, Width);
        }

        public void AddContainer(Container container)
        {
            containers.Add(container);
        }

        public void AddContainers(List<Container> Containers)
        {
            foreach (Container container in Containers)
            {
                AddContainer(container);
            }
        }

        public void FillShip()
        {
            containersLeftOver = new List<Container>();
            while (containers.Count != 0)
            {
                foreach (Container container in containers.ToArray())
                {
                    Console.WriteLine(containers.Count + "Left");
                    if (!ship.WillContainerFit(container))
                    {
                        ContainerDoesNotFit(container);
                        continue;
                    }
                    foreach (ContainerRow row in ship.GetOptimalRowOrder())
                    {
                        foreach (ContainerStack stack in row.GetOptimalStackOrder())
                        {
                            try
                            {
                                stack.AddContainer(container);
                                containers.Remove(container);
                                break;
                            }
                            catch (ValidPositionException)
                            {
                                break;
                            }
                            catch (AddContainerException)
                            {
                                continue;
                            }
                        }
                        if (!containers.Contains(container))
                        {
                            break;
                        }
                    }
                    if (containers.Contains(container))
                    {
                        ContainerDoesNotFit(container);
                        continue;
                    }
                }
            }
        }

        private void ContainerDoesNotFit(Container container)
        {
            Console.WriteLine("Container could not fit on ship");
            containersLeftOver.Add(container);
            containers.Remove(container);
        }

        public override string ToString()
        {
            return ship.ToString() + "\n\n with: " + containersLeftOver.Count + " containers left over";
        }
    }
}
