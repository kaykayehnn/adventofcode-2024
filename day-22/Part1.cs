using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part1
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
        var input = File.ReadAllLines("/Users/krasimirnedelchev/workarea/adventofcode-2024/day-22/input.txt");
        var numbers = input.Select(long.Parse).ToArray();
        var sum = 0L;
        for (int i = 0; i < numbers.Length; i++)
        {
            var secretNumber = numbers[i];
            for (int j = 0; j < 2000; j++)
            {
                secretNumber = Prune(Mix(secretNumber, secretNumber * 64));
                secretNumber = Prune(Mix(secretNumber, secretNumber / 32));
                secretNumber = Prune(Mix(secretNumber, secretNumber * 2048));
            }
            sum += secretNumber;
        }
        System.Console.WriteLine(sum);
    }
}
