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
        var input = File.ReadAllLines("input.txt");
        var count = 0;
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[i].Length - 3; j++)
            {
                if (input[i][j] == 'X' && input[i][j + 1] == 'M' && input[i][j + 2] == 'A' && input[i][j + 3] == 'S') count++;
                if (input[i][j] == 'S' && input[i][j + 1] == 'A' && input[i][j + 2] == 'M' && input[i][j + 3] == 'X') count++;
            }
        }
        for (var i = 0; i < input.Length - 3; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == 'X' && input[i + 1][j] == 'M' && input[i + 2][j] == 'A' && input[i + 3][j] == 'S') count++;
                if (input[i][j] == 'S' && input[i + 1][j] == 'A' && input[i + 2][j] == 'M' && input[i + 3][j] == 'X') count++;
            }
        }
        for (var i = 0; i < input.Length - 3; i++)
        {
            for (var j = 0; j < input[i].Length - 3; j++)
            {
                if (input[i][j] == 'X' && input[i + 1][j + 1] == 'M' && input[i + 2][j + 2] == 'A' && input[i + 3][j + 3] == 'S') count++;
                if (input[i][j] == 'S' && input[i + 1][j + 1] == 'A' && input[i + 2][j + 2] == 'M' && input[i + 3][j + 3] == 'X') count++;
            }
        }
        for (var i = 0; i < input.Length - 3; i++)
        {
            for (var j = 3; j < input[i].Length; j++)
            {
                if (input[i][j] == 'X' && input[i + 1][j - 1] == 'M' && input[i + 2][j - 2] == 'A' && input[i + 3][j - 3] == 'S') count++;
                if (input[i][j] == 'S' && input[i + 1][j - 1] == 'A' && input[i + 2][j - 2] == 'M' && input[i + 3][j - 3] == 'X') count++;
            }
        }
        System.Console.WriteLine(count);
    }
}
