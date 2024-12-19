using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part2
{
    private static long IsPossible(string[] substrings, string text)
    {
        var isPossibleArr = new long[text.Length + 1];
        // The empty string is always possible
        isPossibleArr[0] = 1;

        for (int i = 1; i <= text.Length; i++)
        {
            for (int j = 0; j < substrings.Length; j++)
            {
                var towel = substrings[j];
                if (towel.Length > i || isPossibleArr[i - towel.Length] == 0) continue;

                var isTheSameSuffix = true;
                for (int k = 0; k < towel.Length && isTheSameSuffix; k++)
                {
                    isTheSameSuffix = text[i - towel.Length + k] == towel[k];
                }
                if (isTheSameSuffix)
                {
                    isPossibleArr[i] += isPossibleArr[i - towel.Length];
                }
            }
        }

        return isPossibleArr[text.Length];
    }

    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
        var substrings = input[0].Split([", "], StringSplitOptions.None);

        var possibleCount = input.Skip(2).Sum(line => IsPossible(substrings, line));

        System.Console.WriteLine(possibleCount);
    }
}
