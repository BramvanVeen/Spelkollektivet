﻿using System;
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

                    for (int x = startX; x < width; x++)
                    {
                        roads[x, startY] = true;
                    }
                    break;
                case 1: //down
                    for (int y = startY; y < height; y++)
                    {
                        roads[startX, y] = true;
                    }
                    break;
                case 2: //left
                    for (int x = startX; x >= 0; x--)
                    {
                        roads[x, startY] = true;
                    }
                    break;
                case 3: //up
                    for (int y = startY; y >= 0; y--)
                    {
                        roads[startX, y] = true;
                    }
                    break;
            }

            
        }
        static void GenerateIntersection(bool[,] roads, int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                int width = 50;
                int height = 20;
                var random = new Random();
                int possibleRoad = random.Next(1, 101);
                if (possibleRoad < 71)
                { GenerateRoad(roads, random.Next(width), random.Next(height), random.Next(4)); }
            }
        }
        static void Main(string[] args)
        {
            //Prepare road variables
            int width = 50;
            int height = 20;
            var random = new Random();
            var roads = new bool[width, height];

            //Generate random intersections
            for (int i = 0; i < 1; i++)
            { 
                GenerateIntersection(roads, random.Next(width), random.Next(height)); 
            }

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
