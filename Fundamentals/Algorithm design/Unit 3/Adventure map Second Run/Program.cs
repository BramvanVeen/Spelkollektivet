using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Adventure_map_Second_Run
{
    internal class Program
    {
        static void Map(int width, int height)
        {
            var random = new Random();

            var forest = new List<char> { 'T', '@', '(', ')', '|', '%', '*' };

            void shuffleForest(List<string> items)
            {
                int numberOfItemsInList = forest.Count;
                while (numberOfItemsInList > 1)
                {
                    numberOfItemsInList--;
                    int forestSymbol = random.Next(numberOfItemsInList + 1);
                    string chosenSymbol = items[forestSymbol];
                    items.RemoveAt(forestSymbol);
                    items.Add(chosenSymbol);
                }
            }
            /*void border ()
            {
                
            }*/
            //This is the drawing part
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Decide which character to write and write it.
                    // Draw the border.
                    if (x == 0 && y == 0 || x == 0 && y == height - 1 || x == width - 1 && y == 0 || x == width - 1 && y == height - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        continue;
                    }
                    if (x == 0 || x==width-1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("|");
                        continue;

                    }
                    if (y == 0 ||y==height-1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("-");
                        continue;
                    }




                    // Draw a tree.
                    if (x<width/3&&y>0&&y<height-1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("A");
                        continue;
                    }





                    // None of the conditions were met, so this is an empty space.
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Map(75, 25);
        }
    }
}




