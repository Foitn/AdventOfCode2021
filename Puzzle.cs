using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal abstract class Puzzle
    {
        protected string[] Lines { get; set; }
        public Puzzle(string inputPath)
        {
            Lines = File.ReadAllLines(inputPath);
        }

        public virtual void SolvePart1() { }
        public virtual void SolvePart2() { }
        public virtual Task SolvePart1Async() { return Task.CompletedTask; }
        public virtual Task SolvePart2Async() { return Task.CompletedTask; }
    }
}
