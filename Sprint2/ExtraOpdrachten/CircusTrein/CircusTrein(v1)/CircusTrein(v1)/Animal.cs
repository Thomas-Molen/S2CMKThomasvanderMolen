using System;
using System.Collections.Generic;
using System.Text;

namespace CircusTrein_v1_
{
    public class Animal
    {
        public AnimalFood eats { get; set; }
        public AnimalSize size { get; set; }

        public Animal(AnimalFood Eats, AnimalSize Size)
        {
            eats = Eats;
            size = Size;
        }
    }
}
