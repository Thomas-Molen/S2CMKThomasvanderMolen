using System;

namespace CircusTrein_v1_
{
    class Program
    {
        static void Main(string[] args)
        {
            AnimalContainer container = new AnimalContainer();
            CreateAnimals(container);

            var result = new AnimalDistrubutor().Distribute(container);

            int wagonCount = 0;
            foreach (var wagon in result)
            {
                wagonCount++;
                Console.WriteLine("Wagon " + wagonCount + " contains:");
                foreach (var animal in wagon.GetAllAnimals())
                {
                    Console.WriteLine(animal.size + " sized " + animal.eats + " eater");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Amount of wagons: " + result.Count);
        }

        private static void CreateAnimals(AnimalContainer container)
        {
            for (int i = 0; i < 3; i++)
            {
                container.AddAnimal(new Animal(AnimalFood.Meat, AnimalSize.Large));
            }
            for (int i = 0; i < 1; i++)
            {
                container.AddAnimal(new Animal(AnimalFood.Meat, AnimalSize.Medium));
            }
            for (int i = 0; i < 3; i++)
            {
                container.AddAnimal(new Animal(AnimalFood.Meat, AnimalSize.Small));
            }
            for (int i = 0; i < 3; i++)
            {
                container.AddAnimal(new Animal(AnimalFood.Plant, AnimalSize.Large));
            }
            for (int i = 0; i < 6; i++)
            {
                container.AddAnimal(new Animal(AnimalFood.Plant, AnimalSize.Medium));
            }
            for (int i = 0; i < 4; i++)
            {
                container.AddAnimal(new Animal(AnimalFood.Plant, AnimalSize.Small));
            }
        }
    }
}
