using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day3
{
    internal class PuzzleDay3 : Puzzle
    {
        private string gammaString;
        private string epsilonString;
        private string oxygenString;
        private string co2String;
        private int gamma, epsilon, oxygen, co2;

        public PuzzleDay3(string inputPath) : base(inputPath) { }

        public override void SolvePart1()
        {
            for (int i = 0; i < Lines[0].Length; i++)
            {
                (int mostCommon, int leastCommon) = MostCommon(Lines, i);
                gammaString += mostCommon.ToString();
                epsilonString += leastCommon.ToString();
            }
            gamma = Convert.ToInt32(gammaString, 2);
            epsilon = Convert.ToInt32(epsilonString, 2);
            Console.WriteLine($"Gamma:\t{gamma}");
            Console.WriteLine($"Epsilon:\t{epsilon}");
            Console.WriteLine($"Total:\t{gamma*epsilon}");
        }

        public override void SolvePart2()
        {
            string[] oxygenStrings = FindMostCommon(Lines, 0);
            string[] co2Strings = FindLeastCommon(Lines, 0);
            oxygenString = oxygenStrings[0];
            co2String = co2Strings[0];
            oxygen = Convert.ToInt32(oxygenString, 2);
            co2 = Convert.ToInt32(co2String, 2);
            Console.WriteLine($"Oxygen:\t{oxygen}");
            Console.WriteLine($"CO2:\t{co2}");
            Console.WriteLine($"Total:\t{oxygen * co2}");
        }

        private (int mostCommon, int leastCommon) MostCommon(string[] input, int index)
        {
            int zeros = 0;
            int ones = 0;
            foreach (string line in input)
            {
                char c = line[index];
                if (c == '1')
                {
                    ones++;
                }
                else if (c == '0')
                {
                    zeros++;
                }
                else
                {
                    throw new Exception("Invalid char");
                }
            }
            int mostCommon = ones >= zeros ? 1 : 0;
            int leastCommon = ones >= zeros ? 0 : 1;
            return (mostCommon, leastCommon);
        }

        private string[] FindMostCommon(string[] input, int index)
        {
            (int mostCommon, int leastCommon) = MostCommon(input, index);
            string[] result = input.Where(x => x[index] == $"{mostCommon}"[0]).ToArray();
            if(result.Length == 1)
            {
                return result;
            }
            else
            {
                return FindMostCommon(result, index + 1);
            }
        }
        private string[] FindLeastCommon(string[] input, int index)
        {
            (int mostCommon, int leastCommon) = MostCommon(input, index);
            string[] result = input.Where(x => x[index] == $"{leastCommon}"[0]).ToArray();
            if (result.Length == 1)
            {
                return result;
            }
            else
            {
                return FindLeastCommon(result, index + 1);
            }
        }
    }
}
