using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day5
{
    internal class PuzzleDay5 : Puzzle
    {
        private List<(Location from, Location to)> locations;

        public PuzzleDay5(string input) : base(input) { }

        public override void SolvePart1()
        {
            ParseListOfLinesToLocationsToAndFrom();
            IEnumerable<Location> allLocations = locations.SelectMany(x => x.from.GetLocationsBetweenTwoLocations(x.to));
            int totalLocationsMoreThanTwo = allLocations.GroupBy(x => x.X + x.Y * 1000).Count(group => group.Count() >= 2);
            Console.WriteLine($"Total locations with more than two spots: {totalLocationsMoreThanTwo}");
        }

        public override void SolvePart2()
        {
            ParseListOfLinesToLocationsToAndFrom(false);
            IEnumerable<Location> allLocations = locations.SelectMany(x => x.from.GetLocationsBetweenTwoLocations(x.to));
            int totalLocationsMoreThanTwo = allLocations.GroupBy(x => x.X + x.Y * 1000).Count(group => group.Count() >= 2);
            Console.WriteLine($"Total locations with more than two spots: {totalLocationsMoreThanTwo}");
        }

        private void ParseListOfLinesToLocationsToAndFrom(bool onlyHorizontalOrVertical = true)
        {
            locations = new List<(Location from, Location to)>();
            foreach (string line in Lines)
            {
                string[] locationStrings = line.Split("->");
                (Location from, Location to) newLocations = (new Location(locationStrings[0]), new Location(locationStrings[1]));
                if (onlyHorizontalOrVertical)
                {
                    if (!IsHorizontalLine(newLocations.from, newLocations.to)
                        && !IsVerticalLine(newLocations.from, newLocations.to))
                    {
                        continue;
                    }
                }
                else
                {
                    if (!IsHorizontalLine(newLocations.from, newLocations.to)
                        && !IsVerticalLine(newLocations.from, newLocations.to)
                        && !IsDiagonalLine(newLocations.from, newLocations.to))
                    {
                        continue;
                    }
                }
                locations.Add(newLocations);
            }
        }

        private bool IsHorizontalLine(Location first, Location second) => first.X == second.X;
        private bool IsVerticalLine(Location first, Location second) => first.Y == second.Y;
        private bool IsDiagonalLine(Location first, Location second)
        {
            bool returnable = false;
            int deltaX = Math.Abs(first.X - second.X);
            int deltaY = Math.Abs(first.Y - second.Y);
            if(deltaX > 0 && deltaY > 0)
            {
                returnable = deltaY / deltaX == 1;
            }
            return returnable;
        }
    }
}
