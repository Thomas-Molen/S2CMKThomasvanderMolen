using System;

namespace ContainerVervoer
{
    class Program
    {
        static void Main(string[] args)
        {
            ShipContainer shipContainer = new ShipContainer();

            Console.WriteLine("Please give the needed values to setup the ship in number.");
            Console.WriteLine("What is the ship's max carrying capacity in tons?");
            int capacity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many containers can the ship carry behind eachother?");
            int length = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many containers can the ship carry next to eachother");
            int width = Convert.ToInt32(Console.ReadLine());

            shipContainer.CreateShip(capacity, length, width);
            ContainerGenerator containerGenerator = new ContainerGenerator();

            Console.WriteLine("How many containers do you wish to generate?");
            int containers = Convert.ToInt32(Console.ReadLine());
            shipContainer.AddContainers(containerGenerator.GenerateContainers(containers));
            shipContainer.FillShip();
            Console.WriteLine(shipContainer.ToString());
        }
    }
}
