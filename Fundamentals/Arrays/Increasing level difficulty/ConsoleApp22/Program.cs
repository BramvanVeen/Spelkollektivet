using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            int[] levelsArray = new int[100];
            Console.Write($"Number of monsters in levels:");
            for (int level = 0; level < 100; level++)
            {
                levelsArray[level] = random.Next(1, 51);
            }
            Array.Sort(levelsArray);
            Console.Write(string.Join(", ", levelsArray));
            Console.WriteLine();
        }
    }
}
