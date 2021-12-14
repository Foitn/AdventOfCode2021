using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day14
{
    internal class PuzzleDay14 : Puzzle
    {
        private string[] example = new string[] {
                "NNCB",
                "",
                "CH -> B",
                "HH -> N",
                "CB -> H",
                "NH -> C",
                "HB -> C",
                "HC -> B",
                "HN -> C",
                "NN -> C",
                "BH -> H",
                "NC -> B",
                "NB -> B",
                "BN -> B",
                "BB -> N",
                "BC -> B",
                "CC -> N",
                "CN -> C",
            };

        public PuzzleDay14(string input) : base(input) { }

        public override void SolvePart1()
        {
            //Lines = example;
            string input = this.Lines[0];
            List<(string input, string output)> polymerTemplates = this.Lines.Skip(2).Select(x => (x.Split(" -> ")[0], x.Split(" -> ")[1])).ToList();
            //Console.WriteLine(input);
            for (int i = 0; i < 10; i++)
            {
                input = this.PerformStep(input, polymerTemplates);
            }
            //Console.WriteLine(input);
            List<char> result = input.GroupBy(i => i).OrderBy(g => g.Count()).Select(g => g.Key).ToList();
            char mostCommon = result.Last();
            char leastCommon = result.First();

            int leastCommonCount = input.Count(x => x == leastCommon);
            int mostCommonCount = input.Count(x => x == mostCommon);
            Console.WriteLine($"{mostCommonCount - leastCommonCount}");
        }

        public override void SolvePart2()
        {
            //Lines = example;
            string input = this.Lines[0];
            List<(string input, string output)> polymerTemplates = this.Lines.Skip(2).Select(x => (x.Split(" -> ")[0], x.Split(" -> ")[1])).ToList();
            Dictionary<string, ulong> result = new();
            foreach ((string input, string output) x in polymerTemplates)
            {
                result.Add(x.input, 0);
            }
            for (int i = 0; i < input.Length-1; i++)
            {
                result[input[i].ToString() + input[i+1]]++;
            }
            for (int i = 0; i < 40; i++)
            {
                Console.WriteLine($"Working on step {i}");

                this.PerformDicStep(result, polymerTemplates);
            }

            Dictionary<char, ulong> resultChars = new Dictionary<char, ulong>();
            foreach (KeyValuePair<string, ulong> kvp in result)
            {
                Console.WriteLine($"{kvp.Key}:\t{kvp.Value}");
                char char1 = kvp.Key[0];
                char char2 = kvp.Key[1];
                resultChars.TryAdd(char1, 0);
                resultChars[char1] += kvp.Value;
                //resultChars[char2] -= kvp.Value;
            }

            foreach(KeyValuePair<char, ulong> x in resultChars)
            {
                Console.WriteLine($"{x.Key}:\t{x.Value}");
            }


            ulong leastCommonCount = resultChars.Min(x => x.Value);
            ulong mostCommonCount = resultChars.Max(x => x.Value);

            //Console.WriteLine(input);
            //List<char> resultChars = result.GroupBy(i => i).OrderBy(g => g.Count()).Select(g => g.Key).ToList();
            //char mostCommon = result.Last();
            //char leastCommon = result.First();
            //
            //int leastCommonCount = input.Count(x => x == leastCommon);
            //int mostCommonCount = input.Count(x => x == mostCommon);
            Console.WriteLine($"{mostCommonCount - leastCommonCount+1}");

        }

        private void PerformDicStep(Dictionary<string, ulong> dict, IReadOnlyCollection<(string input, string output)> polymerTemplates)
        {
            Dictionary<string, ulong> newKeys = new();
            foreach (string keys in dict.Keys)
            {
                newKeys.Add(keys, 0);
            }
            foreach (KeyValuePair<string, ulong> input in dict)
            {
                string newChar = polymerTemplates.First(x => x.input == input.Key).output;
                string part1 = input.Key[0] + newChar;
                string part2 = newChar + input.Key[1];
                newKeys[part1] += input.Value;
                newKeys[part2] += input.Value;
                newKeys[input.Key] -= input.Value;
            }

            foreach (KeyValuePair<string, ulong> key in newKeys)
            {
                dict[key.Key] += key.Value;
            }
        }

        private string PerformStep(string input, IReadOnlyCollection<(string input, string output)> polymerTemplates)
        {
            string returnable = input[0].ToString();
            for (int i = 0; i < input.Length - 1; i++)
            {
                string subPolymer = input.Substring(i, 2);
                string output = polymerTemplates.First(x => x.input == subPolymer).output;
                //returnable += subPolymer[0];
                returnable += output;
                returnable += subPolymer[1];
            }
            return returnable;
        }
    }
}

