using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

class Part2
{
    public static long Mix(long a, long b)
    {
        return a ^ b;
    }

    public static long Prune(long a)
    {
        return a % 16777216;
    }
    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
        var s = Stopwatch.StartNew();
        var numbers = input.Select(long.Parse).ToArray();
        var sequences = new int[numbers.Length][];
        for (int i = 0; i < numbers.Length; i++)
        {
            // We need to track the first 2000 changes, which means 2001 elements.
            sequences[i] = new int[2001];
            var secretNumber = numbers[i];
            sequences[i][0] = (int)(secretNumber % 10);
            for (int j = 1; j < sequences[i].Length; j++)
            {
                secretNumber = Prune(Mix(secretNumber, secretNumber * 64));
                secretNumber = Prune(Mix(secretNumber, secretNumber / 32));
                secretNumber = Prune(Mix(secretNumber, secretNumber * 2048));
                sequences[i][j] = (int)(secretNumber % 10);
            }
        }

        var changeSequences = new int[20, 20, 20, 20];
        var best = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            var foundSequences = new bool[20, 20, 20, 20];
            var arr = sequences[i];
            var secretNumber = numbers[i];
            for (int j = 1; j < arr.Length - 3; j++)
            {
                // Offset each of the diffs by 10 to account for negative numbers.
                var c1 = arr[j] - arr[j - 1] + 10;
                var c2 = arr[j + 1] - arr[j] + 10;
                var c3 = arr[j + 2] - arr[j + 1] + 10;
                var c4 = arr[j + 3] - arr[j + 2] + 10;
                if (foundSequences[c1, c2, c3, c4]) continue;

                foundSequences[c1, c2, c3, c4] = true;
                changeSequences[c1, c2, c3, c4] += arr[j + 3];
                if (changeSequences[c1, c2, c3, c4] > best) best = changeSequences[c1, c2, c3, c4];
            }
        }
        System.Console.WriteLine(best);
        System.Console.WriteLine(s.ElapsedMilliseconds);
    }
}
