using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Party_Shuffle
{
    internal class Program
    {
        //Create a  ShuffleList(List<string> items) method
        //that shuffles the items in a list with the Fisher-Yates modern algorithm version.
        //Call the method and pass it the list of names.
        static string ShuffleList(List<string> items)
        {
            {
                var random = new Random();
                int numberOfItemsInList = items.Count;
                while (numberOfItemsInList > 1)
                {
                    numberOfItemsInList--;
                    int chosenName = random.Next(numberOfItemsInList + 1);

                    items.Remove(chosenName);
                    items.AddRange(chosenName);
                }
            }
            return items;
        }
        static void Main(string[] args)
        {
            //Create a list of names in your Main method and fill it with names of participants.
            List<string> names = new List<string> {"Louie", "Bob", "Shlabethany", "Charlie", "Claire", "Spud", "Biffany", "KickBucket"};
            //Output the list of participants.
            Console.Write($"The participants are: ");
            foreach (string name in names)
            {
                Console.Write( name);
                Console.Write(", ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Generating starting order ...");
            //Output the names again to see the new, random order.
            Console.WriteLine(ShuffleList(names));
        }
    }
}
