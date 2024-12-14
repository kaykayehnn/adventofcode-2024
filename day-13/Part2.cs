using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

class Part2
{

    private static double FindSmallestTokens(double buttonAX, double buttonAY, double buttonBX, double buttonBY, double prizeX, double prizeY)
    {
        /*
        buttonAX*x+buttonBX*y=prizeX
        buttonAY*x+buttonBY*y=prizeY
        
        Let's rearrange the first equation:
        buttonAX*x+buttonBX*y=prizeX
        buttonAX*x=prizeX-buttonBX*y
        x=(prizeX-buttonBX*y)/buttonAX

        And then substitute in the second equation

        buttonAY*x+buttonBY*y=prizeY
        buttonAY*((prizeX-buttonBX*y)/buttonAX)+buttonBY*y=prizeY
        buttonAY*(prizeX/buttonAX-buttonBX*y/buttonAX)+buttonBY*y=prizeY
        buttonAY*prizeX/buttonAX-buttonAY*buttonBX*y/buttonAX+buttonBY*y=prizeY
        -buttonAY*buttonBX*y/buttonAX+buttonBY*y=prizeY-buttonAY*prizeX/buttonAX
        y(buttonBY-buttonAY*buttonBX/buttonAX)=prizeY-buttonAY*prizeX/buttonAX
        y=(prizeY-buttonAY*prizeX/buttonAX)/(buttonBY-buttonAY*buttonBX/buttonAX)

        And then substitute back for X
        x=(prizeX-buttonBX*y)/buttonAX
        x=(prizeX-buttonBX*((prizeY-buttonAY*prizeX/buttonAX)/(buttonBY-buttonAY*buttonBX/buttonAX)))/buttonAX
        */
        var y = (prizeY - buttonAY * prizeX / buttonAX) / (buttonBY - buttonAY * buttonBX / buttonAX);
        var roundedY = Math.Round(y);
        // This precision may not be adequate based on the inputs. Try decreasing it in case
        // the result is incorrect.
        if (Math.Abs(y - roundedY) > 1E-4)
        {
            return 0;
        }
        var x = (prizeX - buttonBX * ((prizeY - buttonAY * prizeX / buttonAX) / (buttonBY - buttonAY * buttonBX / buttonAX))) / buttonAX;
        var roundedX = Math.Round(x);
        return roundedX * 3 + roundedY
        ;
    }

    public static void Solve(string[] args)
    {
        var input = File.ReadAllLines("/Users/krasimirnedelchev/workarea/adventofcode-2024/day-13/input.txt");
        var spentTokens = 0D;
        for (int i = 0; i < input.Length; i += 4)
        {
            var buttonA = input[i].Split(["Button A: ", "X+", ", ", "Y+"], StringSplitOptions.RemoveEmptyEntries);
            var buttonB = input[i + 1].Split(["Button B: ", "X+", ", ", "Y+"], StringSplitOptions.RemoveEmptyEntries);
            var prize = input[i + 2].Split(["Prize: ", "X=", ", ", "Y="], StringSplitOptions.RemoveEmptyEntries);
            var buttonAX = double.Parse(buttonA[0]);
            var buttonAY = double.Parse(buttonA[1]);
            var buttonBX = double.Parse(buttonB[0]);
            var buttonBY = double.Parse(buttonB[1]);
            var prizeX = double.Parse(prize[0]) + 10000000000000;
            var prizeY = double.Parse(prize[1]) + 10000000000000;

            spentTokens += FindSmallestTokens(buttonAX, buttonAY, buttonBX, buttonBY, prizeX, prizeY);
        }
        System.Console.WriteLine(spentTokens);
    }
}
