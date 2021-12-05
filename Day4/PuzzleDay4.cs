using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day4
{
    internal class PuzzleDay4 : Puzzle
    {
        private string inputForBingo;
        private List<BingoCard> cards;

        public PuzzleDay4(string input) : base(input) { }

        public override void SolvePart1()
        {
            
            int finalValue = -1;
            int bingoCardSizeX = 5;
            int bingoCardSizeY = 5;
            inputForBingo = Lines[0];
            cards = new List<BingoCard>();
            for(int i = 2; i < Lines.Count(); i+=bingoCardSizeY+1)
            {
                cards.Add(new BingoCard(Lines.Skip(i).Take(bingoCardSizeY).ToArray(), bingoCardSizeX, bingoCardSizeY));
            }
            string[] inputForBingoValues = inputForBingo.Split(',');
            foreach(string input in inputForBingoValues)
            {
                if(int.TryParse(input, out int bingoValue))
                {
                    Console.WriteLine($"New value was {bingoValue}");
                    cards.ForEach(x=> x.AddValue(bingoValue));
                    cards.ForEach(x => Console.WriteLine(x));
                    if(cards.FirstOrDefault(x=> x.HasBingo()) is BingoCard cardThatWon)
                    {
                        finalValue = cardThatWon.GetFinalValue(bingoValue);
                        break;
                    }
                }
            }
            Console.WriteLine($"Final value was {finalValue}");
        }

        public override void SolvePart2()
        {
            int finalValue = -1;
            int bingoCardSizeX = 5;
            int bingoCardSizeY = 5;
            inputForBingo = Lines[0];
            cards = new List<BingoCard>();
            for (int i = 2; i < Lines.Count(); i += bingoCardSizeY + 1)
            {
                cards.Add(new BingoCard(Lines.Skip(i).Take(bingoCardSizeY).ToArray(), bingoCardSizeX, bingoCardSizeY));
            }
            string[] inputForBingoValues = inputForBingo.Split(',');
            foreach (string input in inputForBingoValues)
            {
                if (int.TryParse(input, out int bingoValue))
                {
                    Console.WriteLine($"New value was {bingoValue}");
                    cards.ForEach(x => x.AddValue(bingoValue));
                    cards.ForEach(x => Console.WriteLine(x));
                    if (cards.Where(x => x.HasBingo()).ToList() is IEnumerable<BingoCard> cardsThatWon)
                    {
                        if (cards.Count == 1 && cardsThatWon.Count() == 1)
                        {
                            BingoCard cardThatWon = cards.First();
                            finalValue = cardThatWon.GetFinalValue(bingoValue);
                            break;
                        }
                        foreach (var card in cardsThatWon)
                        {
                            cards.Remove(card);
                        }
                    }
                }
            }
            Console.WriteLine($"Final value was {finalValue}");
        }
    }
}
