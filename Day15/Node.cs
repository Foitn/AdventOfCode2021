using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day15
{
    internal class Node
    {
        public int Value { get; set; }
        public int DistanceValue { get; set; } = int.MaxValue;
        public bool Visited { get; set; }
        public Point Point { get; set; }

        public Node(int value, Point point)
        {
            this.Value = value;
            this.Point = point;
        }
    }
}
