using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day5
{
    internal class Location : IEquatable<Location>
    {
        public Location() { }
        public Location(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public Location(string input) :
            this(
                int.Parse(input.Trim().Split(',')[0].Trim()),
                int.Parse(input.Trim().Split(',')[1].Trim())
                )
        { }

        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;

        public IEnumerable<Location> GetLocationsBetweenTwoLocations(Location otherLocation)
        {
            int largestX = X > otherLocation.X ? X : otherLocation.X;
            int largestY = Y > otherLocation.Y ? Y : otherLocation.Y;
            int smallestX = X < otherLocation.X ? X : otherLocation.X;
            int smallestY = Y < otherLocation.Y ? Y : otherLocation.Y;

            if (largestX == smallestX || largestY == smallestY)
            {
                for (int x = smallestX; x <= largestX; x++)
                {
                    for (int y = smallestY; y <= largestY; y++)
                    {
                        yield return new Location(x, y);
                    }
                }
            }
            else
            {
                Location smallestXLocation = X < otherLocation.X ? this : otherLocation;
                Location largestXLocation = X > otherLocation.X ? this : otherLocation;
                bool isRising = smallestXLocation.Y < largestXLocation.Y;
                for (int x = smallestXLocation.X; x <= largestXLocation.X; x++)
                {
                    int delta = x - smallestXLocation.X;
                    yield return new Location(x, smallestXLocation.Y + (isRising ? delta : -delta));
                }
            }
        }

        public bool Equals(Location? other)
        {
            return this.X == other?.X && this.Y == other?.Y;
        }
    }
}
