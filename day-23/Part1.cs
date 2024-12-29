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
        var graph = new Dictionary<string, List<string>>();
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i].Split('-');
            if (!graph.ContainsKey(line[0]))
            {
                graph.Add(line[0], new List<string>());
            }
            if (!graph.ContainsKey(line[1]))
            {
                graph.Add(line[1], new List<string>());
            }

            graph[line[0]].Add(line[1]);
            graph[line[1]].Add(line[0]);
        }

        var uniqueTriplets = new HashSet<string>();
        var stronglyConnectedComponents = new List<HashSet<string>>();
        foreach (var kvp in graph)
        {
            var nodeName = kvp.Key;
            if (nodeName[0] != 't') continue;

            for (int i = 0; i < kvp.Value.Count; i++)
            {
                // The current node is always connected to the first and second nodes
                for (int j = i + 1; j < kvp.Value.Count; j++)
                {
                    // We only need to check if nodes i and j are connected
                    if (graph[kvp.Value[i]].Contains(kvp.Value[j]))
                    {
                        // Found triplet
                        uniqueTriplets.Add(GetTriplet(nodeName, kvp.Value[i], kvp.Value[j]));
                    }
                }
            }
        }
        System.Console.WriteLine(uniqueTriplets.Count);
    }

    private static string GetTriplet(string a, string b, string c)
    {
        var arr = new[] { a, b, c };
        Array.Sort(arr);
        return string.Join('-', arr);
    }
}
