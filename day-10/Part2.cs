using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

class Part2
{
    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
        var map = input.Select(line => line.Select(c => c - '0').ToArray()).ToArray();
        var routes = map.Select(l => new long[l.Length]).ToArray();

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 0) routes[i][j] = 1;
            }
        }

        var adjacents = new[]{
            [-1, 0],
            [1, 0],
            [0,1],
            new[] {0, -1},
        };
        for (int i = 1; i < 10; i++)
        {
            for (int j = 0; j < map.Length; j++)
            {
                for (int k = 0; k < map[j].Length; k++)
                {
                    if (map[j][k] != i) continue;
                    for (int l = 0; l < adjacents.Length; l++)
                    {
                        int newRow = j + adjacents[l][0];
                        int newCol = k + adjacents[l][1];
                        if (newRow >= 0 && newCol >= 0 && newRow < map.Length && newCol < map[newRow].Length && map[newRow][newCol] == i - 1)
                        {
                            routes[j][k] += routes[newRow][newCol];
                        }
                    }
                }
            }
        }

        var sum = 0L;
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 9) sum += routes[i][j];
            }
        }
        System.Console.WriteLine(sum);
    }
}
