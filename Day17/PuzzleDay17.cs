using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day17
{
    internal class PuzzleDay17 : Puzzle
    {
        private string[] example = new string[]
        {
            "target area: x=20..30, y=-10..-5",
        };

        public PuzzleDay17(string input) : base(input) { }

        public override void SolvePart1()
        {
            //Lines = example;
            string targetAreaString = this.Lines[0];
            int minX = int.Parse(targetAreaString.Split('=')[1].Split("..")[0]);
            int maxX = int.Parse(targetAreaString.Split('=')[1].Split("..")[1].Split(",")[0]);
            int minY = int.Parse(targetAreaString.Split('=')[2].Split("..")[0]);
            int maxY = int.Parse(targetAreaString.Split('=')[2].Split("..")[1]);

            (int x, int y) maxXSpeed = (0, 0);
            (int x, int y) maxYSpeed = (0, 0);

            int highestYVal = int.MinValue;

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    (bool hitsTarget, int highestY) = this.WillHitTarget(i, j, minX, maxX, minY, maxY);
                    if (hitsTarget)
                    {
                        //Console.WriteLine($"Found it for {i},{j}");
                        if (i > maxXSpeed.x)
                        {
                            maxXSpeed = (i, j);
                        }
                        if (j > maxYSpeed.y)
                        {
                            maxYSpeed = (i, j);
                            highestYVal = highestY;
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"Did not find it for {i},{j}");
                    }
                }
            }

            Console.WriteLine($"Max x speed: {maxXSpeed.x},{maxXSpeed.y}");
            Console.WriteLine($"Max y speed: {maxYSpeed.x},{maxYSpeed.y}");
            Console.WriteLine($"Max Y height: {highestYVal}");
        }

        private (bool hitsTarget, int hightesY) WillHitTarget(int speedX, int speedY, int minX, int maxX, int minY, int maxY)
        {
            bool returnable = false;
            int x = 0;
            int y = 0;
            int maxHeightY = int.MinValue;
            while (x <= maxX && y >= minY)
            {
                x += speedX;
                y += speedY;
                switch (speedX)
                {
                    case > 0:
                        speedX--;
                        break;
                    case < 0:
                        speedX++;
                        break;
                }
                speedY--;

                if (y > maxHeightY) maxHeightY = y;

                if (x >= minX && y >= minY && x <= maxX && y <= maxY)
                {
                    returnable = true;
                    break;
                }
            }
            return (returnable, maxHeightY);
        }

        public override void SolvePart2()
        {
            //Lines = example;
            string targetAreaString = this.Lines[0];
            int minX = int.Parse(targetAreaString.Split('=')[1].Split("..")[0]);
            int maxX = int.Parse(targetAreaString.Split('=')[1].Split("..")[1].Split(",")[0]);
            int minY = int.Parse(targetAreaString.Split('=')[2].Split("..")[0]);
            int maxY = int.Parse(targetAreaString.Split('=')[2].Split("..")[1]);

            (int x, int y) maxXSpeed = (0, 0);
            (int x, int y) maxYSpeed = (0, 0);

            int highestYVal = int.MinValue;
            int numberOfHits = 0;

            for (int i = -1000; i < 1000; i++)
            {
                for (int j = -1000; j < 1000; j++)
                {
                    (bool hitsTarget, int highestY) = this.WillHitTarget(i, j, minX, maxX, minY, maxY);
                    if (hitsTarget)
                    {
                        numberOfHits++;
                        //Console.WriteLine($"Found it for {i},{j}");
                        if (i > maxXSpeed.x)
                        {
                            maxXSpeed = (i, j);
                        }
                        if (j > maxYSpeed.y)
                        {
                            maxYSpeed = (i, j);
                            highestYVal = highestY;
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"Did not find it for {i},{j}");
                    }
                }
            }

            Console.WriteLine($"Max x speed: {maxXSpeed.x},{maxXSpeed.y}");
            Console.WriteLine($"Max y speed: {maxYSpeed.x},{maxYSpeed.y}");
            Console.WriteLine($"Max Y height: {highestYVal}");
            Console.WriteLine($"Number of hits {numberOfHits}");
        }
    }
}

