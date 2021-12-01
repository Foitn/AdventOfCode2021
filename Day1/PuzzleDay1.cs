using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day1
{
    internal class PuzzleDay1 : Puzzle
    {
        public PuzzleDay1(string input) : base(input) { }

        public override void SolvePart1()
        {
            int[] numbers = Lines.Select(x => int.Parse(x)).ToArray();
            int amount = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (i == 0) continue;
                if (numbers[i] > numbers[i - 1]) amount++;
            }
            Console.WriteLine(amount);
        }
        public override void SolvePart2()
        {
            int[] numbers = Lines.Select(x => int.Parse(x)).ToArray();
            int amount = 0;
            for (int i = 3; i < numbers.Length; i++)
            {
                int sum1 = numbers[i - 3] + numbers[i - 2] + numbers[i - 1];
                int sum2 = numbers[i - 2] + numbers[i - 1] + numbers[i];
                if (sum2 > sum1) amount++;
            }
            Console.WriteLine(amount);
        }
    }
}
