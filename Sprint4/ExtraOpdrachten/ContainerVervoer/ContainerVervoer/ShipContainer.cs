using ContainerVervoer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class ShipContainer
    {
        public Ship ship;
        public List<Container> containers;
        public List<Container> containersLeftOver;
        public ShipContainer(int Capacity, int Length, int Width)
        {
            ship = new Ship(Capacity, Length, Width);
            containers = new List<Container>();
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
                        Console.WriteLine("I GOT STUCK AND YEETED MYSELF");
                        containersLeftOver.Add(container);
                        containers.Remove(container);
                        continue;
                    }
                }
            }
        }

        public override string ToString()
        {
            return ship.ToString();
        }
    }
}
