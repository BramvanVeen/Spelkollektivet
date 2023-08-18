using System;
using System.Runtime.CompilerServices;

namespace Adventure_map
{
    internal class Program
    {
        static Random random = new Random();
        static void Map(int width, int height)
        {
            int kwart = width / 4;
            int driekwart = kwart * 3;
            int bridgestartingpoint = random.Next(driekwart, width - 1);

            // //Drawing the map to console with all of the preperation from earlier, going line by line and layer by layer
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Decide which character to write and write it.

                    //This is the border: (Top)
                    //Will always be the same
                    if (x == 0  && y == 0)
                    {
                        Console.Write("+");
                    }
                    if (x > 0 && y == 0)
                    {
                        Console.Write("-");
                    }
                    if (x == width - 1 && y == 0)
                    {
                        Console.WriteLine("+");
                    }

                    //This is the last line (Like the first it will always be the same.
                    if (x == 0 && y == height - 1)
                    {
                        Console.Write("+");
                    }
                    if (x > 0 && y == height - 1)
                    {
                        Console.Write("-");
                    }
                    if (x == width - 1 && y == height - 1)
                    {
                        Console.WriteLine("+");
                    }

                    //I need to figure out a way for every other line to begin and end with:｜ 
                    //Next is to place a bridge
                    //Decide where the quarters of the maps are. Divide the width by four, get a kwart result
                    //1 kwart is forest
                    //3 * kwart width is where possibly the bridge starts 

                    //Lets try to randomly generate a bridge somewhere:

                    
                    {
                        //Console.WriteLine("=");
                    }


                }
                Console.WriteLine();
            }

        }
        static void Main(string[] args)
        {
            //Calling the map method with sizes, first for width, second for height. 
            Map(20, 10);
        }
        
    }
}
