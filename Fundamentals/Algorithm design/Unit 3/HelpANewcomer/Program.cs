using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        var data = new List<double>();
        var random = new Random(); // Create a Random instance

        for (int i = 0; i < 70; i++)
        {
            data.Add(random.Next(20));
            DisplayData(data);
        }
    }

    static void DisplayData(List<double> data)
    {
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 0);

        for (int y = 20; y >= 0; y--)
        {
            if (y % 5 == 0)
            {
                Console.Write($"{y,3} |");
            }
            else
            {
                Console.Write("    |");
            }

            for (var x = 0; x < data.Count; x++)
            {
                if (y == 0) // Use double equals for comparison
                {
                    Console.Write("-");
                    continue; // Use continue instead of next
                }

                Console.Write(y <= data[x] ? "\u2592" : " ");
            }

            Console.WriteLine();
        }

        Thread.Sleep(10);
    }
}
