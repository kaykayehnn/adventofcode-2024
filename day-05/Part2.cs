using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

class Part2
{
    static Dictionary<int, List<int>> rules;
    private static bool IsValid(int[] pages)
    {
        var isValid = true;
        var passedPages = new HashSet<int>();
        for (int j = 0; j < pages.Length && isValid; j++)
        {
            var page = pages[j];
            if (!rules.TryGetValue(page, out var value))
            {
                value = new List<int>();
            }
            var intersection = passedPages.Intersect(value);
            isValid = intersection.Count() == 0;
            passedPages.Add(page);
        }
        return isValid;
    }
    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");

        rules = new Dictionary<int, List<int>>();
        var hasReadRules = false;
        var sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            if (line == "")
            {
                hasReadRules = true;
                continue;
            }

            if (hasReadRules)
            {
                var pages = line.Split(',').Select(int.Parse).ToArray();
                var isValid = IsValid(pages);
                if (!isValid)
                {
                    sum += FindSolution(pages);
                }
            }
            else
            {
                var rule = line.Split('|');
                var one = int.Parse(rule[0]);
                var two = int.Parse(rule[1]);

                if (!rules.ContainsKey(one))
                {
                    rules.Add(one, new List<int>());
                }
                rules[one].Add(two);
            }
        }
        System.Console.WriteLine(sum);
    }

    private static int FindSolution(int[] arr)
    {
        var done = new bool[arr.Length];
        var doneSet = new HashSet<int>();
        // Start by adding the numbers that have no rules associated with them.
        // Then keep adding the numbers whose rules are already satisfied.
        while (doneSet.Count < arr.Length)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (done[i]) continue;
                if (!rules.TryGetValue(arr[i], out var val) || val.Where(num => arr.Contains(num) && !doneSet.Contains(num)).Count() == 0)
                {
                    done[i] = true;
                    doneSet.Add(arr[i]);
                    // We are only looking for the middle number, so we can return early.
                    if (doneSet.Count == arr.Length / 2 + 1)
                    {
                        return arr[i];
                    }
                }
            }
        }
        throw new Exception();
    }
}
