using System;
namespace ConsoleApp23
{
    internal class Program
    {
        static void GenerateRoad(bool[,] roads, int startX, int startY, int direction)
        {
            int width = roads.GetLength(0);
            int height = roads.GetLength(1);
            switch (direction)
            {
                case 0: //right
                    for (int right = startX; right < width; right++)
                    {
                        roads[right, startY] = true;
                    }
                    break;
                case 1: //down
                    for (int down = startY; down < height; down++)
                    {
                        roads[startX, down] = true;
                    }
                    break;
                case 2: //left
                    for (int left = startX; left >= 0; left--)
                    {
                        roads[left, startY] = true;
                    }
                    break;
                case 3: //up
                    for (int up = startY; up >= 0; up--)
                    {
                        roads[startX, up] = true;
                    }
                    break;
            }
        }
        static void GenerateIntersection(bool[,] roads, int x, int y)
        {
            for (int i = 0; i < 4; i++)
                
            {
                var random = new Random();

                int possibleRoad = random.Next(1, 101);
                if (possibleRoad < 71)
                { GenerateRoad(roads, x, y, i); }
            }
        }
        static void Main(string[] args)
        {
            //Prepare road variables
            int width = 50;
            int height = 20;
            var roads = new bool[width, height];
            var random = new Random();
            

            //Generate intersections
            for (int intersectionsNumberof = random.Next(10); intersectionsNumberof < 10; intersectionsNumberof++)
            {
                GenerateIntersection(roads, random.Next(width), random.Next(height));
            }
                 
            
            //Draw roads
            for (int Vertical = 0; Vertical < height; Vertical++)
            {
                for (int Horizontal = 0; Horizontal < width; Horizontal++)
                {
                    if (roads[Horizontal, Vertical] == true)
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
