using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day16
{
    internal class Packet
    {
        public int PacketVersion { get; set; }
        public int PacketID { get; set; }
        public List<Packet> SubPackets { get; }
        public List<Packet> AllSubPackets
        {
            get
            {
                List<Packet> start = this.SubPackets.ToList();
                start.AddRange(this.SubPackets.SelectMany(x => x.AllSubPackets));
                return start;
            }
        }

        public int ArraySize = -1;

        public EPacketType PacketType => (EPacketType)this.PacketID;
        public ulong LiteralValue { get; set; } = ulong.MaxValue;
        public ulong CalculatedValue => CalculateValue();

        private ulong CalculateValue()
        {
            ulong value = 0;
            switch (PacketType)
            {
                case EPacketType.Sum:
                    value = this.SubPackets.Aggregate<Packet, ulong>(0, (current, packet) => current + packet.CalculatedValue);
                    break;
                case EPacketType.Product:
                    value = this.SubPackets.Aggregate<Packet, ulong>(1, (current, packet) => current * packet.CalculatedValue);
                    break;
                case EPacketType.Minimum:
                    value = this.SubPackets.Min(x => x.CalculatedValue);
                    break;
                case EPacketType.Maximum:
                    value = this.SubPackets.Max(x => x.CalculatedValue);
                    break;
                case EPacketType.Literal:
                    value = this.LiteralValue;
                    break;
                case EPacketType.GreaterThan:
                    value = (ulong)(this.SubPackets[0].CalculatedValue > this.SubPackets[1].CalculatedValue ? 1 : 0);
                    break;
                case EPacketType.LessThan:
                    value = (ulong)(this.SubPackets[0].CalculatedValue < this.SubPackets[1].CalculatedValue ? 1 : 0);
                    break;
                case EPacketType.EqualTo:
                    value = (ulong)(this.SubPackets[0].CalculatedValue == this.SubPackets[1].CalculatedValue ? 1 : 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return value;
        }

        public Packet(ref int[] input)
        {
            int usedFromString = 0;
            this.SubPackets = new();
            int[] values = input;

            if (values.Length < 6) return;

            this.PacketVersion = (int)BinaryIntArrayToSingleInt(values[..3]);
            this.PacketID = (int)BinaryIntArrayToSingleInt(values[3..6]);

            usedFromString = 6;

            if (this.PacketType == EPacketType.Literal)
            {
                bool isLastPacket = false;
                List<int> intValues = new List<int>();
                for (int i = 6; i < values.Length && !isLastPacket; i += 5)
                {
                    isLastPacket = values[i] == 0;
                    intValues.AddRange(values[(i + 1)..(i + 5)]);
                    usedFromString += 5;
                }
                this.LiteralValue = BinaryIntArrayToSingleInt(intValues.ToArray());
            }
            else
            {
                int length = values[6] == 1 ? 11 : 15;

                int l = (int)BinaryIntArrayToSingleInt(values[7..(7 + length)]);
                if (length == 15)
                {
                    usedFromString = 7 + length;
                    int[] subPacketArray = values[(7 + length)..];
                    do
                    {
                        Packet p = new(ref subPacketArray);
                        this.SubPackets.Add(p);
                        usedFromString += p.ArraySize;
                        subPacketArray = values[(usedFromString)..];
                    } while ((usedFromString- (7 + length)) < l);//(subPacketArray.Any(x => x != 0))
                    //usedFromString = 7 + length + l;
                }
                else
                {
                    usedFromString = 7 + length;
                    for (int i = 0; i < l; i++)
                    {
                        int[] subPacketArray = values[(usedFromString)..];
                        Packet p = new(ref subPacketArray);
                        this.SubPackets.Add(p);
                        usedFromString += p.ArraySize;
                    }
                }
            }
            input = input[usedFromString..];
            this.ArraySize = usedFromString;
        }


        public static ulong BinaryIntArrayToSingleInt(int[] input)
            => BinaryStringToInt(string.Join("", input.Select(x => x.ToString())));

        public static ulong BinaryStringToInt(string input)
            => Convert.ToUInt64(input, 2);


        public static string ParseFromHex(int[] input)
        {
            string returnable = "";
            for (int i = 0; i < input.Length; i += 4)
            {
                int a = input[i];
                int b = i + 1 < input.Length ? input[i + 1] : 0;
                int c = i + 2 < input.Length ? input[i + 2] : 0;
                int d = i + 3 < input.Length ? input[i + 3] : 0;
                returnable += ParseFromHex((a, b, c, d));
            }
            return returnable;
        }

        public static char ParseFromHex((int a, int b, int c, int d) input)
            => input switch
            {
                (0, 0, 0, 0) => '0',
                (0, 0, 0, 1) => '1',
                (0, 0, 1, 0) => '2',
                (0, 0, 1, 1) => '3',
                (0, 1, 0, 0) => '4',
                (0, 1, 0, 1) => '5',
                (0, 1, 1, 0) => '6',
                (0, 1, 1, 1) => '7',
                (1, 0, 0, 0) => '8',
                (1, 0, 0, 1) => '9',
                (1, 0, 1, 0) => 'A',
                (1, 0, 1, 1) => 'B',
                (1, 1, 0, 0) => 'C',
                (1, 1, 0, 1) => 'D',
                (1, 1, 1, 0) => 'E',
                (1, 1, 1, 1) => 'F',
            };

        public static int[] ParseToHex(char input)
            => input switch
            {
                '0' => new[] { 0, 0, 0, 0 },
                '1' => new[] { 0, 0, 0, 1 },
                '2' => new[] { 0, 0, 1, 0 },
                '3' => new[] { 0, 0, 1, 1 },
                '4' => new[] { 0, 1, 0, 0 },
                '5' => new[] { 0, 1, 0, 1 },
                '6' => new[] { 0, 1, 1, 0 },
                '7' => new[] { 0, 1, 1, 1 },
                '8' => new[] { 1, 0, 0, 0 },
                '9' => new[] { 1, 0, 0, 1 },
                'A' => new[] { 1, 0, 1, 0 },
                'B' => new[] { 1, 0, 1, 1 },
                'C' => new[] { 1, 1, 0, 0 },
                'D' => new[] { 1, 1, 0, 1 },
                'E' => new[] { 1, 1, 1, 0 },
                'F' => new[] { 1, 1, 1, 1 },
            };

        public override string ToString()
            => $"V:{this.PacketVersion}\tID:{this.PacketID}\tLV:{this.LiteralValue}\tSubs:{this.SubPackets.Count}";

    }
}
