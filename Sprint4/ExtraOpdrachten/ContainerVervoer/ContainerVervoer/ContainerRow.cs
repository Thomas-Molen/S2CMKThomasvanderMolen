using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerVervoer
{
    public class ContainerRow
    {
        public ContainerStack[] stacks { get; private set; }
        private List<ContainerStack> optimalStacks;

        public ContainerRow(int ShipWidth, Enums.ShipPosition Position)
        {
            stacks = new ContainerStack[ShipWidth];
            optimalStacks = new List<ContainerStack>();

            for (int i = 0; i < ShipWidth; i++)
            {
                stacks[i] = new ContainerStack(Position);
            }
        }

        public List<ContainerStack> GetOptimalStackOrder()
        {
            return optimalStacks = stacks.OrderBy(s => s.totalWeight).ToList();
        }

        public int GetTotalRowWeight()
        {
            return stacks.Sum(stack => stack.totalWeight);
        }

        public override string ToString()
        {
            string returnString = "row contains:\n\n";
            for (int i = 0; i < stacks.Length; i++)
            {
                returnString += stacks[i].ToString() + "\n\n";
            }
            return returnString;
        }
    }
}
