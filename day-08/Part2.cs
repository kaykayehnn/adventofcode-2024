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
        var map = input.Select(l => l.ToCharArray()).ToArray();

        var occurrences = new Dictionary<char, List<Tuple<int, int>>>();
        int i = 0;
        foreach (var item in map)
        {
            int j = 0;
            foreach (var item2 in item)
            {
                if (item2 == '.') { j++; continue; }
                if (!occurrences.ContainsKey(item2))
                {
                    occurrences.Add(item2, new List<Tuple<int, int>>());
                }
                occurrences[item2].Add(new Tuple<int, int>(i, j));
                j++;
            }
            i++;
        }
        int count = 0;
        foreach (var item in occurrences
        )
        {
            for (int j = 0; j < item.Value.Count; j++)
            {
                for (int k = j + 1; k < item.Value.Count; k++)
                {
                    int rowDiffO = item.Value[j].Item1 - item.Value[k].Item1;
                    int colDiffO = item.Value[j].Item2 - item.Value[k].Item2;
                    // Try every possible multiplier that could yield a coordinate inside of the map.
                    int count2 = -map.Length;
                    while (count2 < +map.Length)
                    {
                        int colDiff = colDiffO * count2;
                        int rowDiff = rowDiffO * count2;

                        try
                        {
                            if (map[item.Value[j].Item1 + rowDiff][item.Value[j].Item2 + colDiff] != '#')
                            {
                                map[item.Value[j].Item1 + rowDiff][item.Value[j].Item2 + colDiff] = '#'; count++;
                            }
                        }
                        catch { }

                        try
                        {
                            if (map[item.Value[k].Item1 - rowDiff][item.Value[k].Item2 - colDiff] != '#')
                            {
                                map[item.Value[k].Item1 - rowDiff][item.Value[k].Item2 - colDiff] = '#'; count++;
                            }
                        }
                        catch { }

                        count2++;
                    }
                }
            }
        }

        foreach (var item in map)
        {
            System.Console.WriteLine(item);
        }
        System.Console.WriteLine(count);
    }
}
