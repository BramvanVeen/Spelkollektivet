using System;
using System.Runtime.CompilerServices;

namespace Adventure_map
{
    internal class Program
    {
        static Random random = new Random();
        
        static void Map(int width, int height)
        {
            char[,] grid = new char[width, height];
            int mapQuarter = width / 4;
            int bridgeStartx = width * 3 / 4;

            void GenerateBorders(char[,] grid)
            {
                grid[0, 0] = '+';
                grid[0, height - 1] = '+';
                grid[width - 1, 0] = '+';
                grid[width - 1, height - 1] = '+';
                for (int x = 1; x < width - 1; x++)
                {
                    grid[x, 0] = '-';
                }
                for (int y = 1; y < height - 1; y++)
                {
                    grid[y, 0] = '|';
                }
                //GenerateBorders(grid);
            }
            GenerateBorders(grid);

            // //Drawing the map to console with all of the preperation from earlier, going line by line and layer by layer
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(".");
                }
                Console.WriteLine(".");
            }
        }
        
        static void Main(string[] args)
    {
        //Calling the map method with sizes, first for width, second for height. 
        Map(12, 12);
    }
}
}
