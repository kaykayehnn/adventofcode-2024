using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part1
{
    const int UNVISITED = 0;
    const int WALL = -1;
    const int START = -2;
    const int END = -3;

    const int CHEAT_LENGTH = 2;

    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
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
        // For each square from the path, try cheating in each direction.
        while (true)
        {
            var currentScore = map[startI, startJ];

            foreach (var directionVector in directionVectors)
            {
                var nextI = startI + directionVector[0] * CHEAT_LENGTH;
                var nextJ = startJ + directionVector[1] * CHEAT_LENGTH;
                // The score is decremented by 2 because the cheat steps are also counted towards it.
                if (nextI >= 0 && nextJ >= 0 && nextI < map.GetLength(0) && nextJ < map.GetLength(1) && (map[nextI, nextJ] != WALL && map[nextI, nextJ] < currentScore - 2))
                {
                    var cheatSavings = currentScore - map[nextI, nextJ] - 2;
                    if (cheatSavings >= 100) cheatCount++;
                }
            }

            if(currentScore == 1) break;
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
