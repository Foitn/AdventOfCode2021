using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day16
{
    internal enum EPacketType
    {
        Sum = 0,
        Product = 1,
        Minimum = 2,
        Maximum = 3,
        Literal = 4, 
        GreaterThan = 5,
        LessThan = 6,
        EqualTo = 7,
    }
}
