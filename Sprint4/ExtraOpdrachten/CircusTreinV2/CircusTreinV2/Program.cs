using System;

namespace CircusTreinV2
{
    class Program
    {
        static void Main(string[] args)
        {
            WagonContainer train = new WagonContainer();
            AnimalGenerator animalGenerator = new AnimalGenerator();
            train.AddAnimals(animalGenerator.GenerateAnimals(20));
            train.FillWagons();

            Console.WriteLine(train.ToString());
        }
    }
}
