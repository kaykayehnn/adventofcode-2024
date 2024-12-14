using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

class Program
{
    public static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "Part2.cs")
        {
            Part2.Solve(args);
            return;
        }
        Part1.Solve(args);
    }
}
