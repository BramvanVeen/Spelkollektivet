using System;
namespace ConsoleApp23
{
    internal class Program
    {
        

        static void GenerateRoad(bool[,] roads, int startX, int startY, int direction)
        {
            int width = roads.GetLength(0);
            int height = roads.GetLength(1);
            var random = new Random();
            int firstd100 = random.Next(1, 101);
            int secondd100 = random.Next(1, 101);
            int thirdd100 = random.Next(1, 101);
            int fourthd100 = random.Next(1, 101);


            switch (direction)
            {
                case 0: //right
                    if (firstd100 < 71)
                    {
                        for (int x = startX; x < width; x++)
                        {
                            roads[x, startY] = true;
                        }
                    }
                    if (secondd100 < 71)
                    {
                        for (int y = startY; y < height; y++)
                        {
                            roads[startX, y] = true;
                        }
                    }
                    if (thirdd100 < 71)
                    {
                        for (int x = startX; x >= 0; x--)
                        {
                            roads[x, startY] = true;
                        }
                    }
                    if (fourthd100 < 71)
                    {
                        for (int y = startY; y >= 0; y--)
                        {
                            roads[startX, y] = true;
                        }
                    }
                    break;
            }


        }
        static void GenerateIntersection(bool[,] roads, int x, int y)
        {
            int width = roads.GetLength(0);
            int height = roads.GetLength(1);
            var random = new Random();
            for (int i = 0; i < 5; i++)
            {
                GenerateRoad(roads, random.Next(width), random.Next(height), random.Next(1));
            }
        }


        static void Main(string[] args)
        {
            //Prepare road variables
            int width = 50;
            int height = 20;
            var roads = new bool[width, height];

            //Generate intersection


            GenerateIntersection(roads, 10, 10);


            //Draw roads
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (roads[x, y] == true)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
