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
        var count = 0;
        for (var i = 0; i < input.Length - 2; i++)
        {
            for (var j = 0; j < input[i].Length - 2; j++)
            {
                if (input[i][j] == 'M' && input[i][j + 2] == 'S' && input[i+1][j + 1] == 'A' && input[i+2][j] == 'M'&& input[i+2][j+2] == 'S') count++;
                if (input[i][j] == 'S' && input[i][j + 2] == 'M' && input[i+1][j + 1] == 'A' && input[i+2][j] == 'S'&& input[i+2][j+2] == 'M') count++;
                if (input[i][j] == 'M' && input[i][j + 2] == 'M' && input[i+1][j + 1] == 'A' && input[i+2][j] == 'S'&& input[i+2][j+2] == 'S') count++;
                if (input[i][j] == 'S' && input[i][j + 2] == 'S' && input[i+1][j + 1] == 'A' && input[i+2][j] == 'M'&& input[i+2][j+2] == 'M') count++;
            }
        }
        System.Console.WriteLine(count);
    }
}
