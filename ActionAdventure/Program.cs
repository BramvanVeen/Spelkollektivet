using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading.Tasks.Dataflow;
using static Adventure_map.Program;

namespace Adventure_map
{
    public class Program
    {
        //Declare grid data structures (the grid will be filled with characters, so a grid of chars, and a grid detailing which color the chars should be.
        static char[,] Grid;
        static ConsoleColor[,] GridColor;
        static int Width;
        static int Height;

        //This method prepares the information to be written onto the map before we send it on to be drawn.
        static void SetGridCharAndColor(char symbol, ConsoleColor color, int x, int y)
        {
            Grid[x, y] = symbol;
            GridColor[x, y] = color;
        }

       

        


        //The Map method. Takes width and heigth to create a 2 dimensional grid. Static because it is intended to be a standalone method that does not operate on an instance of a class.
        static void drawMap(int width, int height)
        {
            Random random = new Random();
            Grid = new char[width, height];
            GridColor = new ConsoleColor[width, height];
            Width = width;
            Height = height;

            //Drawing the map to console with all of the preperation from earlier, going line by line and layer by layer
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (Grid[x, y] == '\0')
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.ForegroundColor = GridColor[x, y];
                        Console.Write(Grid[x, y]);
                    }
                    if (x == width - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
        public static void Main(string[] args)
        {
            //Calling the map method with sizes, first for width, second for height. 
            drawMap(39, 21);
        }
    }
}
