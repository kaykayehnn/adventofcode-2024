using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part2
{
    public static void Solve(string[] args)
    {
        int WIDTH = 101;
        int HEIGHT = 103;
        var input = File.ReadAllLines("input.txt");
        var map = new int[HEIGHT, WIDTH];
        var dimensions = new int[2] { HEIGHT, WIDTH };

        var velocities = new int[input.Length, 2];
        var positions = new int[input.Length, 2];
        for (int i = 0; i < input.Length; i++)
        {
            var data = input[i]
                .Split(["p=", ",", " ", "v="], StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            positions[i, 0] = data[1];
            positions[i, 1] = data[0];
            velocities[i, 0] = data[3];
            velocities[i, 1] = data[2];
        }

        int seconds = 1;
        while (true)
        {
            for (int j = 0; j < input.Length; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    positions[j, k] = (positions[j, k] + velocities[j, k] + dimensions[k]) % dimensions[k];
                }
            }
            for (int i = 0; i < input.Length; i++)
            {
                map[positions[i, 0], positions[i, 1]]++;
            }

            var maxConsecutivePositive = 0;


            for (int i = 0; i < HEIGHT; i++)
            {
                int consec = 0;
                for (int j = 0; j < WIDTH; j++)
                {
                    if (map[i, j] > 0) consec++;
                    else consec = 0;
                    maxConsecutivePositive = Math.Max(maxConsecutivePositive, consec);
                }
            }
            if (maxConsecutivePositive > 8)
            {


                var sb = new StringBuilder();
                for (int i = 0; i < HEIGHT; i++)
                {
                    for (int j = 0; j < WIDTH; j++)
                    {
                        sb.Append(map[i, j]);
                    }
                    sb.AppendLine();
                }
                System.Console.WriteLine(sb);
                System.Console.WriteLine($"{seconds}: {maxConsecutivePositive}");

                break;
            }
            seconds++;

            for (int i = 0; i < input.Length; i++)
            {
                map[positions[i, 0], positions[i, 1]]--;
            }
        }
    }
}
