using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerVervoer
{
    public class Ship
    {
        public ContainerRow[] rows { get; private set; }
        private List<ContainerRow> optimalRows;
        public int capacity { get; private set; }
        public int length { get; private set; }
        public int width { get; private set; }

        public Ship(int Capacity, int Length, int Width)
        {
            capacity = Capacity;
            length = Length;
            width = Width;
            rows = new ContainerRow[Length];
            optimalRows = new List<ContainerRow>();

            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                {
                    rows[i] = new ContainerRow(Width, Enums.ShipPosition.First);
                }
                else if (i == (length - 1))
                {
                    rows[i] = new ContainerRow(Width, Enums.ShipPosition.Last);
                }
                else
                {
                    rows[i] = new ContainerRow(Width, Enums.ShipPosition.Middle);
                }
            }
        }

        public List<ContainerRow> GetOptimalRowOrder()
        {
            return optimalRows = rows.OrderBy(r => r.GetTotalRowWeight()).ToList();
        }

        public bool WillContainerFit(Container container)
        {
            if (rows.Sum(r => r.GetTotalRowWeight()) + container.weight < capacity)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            string returnString = "ship contains:\n\n";
            for (int i = 0; i < rows.Length; i++)
            {
                returnString += rows[i].ToString() + "\n\n";
            }
            return returnString;
        }
    }
}
