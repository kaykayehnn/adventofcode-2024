using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part2
{
    static Dictionary<string, List<string>> graph;
    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
        graph = new Dictionary<string, List<string>>();
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
        var graphSorted = graph.OrderByDescending(kvp => kvp.Value.Count).ToArray();

        int maxSize = graphSorted[0].Value.Count + 1;
        while(true){
            foreach (var kvp in graphSorted)
            {
                var arr = new string[maxSize];
                arr[0] = kvp.Key;
                if(CheckCombinations(kvp.Value, arr, 1, 0)) return;
            }
            maxSize--;
        }
    }

    private static bool CheckCombinations(List<string> neighbors, string[] arr, int arrIndex, int listIndex)
    {
        if(arrIndex == arr.Length) {
            // Validate
            for(int i = 1; i < arr.Length; i++){
                for(int j = 0; j < arr.Length; j++) {
                    if(i == j) continue;
                    if(!graph[arr[i]].Contains(arr[j])) return false;
                }
            }
            System.Console.WriteLine($"Found {arr.Length}: {string.Join(',', arr.OrderBy(a => a))}");
            return true;
        }

        for(int i = listIndex; i <= neighbors.Count - (arr.Length - arrIndex); i++){
            arr[arrIndex] = neighbors[i];
            if(CheckCombinations(neighbors,arr,arrIndex + 1, i+1)) return true;
        }
        return false;
    }
}
