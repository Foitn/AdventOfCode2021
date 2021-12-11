using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day11
{
    internal class PuzzleDay11 : Puzzle
    {
        private string[] example = new string[] {
                "5483143223",
                "2745854711",
                "5264556173",
                "6141336146",
                "6357385478",
                "4167524645",
                "2176841721",
                "6882881134",
                "4846848554",
                "5283751526",
            };

        public PuzzleDay11(string input) : base(input) { }

        public override void SolvePart1()
        {
            //Lines = example;
            int[,] octopuses = this.LinesToOctopuses();
            PrintIntArray(octopuses);
            int numberOfFlashes = 0;
            for (int i = 0; i < 100; i++)
            {
                numberOfFlashes += PerformStep(octopuses);
                Console.WriteLine($"Working on step {i + 1}, number of flashes is {numberOfFlashes}");
            }

            Console.WriteLine($"Total number of flashes: {numberOfFlashes}");
        }



        public override void SolvePart2()
        {
            //Lines = example;
            int[,] octopuses = this.LinesToOctopuses();
            PrintIntArray(octopuses);
            int numberOfFlashes = 0;
            int firstWhereAllFlashTogether = -1;
            for (int i = 0; ; i++)
            {
                numberOfFlashes += PerformStep(octopuses);
                Console.WriteLine($"Working on step {i + 1}, number of flashes is {numberOfFlashes}");
                if (octopuses.Cast<int>().All(x => x == 0))
                {
                    firstWhereAllFlashTogether = i+1;
                    break;
                }
            }

            Console.WriteLine($"Total number of flashes: {numberOfFlashes}");
            Console.WriteLine($"First round where all flash together: {firstWhereAllFlashTogether}");
        }

        public int PerformStep(int[,] octopuses)
        {
            int numberOfFlashes = 0;
            for (int i = 0; i < octopuses.GetLength(0); i++)
            {
                for (int j = 0; j < octopuses.GetLength(1); j++)
                {
                    octopuses[i, j]++;
                }
            }

            while (octopuses.Cast<int>().Any(x => x > 9))
            {
                for (int i = 0; i < octopuses.GetLength(0); i++)
                {
                    for (int j = 0; j < octopuses.GetLength(1); j++)
                    {
                        if (octopuses[i, j] > 9)
                        {
                            numberOfFlashes++;
                            octopuses[i, j] = int.MinValue;
                            for (int deltaX = -1; deltaX <= 1; deltaX++)
                            {
                                for (int deltaY = -1; deltaY <= 1; deltaY++)
                                {
                                    if (deltaY == 0 && deltaX == 0) continue;
                                    UpdateNeighbor(i + deltaX, j + deltaY, octopuses);
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < octopuses.GetLength(0); i++)
            {
                for (int j = 0; j < octopuses.GetLength(1); j++)
                {
                    if (octopuses[i, j] < 0)
                    {
                        octopuses[i, j] = 0;
                    }
                }
            }
            return numberOfFlashes;
        }

        private void UpdateNeighbor(int x, int y, int[,] grid)
        {
            if (IsInBounds(x, y, grid))
            {
                grid[x, y]++;
            }
        }

        private int[,] LinesToOctopuses()
        {
            int[,] octopuses = new int[this.Lines[0].Length, this.Lines.Length];
            for (int i = 0; i < octopuses.GetLength(0); i++)
            {
                for (int j = 0; j < octopuses.GetLength(1); j++)
                {
                    octopuses[i, j] = int.Parse(this.Lines[i][j].ToString());
                }
            }

            return octopuses;
        }


        private void PrintIntArray(int[,] octopuses)
        {
            for (int i = 0; i < octopuses.GetLength(0); i++)
            {
                for (int j = 0; j < octopuses.GetLength(1); j++)
                {
                    Console.Write(octopuses[i, j]);
                }
                Console.WriteLine();
            }
        }

        private bool IsInBounds(int x, int y, int[,] grid)
            => x < grid.GetLength(0) && x >= 0 && y < grid.GetLength(1) && y >= 0;
    }
}

