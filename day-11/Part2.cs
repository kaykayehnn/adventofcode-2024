using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

class Part2
{
    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
        var stones = input[0].Split().Select(long.Parse).ToList();

        var numOccurrences = new Dictionary<long, long>();
        for (var i = 0; i < stones.Count; i++)
        {
            if (!numOccurrences.ContainsKey(stones[i]))
            {
                numOccurrences[stones[i]] = 0;
            }
            numOccurrences[stones[i]]++;
        }
        for (int i = 0; i < 75; i++)
        {
            var nextIterStones = new Dictionary<long, long>();
            foreach (var kvp in numOccurrences)
            {
                var stone = kvp.Key;
                var numDigits = ((int)Math.Ceiling(Math.Log10(stone + 1)));
                var newNums = new List<long>();
                if (stone == 0)
                    newNums.Add(1);
                else if (numDigits % 2 == 0)
                {
                    var power = Math.Pow(10, numDigits / 2);
                    var leftHalf = (long)Math.Floor(stone / power);
                    var rightHalf = stone % (long)power;
                    newNums.Add(leftHalf);
                    newNums.Add(rightHalf);
                }
                else
                {
                    newNums.Add(stone * 2024);
                }
                foreach (var item in newNums)
                {
                    if (!nextIterStones.ContainsKey(item))
                    {
                        nextIterStones.Add(item, 0);
                    }
                    nextIterStones[item]+=kvp.Value;
                }
            }
            numOccurrences = nextIterStones;
        }
        System.Console.WriteLine(numOccurrences.Sum(kvp => kvp.Value));
    }
}
