using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day8
{
    internal class PuzzleDay8 : Puzzle
    {
        public PuzzleDay8(string input) : base(input) { }

        public override void SolvePart1()
        {
            //Lines = new string[] {
            //    "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
            //    "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc",
            //    "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
            //    "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
            //    "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
            //    "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
            //    "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
            //    "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
            //    "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
            //    "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
            //};
            Lines = Lines.Select(x => x.Substring(x.IndexOf('|') + 1)).ToArray();
            int numberOf1478 = Lines.SelectMany(x => x.Split(' ')).Count(x =>
                x.Trim().Length == 2 ||
                x.Trim().Length == 3 ||
                x.Trim().Length == 4 ||
                x.Trim().Length == 7
                );
            Console.WriteLine($"Number of 1, 4, 7, 8: {numberOf1478}");
        }

        public override void SolvePart2()
        {
            //Lines = new string[] {
            //    "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
            //    "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc",
            //    "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
            //    "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
            //    "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
            //    "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
            //    "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
            //    "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
            //    "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
            //    "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
            //};
            int totalOutput = 0;
            foreach (string line in Lines)
            {
                string inputString = line.Split('|')[0];
                string outputString = line.Split('|')[1];
                string[] inputStrings = inputString.Split(' ');
                string[] outputStrings = outputString.Split(' ');
                List<string> allStrings = inputStrings.ToList();
                allStrings.AddRange(outputStrings);

                string signals1 = allStrings.First(x => x.Trim().Length == 2);
                string signals4 = allStrings.First(x => x.Trim().Length == 4);
                string signals7 = allStrings.First(x => x.Trim().Length == 3);

                SevenSegmentDisplay display = new SevenSegmentDisplay(signals1, signals4, signals7);
                string value = "";
                foreach (string output in outputStrings.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    int valueOfSingleDigit = display.GetValueBySignals(output);
                    value += valueOfSingleDigit;
                }
                totalOutput += int.Parse(value);
            }
            Console.WriteLine($"Total value is {totalOutput}");
        }
    }
}

