using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day16
{
    internal class PuzzleDay16 : Puzzle
    {
        private string[] example = new string[]
        {
            "D2FE28",
            "38006F45291200",
            "EE00D40C823060",
            "8A004A801A8002F478",
            "620080001611562C8802118E34",
            "C0015000016115A2E0802F182340",
            "A0016C880162017C3686B18A3D4780",
        };

        private string[] example2 = new string[]
        {
            "C200B40A82",
            "04005AC33890",
            "880086C3E88112",
            "CE00C43D881120",
            "D8005AC2A8F0",
            "F600BC2D8F",
            "9C005AC2F8F0",
            "9C0141080250320F1802104A08",
        };

        public PuzzleDay16(string input) : base(input)
        {
        }

        private List<Packet> packets = new ();

        public override void SolvePart1()
        {
            //Lines = example;
            string input = this.Lines[0];
            List<int> inputList = new();
            input.ToList().ForEach(x=> inputList.AddRange(Packet.ParseToHex(x)));
            int[] inputArray = inputList.ToArray();
            while (inputArray.Any(x => x != 0))
            {
                Packet p = new(ref inputArray);
                this.packets.Add(p);
            }
            List<Packet> initial = this.packets.ToList();
            initial.AddRange(this.packets.SelectMany(x=>x.AllSubPackets));
            int sumOfVersions = initial.Sum(x => x.PacketVersion);
            Console.WriteLine($"Sum of versions: {sumOfVersions}");
        }

        public override void SolvePart2()
        {
            
                string input = this.Lines[0];
                this.packets.Clear();
                List<int> inputList = new();
                input.ToList().ForEach(x => inputList.AddRange(Packet.ParseToHex(x)));
                int[] inputArray = inputList.ToArray();
                while (inputArray.Any(x => x != 0))
                {
                    Packet p = new(ref inputArray);
                    this.packets.Add(p);
                }

                List<Packet> initial = this.packets.ToList();
                Console.WriteLine($"{input}\tCalculated value: {initial[0].CalculatedValue}");
            
        }
    }
}

