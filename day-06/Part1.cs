using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

class Part1
{
    public static void Solve(string[] args)
    {
        var directions = new[]{
            new[]{-1, 0},
            [0, 1],
            [1,0],
            [0, -1]
        };

        var input = File.ReadAllLines("input.txt");

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

        bool isInside = true;
        int directionIndex = 0;
        while (isInside)
        {
            try
            {
                var cell = map[guardRow][guardCol];
                if (cell == '.' || cell == '^')
                {
                    map[guardRow][guardCol] = 'X';
                }

                if (cell == '#')
                {
                    guardRow -= directions[directionIndex % directions.Length][0];
                    guardCol -= directions[directionIndex % directions.Length][1];
                    directionIndex++;
                }
                else
                {
                    guardRow += directions[directionIndex % directions.Length][0];
                    guardCol += directions[directionIndex % directions.Length][1];
                }
            }
            catch
            {
                isInside = false;
            }
        }

        var visited = map.Sum(arr => arr.Count(c => c == 'X'));
        System.Console.WriteLine(visited);
    }
}
