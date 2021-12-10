using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day9
{
    internal class PuzzleDay9 : Puzzle
    {
        private string[] example = new string[] {
                "2199943210",
                "3987894921",
                "9856789892",
                "8767896789",
                "9899965678"
            };

        public PuzzleDay9(string input) : base(input) { }

        public override void SolvePart1()
        {
            //Lines = example;
            int[,] depthMap = this.LinesToDepthMap();
            int totalRiskLevel = 0;
            for (int i = 0; i < depthMap.GetLength(0); i++)
            {
                for (int j = 0; j < depthMap.GetLength(1); j++)
                {
                    int value = depthMap[i, j];
                    if (CheckAllNabors(value, i, j, depthMap))
                    {
                        Console.WriteLine($"Adding value {value} which is located at {i},{j}");
                        totalRiskLevel += value + 1;
                    }
                }
            }
            Console.WriteLine($"Total risk level: {totalRiskLevel}");
        }

        public override void SolvePart2()
        {
            //Lines = example;
            int[,] depthMap = this.LinesToDepthMap();
            List<(int x, int y)> lowestPoints = new List<(int x, int y)>();
            for (int i = 0; i < depthMap.GetLength(0); i++)
            {
                for (int j = 0; j < depthMap.GetLength(1); j++)
                {
                    int value = depthMap[i, j];
                    if (CheckAllNabors(value, i, j, depthMap))
                    {
                        lowestPoints.Add((i, j));
                    }
                }
            }
            List<int> sizesOfBasins = new List<int>();
            foreach ((int x, int y) in lowestPoints)
            {
                int basinSize = FloodFillGetSize(x, y, depthMap);
                Console.WriteLine($"Adding basin {x},{y} of size {basinSize}");
                sizesOfBasins.Add(basinSize);
            }
            List<int> largestSizes = sizesOfBasins.OrderByDescending(x => x).Take(3).ToList();
            Console.WriteLine($"Total size of basins is {largestSizes.Aggregate((a, x) => a * x)}");
        }

        private int FloodFillGetSize(int inputX, int inputY, int[,] depthMap)
        {
            int size = 0;
            Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
            List<(int x, int y)> visitedPoints = new List<(int x, int y)>() { (inputX, inputY) };
            stack.Push((inputX, inputY));

            while (stack.Count > 0)
            {
                (int x, int y) = stack.Pop();
                if (depthMap[x, y] != 9)
                {
                    size++;
                    AddToStackIfNotAlreadyVisited(x - 1, y, visitedPoints, stack, depthMap);
                    AddToStackIfNotAlreadyVisited(x + 1, y, visitedPoints, stack, depthMap);
                    AddToStackIfNotAlreadyVisited(x, y - 1, visitedPoints, stack, depthMap);
                    AddToStackIfNotAlreadyVisited(x, y + 1, visitedPoints, stack, depthMap);
                }
            }

            return size;
        }

        private void AddToStackIfNotAlreadyVisited(int x, int y, List<(int x, int y)> visitedPoints, Stack<(int x, int y)> theStack, int[,] depthMap)
        {
            if (!visitedPoints.Any(z => z.x == x && z.y == y)
                && !IsOutOfBounds(x, y, depthMap))
            {
                theStack.Push((x, y));
                visitedPoints.Add((x, y));
            }
        }

        private bool CheckAllNabors(int value, int x, int y, int[,] depthMap)
        {
            bool returnable = true;
            returnable = returnable && CheckNabor(value, x - 1, y, depthMap);
            returnable = returnable && CheckNabor(value, x + 1, y, depthMap);
            returnable = returnable && CheckNabor(value, x, y - 1, depthMap);
            returnable = returnable && CheckNabor(value, x, y + 1, depthMap);
            return returnable;
        }

        private bool CheckNabor(int value, int x, int y, int[,] depthMap)
        {
            bool returnable;
            if (IsOutOfBounds(x, y, depthMap))
            {
                returnable = true;
            }
            else
            {
                returnable = value < depthMap[x, y];
            }
            return returnable;
        }

        private bool IsOutOfBounds(int x, int y, int[,] depthMap)
            => x >= depthMap.GetLength(0) || x < 0 || y >= depthMap.GetLength(1) || y < 0;

        private int[,] LinesToDepthMap()
        {
            int[,] depthMap = new int[Lines.Length, Lines[0].Length];
            for (int i = 0; i < Lines.Length; i++)
            {
                for (int j = 0; j < Lines[0].Length; j++)
                {
                    depthMap[i, j] = int.Parse(Lines[i][j].ToString());
                }
            }
            return depthMap;
        }
    }
}

