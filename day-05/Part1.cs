using System;
// using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

class Part1
{
    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");

        var rules = new Dictionary<int, List<int>>();
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
                var isValid = true;
                var passedPages = new HashSet<int>();
                for (int j = 0; j < pages.Length - 1 && isValid; j++)
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
                if (isValid)
                {
                    sum += pages[pages.Length / 2];
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
}
