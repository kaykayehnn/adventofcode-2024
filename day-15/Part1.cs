using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Part1
{
    public static void Solve(string[] args)
    {
        const char ROBOT = '@';
        const char WALL = '#';
        const char BOX = 'O';
        const char EMPTY_SPACE = '.';

        const char LEFT = '<';
        const char RIGHT = '>';
        const char UP = '^';
        const char DOWN = 'v';

        var input = File.ReadAllLines("/Users/krasimirnedelchev/workarea/adventofcode-2024/day-15/input.txt");
        var mapEnd = Array.IndexOf(input, "");

        var map = new char[mapEnd, input[0].Length];
        int robotI = -1, robotJ = -1;
        for (int i = 0; i < mapEnd; i++)
        {
            var str = input[i];
            for (int j = 0; j < str.Length; j++)
            {
                if (str[j] == ROBOT)
                {
                    robotI = i;
                    robotJ = j;
                }
                map[i, j] = str[j];
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
                int robotAndBoxesLength = 0;

                do robotAndBoxesLength++;
                while (map[
                    robotI + robotAndBoxesLength * directionVector[0],
                    robotJ + robotAndBoxesLength * directionVector[1]] == BOX);

                var isMovePossible = map[robotI + robotAndBoxesLength * directionVector[0], robotJ + robotAndBoxesLength * directionVector[1]] == EMPTY_SPACE;
                if (isMovePossible)
                {
                    // Mark the current robot position as empty.
                    map[robotI, robotJ] = EMPTY_SPACE;

                    // Add a new box directly after the last box.
                    map[robotI + robotAndBoxesLength * directionVector[0], robotJ + robotAndBoxesLength * directionVector[1]] = BOX;

                    // Move the robot one position in the specified direction. This replaces the first box to be moved.
                    robotI += directionVector[0];
                    robotJ += directionVector[1];
                    map[robotI, robotJ] = ROBOT;
                }
            }

        }

        int totalScore = 0;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == BOX) totalScore += i * 100 + j;
            }
        }
        System.Console.WriteLine(totalScore);
    }
}
