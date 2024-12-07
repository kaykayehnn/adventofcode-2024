using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

class Part2
{
    const int WALL = 0b01;
    const int EMPTY_SPACE = 0b10;

    private static int[][] directions = {
            [-1, 0],
            [0, 1],
            [1,0],
            [0, -1]
        };
    private static bool CheckEndless(int[][] map, int guardRow, int guardCol)
    {
        int directionIndex = 0;
        // This matrix keeps track of the cells we have visited and the direction we were moving in
        // when stepping on any cell. If we reach a moment when we find a cell we've previously
        // visited and we're moving in the same direction, then we have found an infinite loop.
        var isInLoop = new bool[map.Length, map[0].Length, directions.Length];
        while (true)
        {
            // If we have exited the bounds of the array, there is no endless loop in the current configuration.
            if (!(guardRow >= 0 && guardCol >= 0 && guardRow < map.Length && guardCol < map[0].Length)) return false;

            var cell = map[guardRow][guardCol];
            var directionIndexMapped = directionIndex % directions.Length;

            if (isInLoop[guardRow, guardCol, directionIndexMapped]) return true;
            isInLoop[guardRow, guardCol, directionIndexMapped] = true;

            if (cell == WALL)
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
    }

    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
        Stopwatch sw = Stopwatch.StartNew();

        var map = input.Select(s => s.Select(cell =>
        {
            // We can simplify the code by saving both cell types as the same constant.
            // We only need the ^ cell to determine the starting coordinates.
            if (cell == '.' || cell == '^') return EMPTY_SPACE;
            if (cell == '#') return WALL;

            throw new Exception();
        }).ToArray()).ToArray();
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

        int endless = 0;
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == EMPTY_SPACE)
                {
                    map[i][j] = WALL;
                    if (CheckEndless(map, guardRow, guardCol)) endless++;
                    map[i][j] = EMPTY_SPACE;
                }
            }
        }

        System.Console.WriteLine(endless);
        System.Console.WriteLine($"Elapsed ms: {sw.ElapsedMilliseconds}");
    }
}
