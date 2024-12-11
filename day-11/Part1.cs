using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

class Part1
{
    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
        var stones = input[0].Split().Select(long.Parse).ToList();

        for (int i = 0; i < 75; i++)
        {
            for (int j = 0; j < stones.Count; j++)
            {
                var stone = stones[j];
                var numDigits = ((int)Math.Ceiling(Math.Log10(stone+1)));
                if (stone == 0)
                    stones[j] = 1;
                else if (numDigits % 2 == 0)
                {
                    var power = Math.Pow(10,numDigits / 2);
                    var leftHalf = (long)Math.Floor(stone / power);
                    var rightHalf = stone % (long)power;
                    stones[j] = leftHalf;
                    stones.Insert(j+1, rightHalf);
                    j++;
                } else {
                    stones[j] = stone * 2024;
                }
            }
            System.Console.WriteLine(i);
            // System.Console.WriteLine(String.Join(" ", stones));
        }
        System.Console.WriteLine(stones.Count);
    }
}
