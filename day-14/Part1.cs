using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part1
{
    public static void Solve(string[] args)
    {
        // int WIDTH = 11;
        // int HEIGHT = 7;
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

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < input.Length; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    positions[j, k] = (positions[j, k] + velocities[j, k] + dimensions[k]) % dimensions[k];
                }
            }
        }
        for (int i = 0; i < input.Length; i++)
        {
            map[positions[i, 0], positions[i, 1]]++;
        }
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

        var halfHeight = HEIGHT / 2;
        var halfWidth = WIDTH / 2;
        var quadrants = new[]{
            new[]{0,halfWidth,halfHeight,WIDTH},
            new[]{0,0,halfHeight,halfWidth},
            new[]{halfHeight + HEIGHT % 2,0,HEIGHT,halfWidth},
            new[]{halfHeight+ (HEIGHT % 2),halfWidth+(WIDTH %2),HEIGHT,WIDTH},
        };

        var spaceFactorProduct = 1;
        foreach (var quadrant in quadrants)
        {
            var count = 0;
            for (int i = quadrant[0]; i < quadrant[2]; i++)
            {
                for (int j = quadrant[1]; j < quadrant[3]; j++)
                {
                    count += map[i, j];
                }
            }
            spaceFactorProduct *= count;
        }
        System.Console.WriteLine(spaceFactorProduct);
    }
}
