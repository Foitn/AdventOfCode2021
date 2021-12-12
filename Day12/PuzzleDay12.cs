using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day12
{
    internal class PuzzleDay12 : Puzzle
    {
        private string[][] example = {
            new []
            {
                "start-A",
                "start-b",
                "A-c",
                "A-b",
                "b-d",
                "A-end",
                "b-end",
            },
            new []{
                "dc-end",
                "HN-start",
                "start-kj",
                "dc-start",
                "dc-HN",
                "LN-dc",
                "HN-end",
                "kj-sa",
                "kj-HN",
                "kj-dc",
            },
            new []{
                "fs-end",
                "he-DX",
                "fs-he",
                "start-DX",
                "pj-DX",
                "end-zg",
                "zg-sl",
                "zg-pj",
                "pj-he",
                "RW-he",
                "fs-DX",
                "pj-RW",
                "zg-RW",
                "start-pj",
                "he-WI",
                "zg-he",
                "pj-fs",
                "start-RW",
            }};

        private readonly List<Cave> caves = new();
        private readonly List<Path> paths = new();
        private Cave startCave, endCave;

        public PuzzleDay12(string input) : base(input) { }

        public override void SolvePart1()
        {
            //this.Lines = this.example[2];
            this.ConvertLinesToCaves();
            startCave = this.caves.Single(x => x.IsStartNode);
            endCave = this.caves.Single(x => x.IsEndNode);
            FindPath(new Path(), this.startCave, true);
            Console.WriteLine($"Number of paths = {this.paths.Count}");
        }


        public override void SolvePart2()
        {
            //this.Lines = this.example[2];
            this.ConvertLinesToCaves();
            startCave = this.caves.Single(x => x.IsStartNode);
            endCave = this.caves.Single(x => x.IsEndNode);
            FindPath(new Path(), this.startCave, false);
            Console.WriteLine($"Number of paths = {this.paths.Count}");
        }

        public void FindPath(Path beginPath, Cave nextCave, bool visitedTwice)
        {
            if (!visitedTwice && beginPath.Caves.Contains(nextCave) && !nextCave.IsLargeCave)
            {
                visitedTwice = true;
            }
            beginPath.Add(nextCave);
            if (nextCave.Equals(this.endCave))
            {
                this.paths.Add(beginPath);
            }
            else
            {
                foreach (Cave child in nextCave.ConnectedCaves)
                {
                    if (!child.Equals(this.startCave) && (child.IsLargeCave || !beginPath.Caves.Contains(child)|| !visitedTwice))
                    {
                        this.FindPath(beginPath.Clone(), child, visitedTwice);
                    }
                }
            }
        }

        private void ConvertLinesToCaves()
        {
            foreach (string line in this.Lines)
            {
                string from = line.Split('-')[0];
                string to = line.Split('-')[1];
                Cave? fromCave = this.caves.FirstOrDefault(x => x.Title == @from);
                Cave? toCave = this.caves.FirstOrDefault(x => x.Title == to);
                if (fromCave is null)
                {
                    fromCave = new Cave(@from);
                    this.caves.Add(fromCave);
                }

                if (toCave is null)
                {
                    toCave = new Cave(to);
                    this.caves.Add(toCave);
                }

                fromCave.ConnectedCaves.Add(toCave);
                toCave.ConnectedCaves.Add(fromCave);
            }
        }
    }
}

