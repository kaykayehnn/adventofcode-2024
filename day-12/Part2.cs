using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

class Part2
{
    private static char[][] map;
    private static char[][] visitedCells;
    private static bool[,,] walls;
    private static bool[,] visitedWalls;
    private static int[][] adjacent = [
        new[]{1,0},
        new[]{-1,0},
        new[]{0,1},
        new[]{0,-1},
    ];
    private static int regionArea = 0;
    private static int regionWalls = 0;
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

            ExploreRegion(newI, newJ);
        }

        for (int k = 0; k < adjacent.Length; k++)
        {
            var newI = i + adjacent[k][0];
            var newJ = j + adjacent[k][1];
            var isEnd = newI < 0 || newJ < 0 || newI >= map.Length || newJ >= map[newI].Length || map[newI][newJ] != cell;
            walls[i, j, k] = isEnd;
        }
    }

    private static void CountWalls(int i, int j)
    {
        var cell = map[i][j];
        visitedWalls[i, j] = true;

        for (int k = 0; k < adjacent.Length; k++)
        {
            if (!walls[i, j, k]) continue;
            var newI = i + adjacent[k][1];
            var newJ = j + adjacent[k][0];
            var isEndOfWall = newI < 0 || newJ < 0 || newI >= map.Length || newJ >= map[newI].Length || map[newI][newJ] != cell || !walls[newI,newJ,k];
            if (isEndOfWall) regionWalls++;
        }
        foreach (var pair in adjacent)
        {
            var newI = i + pair[0];
            var newJ = j + pair[1];
            if (newI < 0 || newJ < 0 || newI >= map.Length || newJ >= map[newI].Length || visitedWalls[newI, newJ] || map[newI][newJ] != cell) continue;


            CountWalls(newI, newJ);
        }
    }

    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
        map = input.Select(line => line.ToCharArray()).ToArray();

        visitedCells = input.Select(l => new char[l.Length]).ToArray();
        // Bools for each 4 walls
        walls = new bool[map.Length, map[0].Length, 4];
        visitedWalls = new bool[map.Length, map[0].Length];

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
                        regionWalls = 0;
                        ExploreRegion(i, j);
                        CountWalls(i, j);
                        sum += regionArea * regionWalls;
                        System.Console.WriteLine(regionArea + " " + regionWalls);
                    }
                }
            }
        } while (hasFoundCell);
        System.Console.WriteLine(sum);
    }
}
