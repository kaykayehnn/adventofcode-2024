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
        var map = input.Select(line => line.Select(c => c - '0').ToArray()).ToArray();
        var routes = map.Select(l => new long[l.Length]).ToArray();

        var totalPaths = 0;
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 0)
                {
                    var visited = map.Select(l => new bool[l.Length]).ToArray();
                    FindRoutes(map, visited, i, j, 1);
                    for(int k =0 ; k < map.Length; k++){
                        for(int l = 0; l < map.Length; l++){
                            if(map[k][l] == 9 && visited[k][l]) totalPaths++;
                        }
                    }
                }
            }
        }

        System.Console.WriteLine(totalPaths);
    }

    private static void FindRoutes(int[][] map, bool[][] visited, int row, int col, int next)
    {
        if (next > 9) return;
        var adjacents = new[]{
            [-1, 0],
            [1, 0],
            [0,1],
            new[] {0, -1},
        };
        for (int i = 0; i < adjacents.Length; i++)
        {

            int newRow = row + adjacents[i][0];
            int newCol = col + adjacents[i][1];
            if (newRow >= 0 && newCol >= 0 && newRow < map.Length && newCol < map[newRow].Length && !visited[newRow][newCol] && map[newRow][newCol] == next)
            {
                visited[newRow][newCol] = true;
                FindRoutes(map, visited, newRow, newCol, next + 1);
            }
        }
    }
}
