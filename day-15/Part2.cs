using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part2
{
    const char ROBOT = '@';
    const char WALL = '#';
    const char BOX = 'O';
    const char EMPTY_SPACE = '.';
    const char BOX_START = '[';
    const char BOX_END = ']';

    public static bool IsBox(char c)
    {
        return c == BOX_START || c == BOX_END;
    }

    public static void Solve(string[] args)
    {

        const char LEFT = '<';
        const char RIGHT = '>';
        const char UP = '^';
        const char DOWN = 'v';

        var input = File.ReadAllLines("/Users/krasimirnedelchev/workarea/adventofcode-2024/day-15/input.txt");
        var mapEnd = Array.IndexOf(input, "");

        int HEIGHT = mapEnd;
        int WIDTH = input[0].Length * 2;

        var map = new char[HEIGHT, WIDTH];
        int robotI = -1, robotJ = -1;
        for (int i = 0; i < mapEnd; i++)
        {
            var str = input[i];
            for (int j = 0; j < str.Length; j++)
            {
                if (str[j] == ROBOT)
                {
                    robotI = i;
                    robotJ = j * 2;
                    map[i, j * 2] = ROBOT;
                    map[i, j * 2 + 1] = EMPTY_SPACE;
                }
                else if (str[j] == BOX)
                {
                    map[i, j * 2] = BOX_START;
                    map[i, j * 2 + 1] = BOX_END;
                }
                else
                {
                    map[i, j * 2] = str[j];
                    map[i, j * 2 + 1] = str[j];
                }
            }
        }

        var directionVectors = new Dictionary<char, int[]>
        {
            { LEFT, new[] { 0, -1 } },
            { RIGHT, new[] { 0, 1 } },
            { UP, new[] { -1, 0 } },
            { DOWN, new[] { 1, 0 } }
        };

        for (int i = mapEnd; i < input.Length; i++)
        {
            var directions = input[i];
            // We don't need to check for out of bounds because the outermost layer of the map
            // is composed of walls.
            foreach (var direction in directions)
            {
                var directionVector = directionVectors[direction];
                var isHorizontalMove = directionVector[0] == 0;
                if (isHorizontalMove)
                {
                    // Logic remains mostly the same as in part 1.
                    int robotAndBoxesLength = 0;

                    do robotAndBoxesLength++;
                    while (IsBox(map[
                        robotI + robotAndBoxesLength * directionVector[0],
                        robotJ + robotAndBoxesLength * directionVector[1]]));

                    var isMovePossible = map[robotI + robotAndBoxesLength * directionVector[0], robotJ + robotAndBoxesLength * directionVector[1]] == EMPTY_SPACE;
                    if (isMovePossible)
                    {
                        // Mark the current robot position as empty.
                        map[robotI, robotJ] = EMPTY_SPACE;

                        // Move the robot one position in the specified direction. This replaces the first box to be moved.
                        robotI += directionVector[0];
                        robotJ += directionVector[1];
                        map[robotI, robotJ] = ROBOT;

                        for (int j = 1; j < robotAndBoxesLength; j++)
                        {
                            map[robotI + j * directionVector[0], robotJ + j * directionVector[1]] = (j % 2 == 1 ^ directionVector[1] < 0) ? BOX_START : BOX_END;
                        }
                    }
                }
                else
                {
                    // The input size is small enough that we can afford to make copies of the map
                    // on every vertical move, similarly to a transaction that we can revert in a database.
                    var mapCopy = new char[HEIGHT, WIDTH];
                    Array.Copy(map, mapCopy, map.Length);
                    try
                    {
                        MoveVertical(map, robotI, robotJ, directionVector);
                        robotI += directionVector[0];
                        robotJ += directionVector[1];
                    }
                    catch
                    {
                        map = mapCopy;
                    }
                }


                // for (int i1 = 0; i1 < map.GetLength(0); i1++)
                // {
                //     for (int j1 = 0; j1 < map.GetLength(1); j1++)
                //     {
                //         System.Console.Write(map[i1, j1]);
                //     }
                //     System.Console.WriteLine();
                // }
                // System.Console.WriteLine();
            }
        }

        int totalScore = 0;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == BOX_START)
                {
                    var leftDistance = j;
                    var score = i * 100 + leftDistance;
                    System.Console.WriteLine(score);
                    totalScore += score;
                }
            }
        }
        System.Console.WriteLine(totalScore);
    }

    private static void MoveVertical(char[,] map, int robotI, int robotJ, int[] directionVector)
    {
        var nextI = robotI + directionVector[0];
        var nextJ = robotJ + directionVector[1];
        var currentChar = map[robotI, robotJ];
        if (map[nextI, nextJ] == WALL)
        {
            throw new Exception("Invalid move operation");
        }

        if (map[nextI, nextJ] == BOX_START)
        {
            MoveVertical(map, nextI, nextJ, directionVector);
            MoveVertical(map, nextI, nextJ + 1, directionVector);
        }
        if (map[nextI, nextJ] == BOX_END)
        {
            MoveVertical(map, nextI, nextJ, directionVector);
            MoveVertical(map, nextI, nextJ - 1, directionVector);
        }

        map[nextI, nextJ] = currentChar;
        map[robotI, robotJ] = EMPTY_SPACE;
    }
}
