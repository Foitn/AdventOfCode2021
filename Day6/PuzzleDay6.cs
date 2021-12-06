using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day6
{
    internal class PuzzleDay6 : Puzzle
    {
        private BigInteger numberOfFish = 0;
        private BigInteger[] numberOfFish2;
        public PuzzleDay6(string input) : base(input) { }

        public override void SolvePart1()
        {
            List<Fish> fishes = Lines[0].Split(',').Select(x => new Fish(byte.Parse(x))).ToList();
            for (int i = 0; i < 80; i++)
            {
                fishes.ToList().ForEach(x => x.Tick(fishes));
            }
            Console.WriteLine($"Total number of fishes: {fishes.Count()}");
        }

        // Please do not ever use this code, for it is terribly slow!
        // It took about 10 hours and 15 minutes to complete (but it does work :))
        public async override Task SolvePart2Async()
        {
            List<byte> fishes = Lines[0].Split(',').Select(x => byte.Parse(x)).ToList();
            numberOfFish2 = new BigInteger[fishes.Count()];
            List<Task> tasks = new List<Task>();
            for (int fishId = 0; fishId < fishes.Count; fishId++)
            {
                Console.WriteLine($"Processing fish {fishId}, current number of fish: {numberOfFish}");
                int theFish = fishId;
                tasks.Add(Task.Run(() => DetermineNumberOfChildren(fishes[theFish], 256, theFish)));
            }
            await Task.WhenAll(tasks);
            foreach(BigInteger big in numberOfFish2)
            {
                numberOfFish += big;
            }
            Console.WriteLine($"Total number of fish: {numberOfFish}");
        }

        private void DetermineNumberOfChildren(int numberOfDaysTillLabour, int numberOfDays, int fishId)
        {
            numberOfFish2[fishId]++;
            if (numberOfDaysTillLabour < numberOfDays)
            {
                for (int i = 0; i < numberOfDays; i++)
                {
                    if (numberOfDaysTillLabour == 0)
                    {
                        numberOfDaysTillLabour = 6;
                        DetermineNumberOfChildren(8, numberOfDays - i - 1, fishId);
                    }
                    else
                    {
                        numberOfDaysTillLabour--;
                    }
                }
            }
        }
    }
}
