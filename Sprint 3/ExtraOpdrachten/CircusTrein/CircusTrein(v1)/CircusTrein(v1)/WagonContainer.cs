using System;
using System.Collections.Generic;
using System.Text;

namespace CircusTrein_v1_
{
    public class WagonContainer
    {
        List<Wagon> wagons;

        public WagonContainer()
        {
            wagons = new List<Wagon>();
        }
        
        public void AddWagon(Wagon NewWagon)
        {
            wagons.Add(NewWagon);
        }

        public Wagon CreateWagon()
        {
            return new Wagon();
        }

        public List<Wagon> GetAllWagons()
        {
            return wagons;
        }

        public void AddAnimal(Wagon WagonToEdit, Animal AnimalToAdd)
        {
            if (AnimalToAdd.eats == AnimalFood.Meat && AnimalToAdd.size == AnimalSize.Large)
            {
                WagonToEdit.UpdateCapacity(-10);
            }
            else
            {
                switch (AnimalToAdd.size)
                {
                    case AnimalSize.Small:
                        WagonToEdit.UpdateCapacity(-1);
                        break;
                    case AnimalSize.Medium:
                        WagonToEdit.UpdateCapacity(-3);
                        break;
                    case AnimalSize.Large:
                        WagonToEdit.UpdateCapacity(-5);
                        break;
                }
            }
            WagonToEdit.AddAnimal(AnimalToAdd);
        }
    }
}
