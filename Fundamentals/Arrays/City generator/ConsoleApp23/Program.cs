using System;
namespace ConsoleApp23
{
    internal class Program
    {
        static void GenerateRoad(bool[,] roads, int startX, int startY, int direction)
        {
            roads[startX, startY] = true;
            if (roads[startX, startY] == true)
            { Console.WriteLine($"#"); }  

            switch (direction)
            {
                case 0: //UP
                case 1: //down
                case 2: // left
                default: // right
                    break;
            }
        }
        static void Main(string[] args)
        {
            var random = new Random();
            
            int width = 50;
            int height = 50;
            var roads = new bool[width, height];

            GenerateRoad(roads, 25, 25, 0);
        }
    }
}
