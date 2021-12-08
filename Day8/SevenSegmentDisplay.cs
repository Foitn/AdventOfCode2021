using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day8
{
    internal class SevenSegmentDisplay
    {
        private string signals1;
        private string signals4;
        private string signals7;

        public SevenSegmentDisplay(string signals1, string signals4, string signals7)
        {
            this.signals1 = signals1;
            this.signals4 = signals4;
            this.signals7 = signals7;
        }

        public int GetValueBySignals(string signals)
        {
            int returnable = 0;
            switch (signals.Length)
            {
                case 2:
                    returnable = 1;
                    break;
                case 3:
                    returnable = 7;
                    break;
                case 4:
                    returnable = 4;
                    break;
                case 5:
                    if (AmountWithinArray(signals, signals4) == 3
                        && AmountWithinArray(signals, signals1) == 2
                        && AmountWithinArray(signals, signals7) == 3)
                    {
                        returnable = 3;
                    }
                    else if (AmountWithinArray(signals, signals4) == 3
                        && AmountWithinArray(signals, signals1) == 1
                        && AmountWithinArray(signals, signals7) == 2)
                    {
                        returnable = 5;
                    }
                    else if(AmountWithinArray(signals, signals4) == 2
                        && AmountWithinArray(signals, signals1) == 1
                        && AmountWithinArray(signals, signals7) == 2)
                    {
                        returnable = 2;
                    }
                    break;
                case 6:
                    if (AmountWithinArray(signals, signals4) == 4
                        && AmountWithinArray(signals, signals1) == 2
                        && AmountWithinArray(signals, signals7) == 3)
                    {
                        returnable = 9;
                    }
                    else if(AmountWithinArray(signals, signals4) == 3
                        && AmountWithinArray(signals, signals1) == 1
                        && AmountWithinArray(signals, signals7) == 2)
                    {
                        returnable = 6;
                    }
                    break;
                case 7:
                    returnable = 8;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }
            return returnable;
        }

        private int AmountWithinArray(string input, string other)
        {
            int returnable = 0;
            foreach (char c in input)
            {
                if (other.Contains(c))
                {
                    returnable++;
                }
            }
            return returnable;
        }
    }
}
