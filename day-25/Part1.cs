using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part1
{
    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");

        var keys = new List<int[]>();
        var locks = new List<int[]>();
        const int LOCK_HEIGHT = 7;
        int LOCK_WIDTH = input[0].Length;
        for (int i = 0; i < input.Length; i += LOCK_HEIGHT + 1)
        {
            var firstLine = input[i];

            var dimensions = new int[LOCK_WIDTH];
            for (int j = 0; j < LOCK_WIDTH; j++)
            {
                // The first/last line is not considered part of the key/lock
                dimensions[j]--;
                for (int k = i; k < i + LOCK_HEIGHT; k++)
                {
                    if (input[k][j] == '#') dimensions[j]++;
                }
            }

            if (firstLine.Replace(".", "") == string.Empty)
                // key
                keys.Add(dimensions);
            else
                locks.Add(dimensions);
        }

        var matching = 0;
        for (int i = 0; i < keys.Count; i++)
        {
            for (int j = 0; j < locks.Count; j++)
            {
                bool fits = true;
                for(int k = 0 ; k < LOCK_WIDTH && fits; k++){
                    fits = keys[i][k] + locks[j][k] <= LOCK_HEIGHT - 2; 
                }
                if(fits) matching++;
            }
        }
        System.Console.WriteLine(matching);
    }
}
