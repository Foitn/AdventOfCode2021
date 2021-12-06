using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day6
{
    internal class Fish
    {
        public static int NumberOfFish = 0;
        private byte DaysBeforeLabour;

        public Fish(byte daysBeforLabour = 8)
        {
            DaysBeforeLabour = (byte)daysBeforLabour;
        }

        public void Tick(List<Fish> ListOfFishes)
        {
            if (DaysBeforeLabour == 0)
            {
                DaysBeforeLabour = 6;
                ListOfFishes.Add(new Fish());
            }
            else
            {
                DaysBeforeLabour--;
            }
        }
    }
}
