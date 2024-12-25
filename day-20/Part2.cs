using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part2
{
    const int UNVISITED = 0;
    const int WALL = -1;
    const int START = -2;
    const int END = -3;

    const int CHEAT_MAX_LENGTH = 20;

    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("/Users/krasimirnedelchev/workarea/adventofcode-2024/day-20/input.txt");
        var map = new int[input.Length, input[0].Length];
        int startI = -1, startJ = -1;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                var c = input[i][j];
                if (c == '#') map[i, j] = WALL;
                else if (c == 'S')
                {
                    map[i, j] = START;
                    startI = i;
                    startJ = j;
                }
                else if (c == 'E') map[i, j] = END;
            }
        }

        var directionVectors = new int[][]{
            new int[]{0,1},
            new int[]{0,-1},
            new int[]{1,0},
            new int[]{-1,0},
        };

        // Traverse the racetrack once to set a score for each cell of the path.
        int score = 0;
        while (true)
        {
            var currentCell = map[startI, startJ];
            map[startI, startJ] = ++score;
            if (currentCell == END)
            {
                break;
            }
            foreach (var directionVector in directionVectors)
            {
                var nextI = startI + directionVector[0];
                var nextJ = startJ + directionVector[1];
                if (nextI >= 0 && nextJ >= 0 && nextI < map.GetLength(0) && nextJ < map.GetLength(1) && (map[nextI, nextJ] == UNVISITED || map[nextI, nextJ] == END))
                {
                    startI = nextI;
                    startJ = nextJ;
                    break;
                }
            }
        }

        int cheatCount = 0;
        while (true)
        {
            var currentScore = map[startI, startJ];
            // As cheats can be of variable length, we try every combination of cheated squares along the
            // horizontal and vertical axes where the total number of squares is less than the max cheat length.
            // In other words, we traverse all cells in a 20x20 box around the current cell, and calculate the cheat score
            // for every cell where the total of the xDifference and yDifference is less than the max cheat length.
            for (int i = -CHEAT_MAX_LENGTH; i <= CHEAT_MAX_LENGTH; i++)
            {
                for (int j = -CHEAT_MAX_LENGTH; j <= CHEAT_MAX_LENGTH; j++)
                {
                    int cheatStepsLength = Math.Abs(i) + Math.Abs(j);
                    if(cheatStepsLength > CHEAT_MAX_LENGTH) continue;

                    var nextI = startI + i;
                    var nextJ = startJ + j;
                    if (nextI >= 0 && nextJ >= 0 && nextI < map.GetLength(0) && nextJ < map.GetLength(1) && (map[nextI, nextJ] != WALL && map[nextI, nextJ] < currentScore - cheatStepsLength))
                    {
                        var cheatSavings = currentScore - map[nextI, nextJ] - cheatStepsLength;
                        if (cheatSavings >= 100) cheatCount++;
                    }
                }
            }

            if (currentScore == 1) break;
            foreach (var directionVector in directionVectors)
            {
                var nextI = startI + directionVector[0];
                var nextJ = startJ + directionVector[1];
                if (nextI >= 0 && nextJ >= 0 && nextI < map.GetLength(0) && nextJ < map.GetLength(1) && (map[nextI, nextJ] == currentScore - 1))
                {
                    startI = nextI;
                    startJ = nextJ;
                    break;
                }
            }
        }
        System.Console.WriteLine(cheatCount);
    }
}
