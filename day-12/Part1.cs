using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

class Part1
{
    private static char[][] map;
    private static char[][] visitedCells;
    private static int[][] adjacent = [
        new[]{1,0},
        new[]{-1,0},
        new[]{0,1},
        new[]{0,-1},
    ];
    private static int regionArea = 0;
    private static int regionPerimeter = 0;
    private static void ExploreRegion(int i, int j)
    {
        var cell = map[i][j];
        visitedCells[i][j] = cell;
        regionArea++;
        foreach (var pair in adjacent)
        {
            var newI = i + pair[0];
            var newJ = j + pair[1];
            if (newI < 0 || newJ < 0 || newI >= map.Length || newJ >= map[newI].Length || visitedCells[newI][newJ] == cell || map[newI][newJ] != cell) continue;

            var perimeter = 4;
            foreach (var pair2 in adjacent)
            {
                var newI2 = newI + pair2[0];
                var newJ2 = newJ + pair2[1];
                if (newI2 < 0 || newJ2 < 0 || newI2 >= map.Length || newJ2 >= map[newI2].Length || visitedCells[newI2][newJ2] != cell) continue;
                perimeter -= 2;
            }
            regionPerimeter += perimeter;
            ExploreRegion(newI, newJ);
        }
    }

    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
        map = input.Select(line => line.ToCharArray()).ToArray();

        visitedCells = input.Select(l => new char[l.Length]).ToArray();
        bool hasFoundCell;
        var sum = 0;
        do
        {
            hasFoundCell = false;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (visitedCells[i][j] == 0)
                    {
                        hasFoundCell = true;
                        regionArea = 0;
                        regionPerimeter = 4;
                        ExploreRegion(i, j);
                        sum += regionArea * regionPerimeter;
                        System.Console.WriteLine(regionArea + " " + regionPerimeter);
                    }
                }
            }
        } while (hasFoundCell);
        System.Console.WriteLine(sum);
    }
}
