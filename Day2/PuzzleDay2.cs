using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day2
{
    internal class PuzzleDay2 : Puzzle
    {
        private int depth;
        private int horizontal;
        private int aim;

        public PuzzleDay2(string inputPath) : base(inputPath) { }

        public override void SolvePart1()
        {
            IEnumerable<(EDirection direction, int length)> values = ParseList();
            foreach ((EDirection direction, int length) in values)
            {
                switch (direction)
                {
                    case EDirection.Forward:
                        horizontal += length;
                        break;
                    case EDirection.Backward:
                        horizontal -= length;
                        break;
                    case EDirection.Up:
                        depth -= length;
                        break;
                    case EDirection.Down:
                        depth += length;
                        break;
                }
            }
            int resultingVector = Math.Abs(horizontal)*Math.Abs(depth);
            Console.WriteLine(resultingVector);
        }

        public override void SolvePart2()
        {
            IEnumerable<(EDirection direction, int length)> values = ParseList();
            foreach ((EDirection direction, int length) in values)
            {
                switch (direction)
                {
                    case EDirection.Forward:
                        horizontal += length;
                        depth += aim * length;
                        break;
                    case EDirection.Backward:
                        horizontal -= length;
                        depth -= aim * length;
                        break;
                    case EDirection.Up:
                        aim -= length;
                        break;
                    case EDirection.Down:
                        aim += length;
                        break;
                }
            }
            int resultingVector = Math.Abs(horizontal) * Math.Abs(depth);
            Console.WriteLine(resultingVector);
        }

        private IEnumerable<(EDirection direction, int length)> ParseList()
        {
            foreach (string line in Lines)
            {
                string[] values = line.Split(' ');
                values[0] = char.ToUpper(values[0][0]) + values[0].Substring(1);
                EDirection direction = (EDirection)Enum.Parse(typeof(EDirection), values[0]);
                int length = int.Parse(values[1]);
                yield return (direction, length);
            }
        }
    }
}
