using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircusTrein_v1_
{
    public class AnimalDistrubutor
    {
        WagonContainer wagonContainer = new WagonContainer();
        List<Animal> RemoveableAnimals = new List<Animal>();
        List<Animal> animals;
        public List<Wagon> Distribute(AnimalContainer animalContainer)
        {
            animals = animalContainer.GetAllAnimals();
            SortLargeMeatEaters(animals);
            SortMeatEaterWagon(animals);
            SortPlantEaterWagon(animals);

            return wagonContainer.GetAllWagons();
        }

        private void SortLargeMeatEaters(List<Animal> Animals)
        {
            foreach (var animal in Animals)
            {
                if (animal.size == AnimalSize.Large && animal.eats == AnimalFood.Meat)
                {
                    var newWagon = wagonContainer.CreateWagon();
                    wagonContainer.AddAnimal(newWagon, animal);
                    RemoveableAnimals.Add(animal);
                    wagonContainer.AddWagon(newWagon);
                }
            }
            ClearRemoveableAnimals();
        }

        private void SortMeatEaterWagon(List<Animal> Animals)
        {
            foreach (var animal in Animals.ToList())
            {
                if (animal.eats == AnimalFood.Meat)
                {
                    var newWagon = wagonContainer.CreateWagon();
                    wagonContainer.AddAnimal(newWagon, animal);
                    RemoveableAnimals.Add(animal);

                    FillMeatWagon(newWagon, Animals);
                    wagonContainer.AddWagon(newWagon);
                }
            }
        }

        private void FillMeatWagon(Wagon WagonToFill, List<Animal> Animals)
        {
            foreach (var meatAnimal in WagonToFill.GetAllAnimals().ToList())
            {
                if (meatAnimal.eats == AnimalFood.Meat)
                {
                    switch (meatAnimal.size)
                    {
                        case AnimalSize.Small:
                            foreach (var animal in Animals)
                            {
                                if (animal.eats == AnimalFood.Plant)
                                {
                                    if (WagonToFill.capacity > 4 && animal.size == AnimalSize.Large)
                                    {
                                        wagonContainer.AddAnimal(WagonToFill, animal);
                                        RemoveableAnimals.Add(animal);
                                    }
                                    else if (WagonToFill.capacity > 2 && animal.size == AnimalSize.Medium)
                                    {
                                        wagonContainer.AddAnimal(WagonToFill, animal);
                                        RemoveableAnimals.Add(animal);
                                    }
                                }
                            }
                            break;  
                        case AnimalSize.Medium:
                            foreach (var animal in Animals)
                            {
                                if (animal.eats == AnimalFood.Plant)
                                {
                                    if (WagonToFill.capacity > 4 && animal.size == AnimalSize.Large)
                                    {
                                        wagonContainer.AddAnimal(WagonToFill, animal);
                                        RemoveableAnimals.Add(animal);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            
            ClearRemoveableAnimals();
        }

        private void SortPlantEaterWagon(List<Animal> AnimalsToSort)
        {
            var newWagon = wagonContainer.CreateWagon();
            foreach (var animal in AnimalsToSort)
            {
                if (animal.eats == AnimalFood.Plant)
                {
                    if (newWagon.capacity > 4 && animal.size == AnimalSize.Large)
                    {
                        wagonContainer.AddAnimal(newWagon, animal);
                        RemoveableAnimals.Add(animal);
                    }
                    else if (newWagon.capacity > 2 && animal.size == AnimalSize.Medium)
                    {
                        wagonContainer.AddAnimal(newWagon, animal);
                        RemoveableAnimals.Add(animal);
                    }
                    else if (newWagon.capacity > 0 && animal.size == AnimalSize.Small)
                    {
                        wagonContainer.AddAnimal(newWagon, animal);
                        RemoveableAnimals.Add(animal);
                    }
                    else
                    {
                        wagonContainer.AddWagon(newWagon);
                        newWagon = wagonContainer.CreateWagon();
                        wagonContainer.AddAnimal(newWagon, animal);
                    }
                }
            }
            wagonContainer.AddWagon(newWagon);
            ClearRemoveableAnimals();
        }

        private void ClearRemoveableAnimals()
        {
            foreach (var animalToRemove in RemoveableAnimals)
            {
                animals.Remove(animalToRemove);
            }
            RemoveableAnimals.Clear();
        }
    }
}
