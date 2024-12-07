using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

class Part1
{
    private static bool BruteForce(long result, long[] operands, long targetValue, int index)
    {
        if (index == operands.Length)
        {
            return result == targetValue;
        }

        var currentOperand = operands[index];
        return BruteForce(result + currentOperand, operands, targetValue, index + 1) ||
            BruteForce(result * currentOperand, operands, targetValue, index + 1);

    }

    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");

        var sum = 0L;
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var tokens = line.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            var result = tokens[0];
            var operands = tokens.Skip(1).ToArray();
            if (BruteForce(operands[0], operands, result, 1))
            {
                sum += result;
            }
        }

        System.Console.WriteLine(sum);
    }
}
