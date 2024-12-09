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
        var line = input[0];
        var fs = new List<int>();
        for (int i = 0; i < line.Length; i++)
        {
            var element = (int)(line[i] - '0');
            for (var j = 0; j < element; j++)
            {
                fs.Add(i % 2 == 0 ? i / 2 : -1);
            }
        }

        var rightIndex = fs.Count - 1;
        while (true)
        {
            var leftIndex = 0;
            while (rightIndex >= fs.Count || fs[rightIndex] == -1) rightIndex--;
            int len = 1;
            while (rightIndex > 0 && fs[rightIndex] == fs[rightIndex - 1]) { rightIndex--; len++; }
            if(rightIndex == 0) break;
            int start = -1;
            while (leftIndex <= rightIndex)
            {
                if (fs[leftIndex] == -1)
                {
                    if (start == -1) start = leftIndex;
                }
                else
                {
                    if (start != -1)
                    {
                        var freeLen = leftIndex - start;
                        if (freeLen >= len)
                        {
                            for (int i = 0; i < len; i++)
                            {
                                fs[start + i] = fs[rightIndex + i];
                                fs[rightIndex + i] = -1;
                            }
                            break;
                        }
                    }

                    start = -1;
                }
                leftIndex++;
            }
            rightIndex--;
        }

        var checksum = 0L;
        for (int i = 0; i < fs.Count; i++)
        {
            var element = fs[i];
            if (element == -1) continue;
            checksum += element * i;
        }

        System.Console.WriteLine(checksum);
    }
}
