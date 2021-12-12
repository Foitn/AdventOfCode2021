using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day12
{
    internal class Cave
    {
        public bool IsLargeCave => this.Title.All(char.IsUpper);
        public string Title { get; set; }
        public List<Cave> ConnectedCaves { get; set; }
        public bool IsStartNode => this.Title == "start";
        public bool IsEndNode => this.Title == "end";
        public int NumberOfVisits { get; set; } = 0;
        public bool HasBeenVisitedMaxNumberOfTimes => !this.IsLargeCave && this.NumberOfVisits > 0;

        public Cave(string title) : this(title, new List<Cave>()) { }

        public Cave(string title, List<Cave> connectedCaves)
        {
            this.Title = title;
            this.ConnectedCaves = connectedCaves;
        }

        public override bool Equals(object? obj)
        {
            return obj is Cave other && this.Equals(other);
        }

        protected bool Equals(Cave other)
        {
            return this.Title == other.Title;
        }

        public override int GetHashCode()
        {
            return this.Title.GetHashCode();
        }
    }
}
