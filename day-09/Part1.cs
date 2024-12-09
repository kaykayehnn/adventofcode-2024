using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

class Part1
{
    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
        var line = input[0];
        var fs = new List<int>();
        for(int i = 0 ; i < line.Length; i++)
        {
            var element = (int)(line[i]- '0');
            for(var j =0;j < element; j++){
                fs.Add(i % 2 == 0 ? i / 2 : -1);
            }
        }

        while(true){
            var lastElement = fs.FindLastIndex(a=>a!=-1);
            var firstMinusOne = fs.FindIndex(a => a == -1);
            if(lastElement > firstMinusOne && lastElement != -1 && firstMinusOne != -1) {
                fs[firstMinusOne] = fs[lastElement];
                fs.RemoveAt(lastElement);
            } else break;
        }

        var checksum = 0L;
        for (int i = 0; i < fs.Count; i++)
        {
            var element = fs[i];
            if(element == -1) break;
            checksum += element * i;
        }

        System.Console.WriteLine(checksum);
    }
}
