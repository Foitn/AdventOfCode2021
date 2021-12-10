using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day10
{
    internal class PuzzleDay10 : Puzzle
    {
        private string[] example = new string[] {
            "[({(<(())[]>[[{[]{<()<>>",
            "[(()[<>])]({[<{<<[]>>(",
            "{([(<{}[<>[]}>{[]{[(<()>",
            "(((({<>}<{<{<>}{[]{[]{}",
            "[[<[([]))<([[{}[[()]]]",
            "[{[{({}]{}}([{[{{{}}([]",
            "{<[[]]>}<{[{[{[]{()[[[]",
            "[<(<(<(<{}))><([]([]()",
            "<{([([[(<>()){}]>(<<{{",
            "<{([{{}}[<[[[<>{}]]]>[]]",
            };

        public PuzzleDay10(string input) : base(input) { }

        public override void SolvePart1()
        {
            //Lines = example;
            bool corrupted = false;
            int total = 0;
            foreach (string line in Lines)
            {
                corrupted = false;
                List<char> chars = new List<char>();
                char currentChar = '\0';
                foreach (char ch in line)
                {
                    currentChar = ch;
                    if (IsOpening(ch))
                    {
                        chars.Add(ch);
                    }
                    else
                    {
                        if (GetClosing(chars.Last()) == ch)
                        {
                            chars.RemoveAt(chars.Count - 1);
                        }
                        else
                        {
                            Console.WriteLine($"Corrupted character is {currentChar}");
                            corrupted = true;
                            break;
                        }
                    }
                }
                if (corrupted)
                {
                    total += ClosingToValue(currentChar);
                }
            }
            Console.WriteLine($"Total {total}");
        }

        public override void SolvePart2()
        {
            //Lines = example;
            bool corrupted = false;
            List<long> scores = new List<long>();
            foreach (string line in Lines)
            {
                corrupted = false;
                List<char> chars = new List<char>();
                char currentChar = '\0';
                foreach (char ch in line)
                {
                    currentChar = ch;
                    if (IsOpening(ch))
                    {
                        chars.Add(ch);
                    }
                    else
                    {
                        if (GetClosing(chars.Last()) == ch)
                        {
                            chars.RemoveAt(chars.Count - 1);
                        }
                        else
                        {
                            //Console.WriteLine($"Corrupted character is {currentChar}");
                            corrupted = true;
                            break;
                        }
                    }
                }
                if (corrupted)
                {
                    //total += ClosingToValue(currentChar);
                }
                else if (chars.Count > 0)
                {
                    chars.Reverse();
                    List<char> closingList = chars.Select(x => GetClosing(x)).ToList();
                    long totalValue = 0;
                    foreach (char ch in closingList)
                    {
                        totalValue *= 5;
                        totalValue += ClosingToValue2(ch);
                    }
                    scores.Add(totalValue);
                    Console.WriteLine($"Incomplete line can be fixed with {new string(closingList.ToArray())}, total value is {totalValue}");
                }
            }
            scores = scores.OrderBy(x => x).ToList();
            Console.WriteLine($"Total {scores[(scores.Count - 1) / 2]}");

        }

        private bool IsOpening(char c)
            => c == '(' || c == '{' || c == '<' || c == '[';

        private char GetClosing(char c)
            => c switch
            {
                '(' => ')',
                '{' => '}',
                '<' => '>',
                '[' => ']',
                _ => throw new ArgumentException()
            };

        private int ClosingToValue(char c)
            => c switch
            {
                ')' => 3,
                ']' => 57,
                '}' => 1197,
                '>' => 25137,
                _ => throw new ArgumentException()
            };
        private int ClosingToValue2(char c)
            => c switch
            {
                ')' => 1,
                ']' => 2,
                '}' => 3,
                '>' => 4,
                _ => throw new ArgumentException()
            };
    }
}

