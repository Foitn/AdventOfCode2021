using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day15
{
    internal class PuzzleDay15 : Puzzle
    {
        private string[] example = new string[]
        {
            "1163751742",
            "1381373672",
            "2136511328",
            "3694931569",
            "7463417111",
            "1319128137",
            "1359912421",
            "3125421639",
            "1293138521",
            "2311944581",
        };

        public PuzzleDay15(string input) : base(input)
        {
        }

        //private int[,] grid;
        private List<int> risks = new();
        private int risk = int.MaxValue;
        private Node[,] nodes;

        public override void SolvePart1()
        {
            //Lines = example;
            this.nodes = new Node[this.Lines[0].Length, this.Lines.Length];
            for (int x = 0; x < this.Lines[0].Length; x++)
            {
                for (int y = 0; y < this.Lines.Length; y++)
                {
                    this.nodes[x, y] = new Node(int.Parse(this.Lines[x][y].ToString()), new Point(x, y));
                }
            }
            FindPath();
        }

        public override void SolvePart2()
        {
            //Lines = example;
            int multiply = 5;
            this.nodes = new Node[this.Lines[0].Length * multiply, this.Lines.Length * multiply];
            for (int x = 0; x < this.Lines[0].Length; x++)
            {
                for (int y = 0; y < this.Lines.Length; y++)
                {
                    for (int i = 0; i < multiply; i++)
                    {
                        for (int j = 0; j < multiply; j++)
                        {
                            int value;
                            if ((int.Parse(this.Lines[x][y].ToString()) + i + j) / 10 != 1)
                            {
                                value = int.Parse(this.Lines[x][y].ToString()) + i + j;
                            }
                            else
                            {
                                value = (int.Parse(this.Lines[x][y].ToString()) + i + j+1) % 10;
                            }
                            int newX = x + i * this.Lines[0].Length;
                            int newY = y + j * this.Lines.Length;
                            this.nodes[newX, newY] = new Node(value, new Point(newX, newY));
                        }
                    }
                }
            }
            FindPath();
        }


        private void FindPath()
        {
            int x = 0;
            int y = 0;
            Node currentNode = this.nodes[x, y];
            currentNode.DistanceValue = 0;
            List<Node> stackNodes = this.nodes.Cast<Node>().ToList();
            while (stackNodes.Count > 0)
            {
                if(stackNodes.Count % 100 == 0) Console.WriteLine($"Only {stackNodes.Count} to go");

                currentNode = stackNodes.MinBy(x => x.DistanceValue);
                stackNodes.Remove(currentNode);

                x = currentNode.Point.X;
                y = currentNode.Point.Y;
                
                if (x == this.nodes.GetLength(0)-1 && y == this.nodes.GetLength(1)-1)
                {
                    break;
                }

                ChangeDistanceValue(x + 1, y, currentNode, this.nodes);
                ChangeDistanceValue(x - 1, y, currentNode, this.nodes);
                ChangeDistanceValue(x, y + 1, currentNode, this.nodes);
                ChangeDistanceValue(x, y - 1, currentNode, this.nodes);

                currentNode.Visited = true;
            }
            //PrintIntArray(this.nodes);
            Console.WriteLine(this.nodes[this.nodes.GetLength(0) - 1, this.nodes.GetLength(1) - 1].DistanceValue);
        }

        private static Node? ChangeDistanceValue(int x, int y, Node currentNode, Node[,] nodes)
        {
            Node? returnable = null;
            if (IsInBounds(x, y, nodes))
            {
                returnable = nodes[x, y];
                int distanceValue = currentNode.DistanceValue + returnable.Value;
                if (distanceValue < returnable.DistanceValue)
                {
                    returnable.DistanceValue = distanceValue;
                }
            }
            return returnable;
        }

        private Node FindSmallestNode(params Node?[] nodes)
        {
            return nodes.Where(x => x != null && !x.Visited)?.OrderBy(x => x.DistanceValue)?.FirstOrDefault();
        }


        private static bool IsInBounds<T>(int x, int y, T[,] grid)
            => x < grid.GetLength(0) && x >= 0 && y < grid.GetLength(1) && y >= 0;

        private void PrintIntArray(Node[,] octopuses)
        {
            for (int i = 0; i < octopuses.GetLength(0); i++)
            {
                for (int j = 0; j < octopuses.GetLength(1); j++)
                {
                    Console.Write($"{octopuses[i, j].DistanceValue}\t");
                }
                Console.WriteLine();
            }
        }
    }
}

