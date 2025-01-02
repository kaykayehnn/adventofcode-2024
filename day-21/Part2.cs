using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Part2
{
    private static char LEFT = '<';
    private static char RIGHT = '>';
    private static char UP = '^';
    private static char DOWN = 'v';
    private static char ENTER = 'A';

    private static string TypeNumeric(string code)
    {
        /*
            +---+---+---+
            | 7 | 8 | 9 |
            +---+---+---+
            | 4 | 5 | 6 |
            +---+---+---+
            | 1 | 2 | 3 |
            +---+---+---+
                | 0 | A |
                +---+---+
        */
        var coords = new Dictionary<char, int[]>
        {
            { '7', new int[] { 0, 0 } },
            { '8', new int[] { 0, 1 } },
            { '9', new int[] { 0, 2 } },
            { '4', new int[] { 1, 0 } },
            { '5', new int[] { 1, 1 } },
            { '6', new int[] { 1, 2 } },
            { '1', new int[] { 2, 0 } },
            { '2', new int[] { 2, 1 } },
            { '3', new int[] { 2, 2 } },
            { '0', new int[] { 3, 1 } },
            { 'A', new int[] { 3, 2 } }
        };

        int initialRow = 3;
        int initialCol = 2;
        var sb = new StringBuilder();
        for (int i = 0; i < code.Length; i++)
        {
            var newCoords = coords[code[i]];
            var rowDiff = initialRow - newCoords[0];
            var colDiff = initialCol - newCoords[1];
            var updo = true;

            // https://www.reddit.com/r/adventofcode/comments/1hjgyps/2024_day_21_part_2_i_got_greedyish/
            if (initialRow == 3 && newCoords[1] == 0)
            {
                updo = true;
            }
            else if (initialCol == 0 && newCoords[0] == 3)
            {
                updo = false;
            }
            else if (rowDiff > 0 && colDiff > 0)
            {
                updo = false;
            }
            else if (rowDiff < 0 && colDiff > 0)
            {
                updo = false;
            }
            else if (rowDiff < 0 && colDiff < 0)
            {
                updo = true;
            }
            else if (rowDiff > 0 && colDiff < 0)
            {
                updo = true;
            }

            if (updo)
            {
                while (rowDiff > 0)
                {
                    sb.Append(UP);
                    rowDiff--;
                }
                while (rowDiff < 0)
                {
                    sb.Append(DOWN);
                    rowDiff++;
                }

            }

            while (colDiff < 0)
            {
                sb.Append(RIGHT);
                colDiff++;
            }
            while (colDiff > 0)
            {
                sb.Append(LEFT);
                colDiff--;
            }

            if (!updo)
            {
                while (rowDiff > 0)
                {
                    sb.Append(UP);
                    rowDiff--;
                }
                while (rowDiff < 0)
                {
                    sb.Append(DOWN);
                    rowDiff++;
                }
            }

            initialRow = newCoords[0];
            initialCol = newCoords[1];
            sb.Append(ENTER);
        }

        // This implementation might not work if there are for cases such as 022A. We can filter the result list
        // for empty arrays.
        return sb.ToString();
    }

    private static string TypeDirectional(string code)
    {
        /*
                +---+---+
                | ^ | A |
            +---+---+---+
            | < | v | > |
            +---+---+---+
        */
        var coords = new Dictionary<char, int[]>
        {
            { '^', new int[] { 0, 1 } },
            { 'A', new int[] { 0, 2 } },
            { '<', new int[] { 1, 0 } },
            { 'v', new int[] { 1, 1 } },
            { '>', new int[] { 1, 2 } },
        };
        var coordsPriority = new Dictionary<int, string>
        {
            /* ^ */{ (0<<3)|1, "^v<>A" },
            /* A */{ (0<<3)|2, "v><^A" },
            /* < */{ (1<<3)|0, "<v>^A" },
            /* v */{ (1<<3)|1, "v<^>A" },
            /* > */{ (1<<3)|2, ">v<^A" }
        };

        int initialRow = 0;
        int initialCol = 2;
        var sb = new StringBuilder();
        for (int i = 0; i < code.Length; i++)
        {
            var newCoords = coords[code[i]];
            var rowDiff = initialRow - newCoords[0];
            var colDiff = initialCol - newCoords[1];
            var updo = true;
            if (initialRow == 1 && initialCol == 0)
            {
                updo = false;
            }
            else if (newCoords[0] == 1 && newCoords[1] == 0)
            {
                updo = true;
            }
            else if (rowDiff > 0 && colDiff > 0)
            {
                updo = false;
            }
            else if (rowDiff < 0 && colDiff > 0)
            {
                updo = false;
            }
            else if (rowDiff < 0 && colDiff < 0)
            {
                updo = true;
            }
            else if (rowDiff > 0 && colDiff < 0)
            {
                updo = true;
            }

            if (updo)
            {
                while (rowDiff > 0)
                {
                    sb.Append(UP);
                    rowDiff--;
                }
                while (rowDiff < 0)
                {
                    sb.Append(DOWN);
                    rowDiff++;
                }

            }

            while (colDiff < 0)
            {
                sb.Append(RIGHT);
                colDiff++;
            }
            while (colDiff > 0)
            {
                sb.Append(LEFT);
                colDiff--;
            }

            if (!updo)
            {
                while (rowDiff > 0)
                {
                    sb.Append(UP);
                    rowDiff--;
                }
                while (rowDiff < 0)
                {
                    sb.Append(DOWN);
                    rowDiff++;
                }
            }

            initialRow = newCoords[0];
            initialCol = newCoords[1];
            sb.Append(ENTER);

        }
        return sb.ToString();
    }

    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("/Users/krasimirnedelchev/workarea/adventofcode-2024/day-21/input.txt");
        var total = 0L;
        var uniqueDirectionalSequences = new HashSet<string>();

        foreach (var code in input)
        {
            var numericCode = TypeNumeric(code);
            var directionalFirst = TypeDirectional(numericCode);
            // Loop through two levels of directional codes to generate all possible directional sequences.
            for (int i = 0; i < 12; i++)
            {
                var matches = Regex.Matches(directionalFirst, @"[^A]+A+");
                uniqueDirectionalSequences.UnionWith(matches.Select(a=>a.Value));
                directionalFirst = TypeDirectional(directionalFirst);
            }
        }

        // Compute the resulting number of elements at the 13th iteration for each of the unique blocks.
        var results = new Dictionary<string, int>();
        foreach (var sequence in uniqueDirectionalSequences)
        {
            var directionalCode = TypeDirectional(sequence);
            for (int i = 0; i < 12; i++)
            {
                directionalCode = TypeDirectional(directionalCode);
            }
            results.Add(sequence, directionalCode.Length);
        }

        foreach (var code in input)
        {
            var numericCode = TypeNumeric(code);
            var directionalFirst = TypeDirectional(numericCode);
            long finalLength = directionalFirst.Length;
            // Loop through two levels of directional codes to generate all possible directional sequences.
            for (int i = 0; i < 12; i++)
            {
                long sum = 0;
                var matches = Regex.Matches(directionalFirst, @"[^A]+A+");
                foreach (Match m in matches)
                {
                    sum+=results[m.Value];
                }

                finalLength = sum;
                directionalFirst = TypeDirectional(directionalFirst);
            }

            System.Console.WriteLine($"{code}: {finalLength}");
            int numericValue = int.Parse(code.Substring(0, code.Length - 1));
            var product = numericValue * finalLength;
            total += product;
        }
        System.Console.WriteLine(total);
    }
}
