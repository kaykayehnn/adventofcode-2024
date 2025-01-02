using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

class Part2BruteForce
{
    public static long Mix(long a, long b)
    {
        return a ^ b;
    }

    public static long Prune(long a)
    {
        return a % 16777216;
    }

    public static long SolveTask(int c1)
    {
        var input = File.ReadAllLines("input.txt");
        var numbers = input.Select(long.Parse).ToArray();
        var totalBest = 0L;
        var sequences = new long[numbers.Length][];
        for (int i = 0; i < numbers.Length; i++)
        {
            sequences[i] = new long[2001];
            var secretNumber = numbers[i];
            sequences[i][0] = secretNumber % 10;
            for (int j = 1; j < sequences[i].Length; j++)
            {
                secretNumber = Prune(Mix(secretNumber, secretNumber * 64));
                secretNumber = Prune(Mix(secretNumber, secretNumber / 32));
                secretNumber = Prune(Mix(secretNumber, secretNumber * 2048));
                sequences[i][j] = secretNumber % 10;
            }
        }
        for (int c2 = -9; c2 < 10; c2++)
        {
            if (c1 + c2 < -9 || c1 + c2 > 9) continue;
            for (int c3 = -9; c3 < 10; c3++)
            {
                if (c1 + c2 + c3 < -9 || c1 + c2 + c3 > 9) continue;

                for (int c4 = -9; c4 < 10; c4++)
                {
                    if (c1 + c2 + c3 + c4 < -9 || c1 + c2 + c3 + c4 > 9) continue;

                    var subTotal = 0L;
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        var arr = sequences[i];
                        for (int j = 1; j < arr.Length - 3; j++)
                        {
                            if (arr[j] - arr[j - 1] == c1 && arr[j + 1] - arr[j] == c2 && arr[j + 2] - arr[j + 1] == c3 && arr[j + 3] - arr[j + 2] == c4)
                            {
                                subTotal += arr[j+3];
                                break;
                            }
                        }
                    }
                    if (totalBest < subTotal) totalBest = subTotal;
                }
            }
        }

        return totalBest;
    }

    public static void Solve(string[] args)
    {
        var s = Stopwatch.StartNew();
        var tasks = new List<Task<long>>();
        for (int c1 = -9; c1 < 10; c1++)
        {
            int c1Copy = c1;
            tasks.Add(Task.Run(() => SolveTask(c1Copy)));
        }

        Task.WaitAll(tasks.ToArray());
        var totalBest = tasks.Max(t => t.Result);

        System.Console.WriteLine(totalBest);
        System.Console.WriteLine(s.ElapsedMilliseconds);
    }
}
