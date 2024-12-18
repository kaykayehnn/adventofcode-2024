using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part1
{
    public static void Solve(string[] args)
    {
        const int UNVISITED = -1;
        const int WALL = -2;

        // const int SIZE = 7;
        // const int CORRUPTED_BYTES = 12;
        const  int SIZE = 71;
        const int CORRUPTED_BYTES = 1024;
        var input = File.ReadAllLines("input.txt");
        var map = new int[SIZE, SIZE];
        var adjacent = new int[4][]{
            new int[]{1,0},
            new int[]{-1,0},
            new int[]{0,1},
            new int[]{0,-1},
        };

        for (int i = 0; i < SIZE; i++)
            for (int j = 0; j < SIZE; j++)
                map[i, j] = UNVISITED;

        for (int i = 0; i < CORRUPTED_BYTES; i++)
        {
            var coords = input[i].Split(',').Select(int.Parse).ToArray();
            map[coords[1], coords[0]] = WALL;
        }

        var queue = new SortedSet<Path>
        {
            new Path() { X = 0, Y = 0, Steps = 0 }
        };

        while (queue.Count > 0)
        {
            var current = queue.Min;
            queue.Remove(current);
            if (map[current.Y, current.X] == UNVISITED)
            {
                map[current.Y, current.X] = current.Steps;
            }
            else continue;

            foreach (var pair in adjacent)
            {
                var newX = current.X + pair[0];
                var newY = current.Y + pair[1];
                if (newX < 0 || newY < 0 || newX >= SIZE || newY >= SIZE) continue;

                if (map[newY, newX] == UNVISITED)
                {
                    queue.Add(new Path() { X = newX, Y = newY, Steps = current.Steps + 1 });
                }
            }
        }

        System.Console.WriteLine(map[SIZE - 1, SIZE - 1]);
    }
}
