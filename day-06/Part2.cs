using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

class Part2
{
    private static int[][] directions = {
            [-1, 0],
            [0, 1],
            [1,0],
            [0, -1]
        };
    private static bool CheckEndless(char[][] map, int guardRow, int guardCol)
    {
        int directionIndex = 0;
        var isInLoop = new bool[map.Length, map[0].Length, directions.Length];
        while (true)
        {
            try
            {
                var cell = map[guardRow][guardCol];
                var directionIndexMapped = directionIndex % directions.Length;
                if (cell == '.' || cell == '^')
                {
                    if (isInLoop[guardRow, guardCol, directionIndexMapped]) return true;
                    isInLoop[guardRow, guardCol, directionIndexMapped] = true;
                }

                if (cell == '#')
                {
                    guardRow -= directions[directionIndexMapped][0];
                    guardCol -= directions[directionIndexMapped][1];
                    directionIndex++;
                }
                else
                {
                    guardRow += directions[directionIndexMapped][0];
                    guardCol += directions[directionIndexMapped][1];
                }
            }
            catch
            {
                return false;
            }
        }
    }

    public static void Solve(string[] args)
    {
        Stopwatch sw = Stopwatch.StartNew();
        var input = File.ReadAllLines("/Users/krasimirnedelchev/workarea/adventofcode-2024/day-06/input.txt");

        var map = input.Select(s => s.ToCharArray()).ToArray();
        var guardRow = 0;
        var guardCol = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var index = input[i].IndexOf('^');
            if (index >= 0)
            {
                guardRow = i;
                guardCol = index;
                break;
            }
        }

        var mapCopy = new char[map.Length][];
        for (var i = 0; i < map.Length; i++)
        {
            mapCopy[i] = new char[map[i].Length];
        }

        int endless = 0;
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '.')
                {
                    for (var k = 0; k < map.Length; k++)
                    {
                        for (var l = 0; l < map[k].Length; l++)
                        {
                            mapCopy[k][l] = map[k][l];
                        }
                    }

                    mapCopy[i][j] = '#';
                    if (CheckEndless(mapCopy, guardRow, guardCol)) endless++;
                }
            }
        }
        System.Console.WriteLine(endless);
        System.Console.WriteLine(sw.ElapsedMilliseconds);
    }
}
