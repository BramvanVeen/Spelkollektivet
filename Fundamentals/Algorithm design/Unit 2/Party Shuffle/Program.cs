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
            static void ShuffleList(List<string> items)
        {
                var random = new Random();
                int numberOfItemsInList = items.Count;
                while (numberOfItemsInList > 1)
                {
                    numberOfItemsInList--;
                    int chosenNameIndex = random.Next(numberOfItemsInList + 1);
                Swap(items, chosenNameIndex, 7);
                }
        }
        public static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
        static void Main(string[] args)
        {
            //Create a list of names in your Main method and fill it with names of participants.
            List<string> names = new List<string> {"Louie", "Bob", "Shlabethany", "Charlie", "Claire", "Spud", "Biffany", "KickBucket"};
            //Output the list of participants.
            Console.Write($"The participants are: {string.Join(", ", names)} ");
            Console.WriteLine();
            Console.WriteLine($"Generating starting order ...");
            ShuffleList(names);
            //Output the names again to see the new, random order.
            Console.WriteLine();
            Console.Write($"The order is: {string.Join(", ", names)} ");
            Console.WriteLine();
        }
    }
}
