using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day12
{
    internal class Path
    {
        public List<Cave> Caves { get; private init; } = new();
        public override string ToString()
        {
            return string.Join(',', this.Caves.Select(x => x.Title));
        }

        public Path Clone()
        {
            return new Path() { Caves = this.Caves.ToList() };
        }

        public void Add(Cave cave) => this.Caves.Add(cave);
        public void AddRange(IEnumerable<Cave> caves) => this.Caves.AddRange(caves);


    }
}
