using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Party_Shuffle
{
    internal class Program
    {
            static void ShuffleList(List<string> items)
        {
                var random = new Random();
                int numberOfItemsInList = items.Count;
                while (numberOfItemsInList > 1)
                {
                    numberOfItemsInList--;
                    int chosenNameIndex = random.Next(numberOfItemsInList + 1);
                string chosenName = items[chosenNameIndex];
                items.RemoveAt(chosenNameIndex);
                items.Add(chosenName);
            }
        }
        static void Main(string[] args)
        {
            List<string> names = new List<string> {"Louie", "Bob", "Shlabethany", "Spud", "Charlie", "Claire", "Biffany", "KickBucket"};
            Console.Write($"The participants are: {string.Join(", ", names)} ");
            Console.WriteLine();
            Console.WriteLine($"Generating starting order ...");
            ShuffleList(names);
            Console.WriteLine();
            Console.Write($"The order is: {string.Join(", ", names)} ");
            Console.WriteLine();
        }
    }
}
