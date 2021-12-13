using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day13
{
    internal class PuzzleDay13 : Puzzle
    {
        private string[] example = new string[] {
            "6,10",
            "0,14",
            "9,10",
            "0,3",
            "10,4",
            "4,11",
            "6,0",
            "6,12",
            "4,1",
            "0,13",
            "10,12",
            "3,4",
            "3,0",
            "8,4",
            "1,10",
            "2,14",
            "8,10",
            "9,0",
            "",
            "fold along y=7",
            "fold along x=5",
        };

        private List<string> folds = new();
        private List<Point> coordinates = new();
        private bool?[,] grid;

        public PuzzleDay13(string input) : base(input) { }



        public override void SolvePart1()
        {
            //this.Lines = this.example;
            this.Lines = this.Lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            this.folds = this.Lines.Where(x => x.Contains("fold along")).ToList();
            this.coordinates = this.Lines.Where(x => !x.Contains("fold along")).Select(x => new Point(int.Parse(x.Split(',')[0]), int.Parse(x.Split(',')[1]))).ToList();

            int maxX = this.coordinates.Max(x => x.Y);
            int maxY = this.coordinates.Max(x => x.X);

            this.grid = new bool?[maxX + 1, maxY + 1];

            foreach (Point coordinate in this.coordinates)
            {
                this.grid[coordinate.Y, coordinate.X] = true;
            }
            //PrintArray(this.grid);
            //Console.WriteLine();

            PerformFold(folds[0]);

            //PrintArray(this.grid);
            int numberOfDots = this.grid.Cast<bool?>().Count(x => x == true);
            Console.WriteLine($"Total number of dots {numberOfDots}");
        }


        public override void SolvePart2()
        {
            //this.Lines = this.example[2];
            this.Lines = this.Lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            this.folds = this.Lines.Where(x => x.Contains("fold along")).ToList();
            this.coordinates = this.Lines.Where(x => !x.Contains("fold along")).Select(x => new Point(int.Parse(x.Split(',')[0]), int.Parse(x.Split(',')[1]))).ToList();

            int maxX = this.coordinates.Max(x => x.Y);
            int maxY = this.coordinates.Max(x => x.X);

            this.grid = new bool?[maxX + 1, maxY + 1];

            foreach (Point coordinate in this.coordinates)
            {
                this.grid[coordinate.Y, coordinate.X] = true;
            }
            //PrintArray(this.grid);
            //Console.WriteLine();
            foreach (string fold in folds)
            {
                PerformFold(fold);
            }

            //PrintArray(this.grid, 25);
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Console.Write(grid[i, j] == null ? "." : grid[i, j] == true ? "#" : "-");
                }
                Console.WriteLine();
            }
            int numberOfDots = this.grid.Cast<bool?>().Count(x => x == true);
            Console.WriteLine($"Total number of dots {numberOfDots}");
        }

        public void PerformFold(string fold)
        {
            fold = fold.Split(' ')[2];
            string direction = fold.Split('=')[0];
            int lineNumber = int.Parse(fold.Split('=')[1]);

            if (direction == "x")
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    for (int y = 0; y < grid.GetLength(1); y++)
                    {
                        if (y == lineNumber)
                        {
                            this.grid[x, y] = false;
                        }
                        else if (y > lineNumber)
                        {
                            if (this.grid[x, y] == true)
                            {
                                this.grid[x, lineNumber - (y - lineNumber)] = true;
                                this.grid[x, y] = null;
                            }
                        }
                    }
                }
            }
            else if (direction == "y")
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    for (int y = 0; y < grid.GetLength(1); y++)
                    {
                        if (x == lineNumber)
                        {
                            this.grid[x, y] = false;
                        }
                        else if (x > lineNumber)
                        {
                            if (this.grid[x, y] == true)
                            {
                                this.grid[lineNumber - (x - lineNumber), y] = true;
                                this.grid[x, y] = null;
                            }
                        }
                    }
                }
            }
        }

        private void PrintArray(bool?[,] grid, int maxX = -1)
        {
            for (int i = 0; i < (maxX == -1 ? grid.GetLength(0) : maxX); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j] == null ? "." : grid[i, j] == true ? "#" : "-");
                }
                Console.WriteLine();
            }
        }

        private bool IsInBounds(int x, int y, bool?[,] grid)
            => x < grid.GetLength(0) && x >= 0 && y < grid.GetLength(1) && y >= 0;
    }
}

