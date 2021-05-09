using System;

namespace ContainerVervoer
{
    class Program
    {
        static void Main(string[] args)
        {
            ShipContainer shipContainer = new ShipContainer(100000, 4, 4);
            ContainerGenerator containerGenerator = new ContainerGenerator();

            shipContainer.AddContainers(containerGenerator.GenerateContainers(200));
            shipContainer.FillShip();
            Console.WriteLine(shipContainer.ToString());
            while (true)
            {

            }
        }
    }
}
