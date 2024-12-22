using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq.Expressions;

class Part1
{
    public static void Solve(string[] args)
    {
        const int UNVISITED_SPACE = 0;
        const int WALL = -1;
        const int END = -2;
        const int MOVE_SCORE_INCREMENT = 1;
        const int ROTATE_SCORE_INCREMENT = 1000;

        var input = File.ReadAllLines("input.txt");
        var map = new int[input.Length, input[0].Length];
        var visited = new bool[input.Length, input[0].Length, 4];
        int startI = -1, startJ = -1;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                var element = input[i][j];
                if (element == '#') map[i, j] = WALL;
                if (element == 'E') map[i, j] = END;
                if (element == 'S')
                {
                    startI = i;
                    startJ = j;
                }
            }
        }

        var queue = new SortedSet<Path>();
        queue.Add(new Path()
        {
            I = startI,
            J = startJ,
            Direction = 0,
            Score = 0
        });

        while (queue.Count > 0)
        {
            var current = queue.Min;
            queue.Remove(current);
            if (map[current.I, current.J] == END)
            {
                System.Console.WriteLine(current.Score);
                return;
            }
            if (map[current.I, current.J] != UNVISITED_SPACE) continue;
            if(visited[current.I, current.J, current.Direction]) continue;
            visited[current.I, current.J, current.Direction] = true;

            var vector = Path.GetDirectionVector(current.Direction);
            int nextI = current.I + vector.Y;
            int nextJ = current.J + vector.X;
            int nextScore = current.Score + MOVE_SCORE_INCREMENT;
            while (nextI >= 0 && nextJ >= 0 && nextI < input.Length && nextJ < input[0].Length
            && map[nextI, nextJ] != WALL)
            {
                queue.Add(new Path()
                {
                    I = nextI,
                    J = nextJ,
                    Direction = current.Direction,
                    Score = nextScore
                });
                nextI += vector.Y;
                nextJ += vector.X;
                nextScore += MOVE_SCORE_INCREMENT;
            }

            var newDirections = new int[] { current.Direction + 1, current.Direction - 1 };
            foreach (var newDirection in newDirections)
            {
                if (visited[current.I, current.J, (4 + newDirection) % 4]) continue;

                queue.Add(new Path()
                {
                    I = current.I,
                    J = current.J,
                    Score = current.Score + ROTATE_SCORE_INCREMENT,
                    Direction = newDirection
                });
            }
        }
    }
}
