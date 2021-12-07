using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day7
{
    internal class PuzzleDay7 : Puzzle
    {
        public PuzzleDay7(string input) : base(input) { }

        public override void SolvePart1()
        {
            int[] positions = Lines[0].Split(',').Select(int.Parse).ToArray();
            int minPosition = positions.Min();
            int maxPosition = positions.Max();
            int amountOfFuel = -1;
            int position = -1;
            for (int i = minPosition; i < maxPosition; i++)
            {
                Console.WriteLine($"Currently looking at {i}, total is {maxPosition}");
                int localAmountOfFuel = AmountOfFuelToGetTo(i, positions);
                if (amountOfFuel == -1 || localAmountOfFuel < amountOfFuel)
                {
                    amountOfFuel = localAmountOfFuel;
                    position = i;
                }
            }
            Console.WriteLine($"Total fuel needed is {amountOfFuel}, to get to {position}");
        }

        public override void SolvePart2()
        {
            int[] positions = Lines[0].Split(',').Select(int.Parse).ToArray();
            int minPosition = positions.Min();
            int maxPosition = positions.Max();
            int amountOfFuel = -1;
            int position = -1;
            for (int i = minPosition; i < maxPosition; i++)
            {
                Console.WriteLine($"Currently looking at {i}, total is {maxPosition}");
                int localAmountOfFuel = AmountOfFuelToGetToIncrementing(i, positions);
                if (amountOfFuel == -1 || localAmountOfFuel < amountOfFuel)
                {
                    amountOfFuel = localAmountOfFuel;
                    position = i;
                }
            }
            Console.WriteLine($"Total fuel needed is {amountOfFuel}, to get to {position}");
        }

        private int AmountOfFuelToGetTo(int goal, int[] currentlocations)
        {
            return currentlocations.Sum(x => Math.Abs(x - goal));
        }

        private int AmountOfFuelToGetToIncrementing(int goal, int[] currentLocations)
        {
            int amountOfFuel = 0;
            for (int i = 0; i < currentLocations.Length; i++)
            {
                for (int j = 1; j <= Math.Abs(currentLocations[i] - goal); j++)
                {
                    amountOfFuel += j;
                }
            }
            return amountOfFuel;
        }
    }
}
