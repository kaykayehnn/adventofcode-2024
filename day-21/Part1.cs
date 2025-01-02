using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part1
{
    private static char LEFT = '<';
    private static char RIGHT = '>';
    private static char UP = '^';
    private static char DOWN = 'v';
    private static char ENTER = 'A';

    private static Dictionary<int, char> directionalCoords = new Dictionary<int, char>
        {
            { (0 << 3) | 1, '^' },
            { (0 << 3) | 2, 'A' },
            { (1 << 3) | 0, '<' },
            { (1 << 3) | 1, 'v' },
            { (1 << 3) | 2, '>' },
        };

    private static Dictionary<int, char> numericCoords = new Dictionary<int, char>
        {
            { (0<<3) | 0, '7'},
            { (0<<3) | 1, '8'},
            { (0<<3) | 2, '9'},
            { (1<<3) | 0, '4'},
            { (1<<3) | 1, '5'},
            { (1<<3) | 2, '6'},
            { (2<<3) | 0, '1'},
            { (2<<3) | 1, '2'},
            { (2<<3) | 2, '3'},
            { (3<<3) | 1, '0'},
            { (3<<3) | 2, 'A' }
        };

    private static string DecodeNumeric(char[] code)
    {
        int initialRow3 = 3;
        int initialCol3 = 2;
        var code4 = new List<char>();
        foreach (var newChar in code)
        {
            if (newChar == '<') initialCol3--;
            if (newChar == '>') initialCol3++;
            if (newChar == '^') initialRow3--;
            if (newChar == 'v') initialRow3++;
            if (initialCol3 == 0 && initialRow3 == 3) return null;

            if (newChar == 'A')
            {
                if (numericCoords.TryGetValue(initialRow3 << 3 | initialCol3, out var val))
                {
                    code4.Add(val);
                }
                else return null;
            }
        }
        return string.Join("", code4);
    }

    private static string DecodeDirectional(char[] code)
    {
        int initialRow3 = 0;
        int initialCol3 = 2;
        var code4 = new List<char>();
        foreach (var newChar in code)
        {
            if (newChar == '<') initialCol3--;
            if (newChar == '>') initialCol3++;
            if (newChar == '^') initialRow3--;
            if (newChar == 'v') initialRow3++;
            if (initialCol3 == 0 && initialRow3 == 0) return null;

            if (newChar == 'A')
            {
                if (directionalCoords.TryGetValue(initialRow3 << 3 | initialCol3, out var val))
                {
                    code4.Add(val);
                }
                else return null;
            }
        }
        return string.Join("", code4);
        // try
        // {
        //     int initialRow = 0;
        //     int initialCol = 2;
        //     int initialRow2 = 0;
        //     int initialCol2 = 2;
        //     int initialRow3 = 3;
        //     int initialCol3 = 2;
        //     var code4 = new List<char>();

        //     foreach (var newChar in code)
        //     {
        //         if (newChar == '<') initialCol--;
        //         if (newChar == '>') initialCol++;
        //         if (newChar == '^') initialRow--;
        //         if (newChar == 'v') initialRow++;
        //         if (initialCol == 0 && initialRow == 0) return null;
        //         if (newChar == 'A')
        //         {
        //             var newChar2 = directionalCoords[initialRow2 << 3 | initialCol2];
        //             if (newChar2 == '<') initialCol2--;
        //             if (newChar2 == '>') initialCol2++;
        //             if (newChar2 == '^') initialRow2--;
        //             if (newChar2 == 'v') initialRow2++;
        //             if (initialCol2 == 0 && initialRow2 == 0) return null;
        //             if (newChar2 == 'A')
        //             {
        //                 var newChar3 = directionalCoords[initialRow3 << 3 | initialCol3];
        //                 if (newChar3 == '<') initialCol3--;
        //                 if (newChar3 == '>') initialCol3++;
        //                 if (newChar3 == '^') initialRow3--;
        //                 if (newChar3 == 'v') initialRow3++;
        //                 if (initialCol3 == 0 && initialRow3 == 3) return null;

        //                 if (newChar3 == 'A') code4.Add(numericCoords[initialRow3 << 3 | initialCol3]);
        //             }
        //         }
        //     }

        //     return string.Join("", code4);
        // }
        // catch
        // {
        //     return null;
        // }

    }

    private static int SolveCode(string code)
    {
        var bestPrefixes = new HashSet<string>();
        bestPrefixes.Clear();
        bestPrefixes.Add("");
        for (int i = 0; i < code.Length; i++)
        {
            var savedPrefixes = bestPrefixes.ToArray();
            bestPrefixes.Clear();
            var searchedString = string.Join("", code.Take(i + 1));
            for (int j = 1; ; j++)
            {
                var arr = new char[j];
                foreach (var sp in savedPrefixes)
                {
                    if (j < sp.Length) continue;
                    for (int k = 0; k < sp.Length; k++)
                    {
                        arr[k] = sp[k];
                    }
                    GenerateCodes(arr, bestPrefixes, sp.Length, searchedString, 1);
                }
                if (bestPrefixes.Count > 0)
                {
                    break;
                }
            }
        }

        var savedPrefixes2 = bestPrefixes.ToArray();
        var forNextIteration = new HashSet<string>();
        var currentBestLength = int.MaxValue;
        foreach (var code2 in savedPrefixes2)
        {
            bestPrefixes.Clear();
            bestPrefixes.Add("");

            for (int i = 0; i < code2.Length; i++)
            {
                var savedPrefixes = bestPrefixes.ToArray();
                bestPrefixes.Clear();
                var searchedString = string.Join("", code2.Take(i + 1));
                for (int j = 1; j <= currentBestLength; j++)
                {
                    var arr = new char[j];
                    foreach (var sp in savedPrefixes)
                    {
                        if (j < sp.Length) continue;
                        for (int k = 0; k < sp.Length; k++)
                        {
                            arr[k] = sp[k];
                        }
                        GenerateCodes(arr, bestPrefixes, sp.Length, searchedString, 2);
                    }
                    if (bestPrefixes.Count > 0)
                    {
                        break;
                    }
                }
            }
            foreach (var bp in bestPrefixes)
            {
                currentBestLength = Math.Min(bp.Length, currentBestLength);
            }
            forNextIteration.UnionWith(bestPrefixes);
        }

        var savedPrefixes3 = forNextIteration.ToArray();
        currentBestLength = int.MaxValue;
        foreach (var code3 in savedPrefixes3)
        {
            bestPrefixes.Clear();
            bestPrefixes.Add("");

            for (int i = 0; i < code3.Length; i++)
            {
                var savedPrefixes = bestPrefixes.ToArray();
                bestPrefixes.Clear();
                var searchedString = string.Join("", code3.Take(i + 1));
                for (int j = 1; j <= currentBestLength; j++)
                {
                    var arr = new char[j];
                    foreach (var sp in savedPrefixes)
                    {
                        if (j < sp.Length) continue;
                        for (int k = 0; k < sp.Length; k++)
                        {
                            arr[k] = sp[k];
                        }
                        GenerateCodes(arr, bestPrefixes, sp.Length, searchedString, 2);
                    }
                    if (bestPrefixes.Count > 0)
                    {
                        break;
                    }
                }
            }

            foreach (var bp in bestPrefixes)
            {
                currentBestLength = Math.Min(bp.Length, currentBestLength);
            }
        }
        System.Console.WriteLine($"{code}: {currentBestLength}");
        return currentBestLength * int.Parse(code.Substring(0, code.Length - 1));
    }

    public static void Solve(string[] args)
    {
        var totalSum = 0;
        var input = File.ReadAllLines("input.txt");
        var tasks = input.Select(code => Task.Run(() => SolveCode(code))).ToArray();

        Task.WaitAll(tasks);

        foreach (var task in tasks)
        {
            totalSum += task.Result;
        }
        System.Console.WriteLine(totalSum);
    }

    private static void GenerateCodes(char[] chars, HashSet<string> bestPrefixes, int index, string match, int level)
    {
        if (index == chars.Length)
        {
            string value = null;
            if (level == 1) value = DecodeNumeric(chars);
            else if (level == 2) value = DecodeDirectional(chars);
            if (value != null)
            {
                if (value == match)
                {
                    bestPrefixes.Add(string.Join("", chars));
                }
            }
            return;
        }

        chars[index] = LEFT;
        GenerateCodes(chars, bestPrefixes, index + 1, match, level);
        chars[index] = RIGHT;
        GenerateCodes(chars, bestPrefixes, index + 1, match, level);
        chars[index] = DOWN;
        GenerateCodes(chars, bestPrefixes, index + 1, match, level);
        chars[index] = UP;
        GenerateCodes(chars, bestPrefixes, index + 1, match, level);
        chars[index] = ENTER;
        GenerateCodes(chars, bestPrefixes, index + 1, match, level);
    }
}
