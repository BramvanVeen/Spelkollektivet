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

namespace Adventure_map
{
    public class Program
    {
        enum Direction
        {
            North,
            South,
            West,
            East
        }
        public class MapElement
        {
            public char[] Symbols;
            public ConsoleColor Color;
            public int Width;
            public int CurveChance;
            public MapElement(char[] symbols, ConsoleColor color, int width, int curveChance)
            {
                Symbols = symbols;
                Color = color;
                Width = width;
                CurveChance = curveChance;
            }
        }
        static MapElement WallElement = new(new char[] { '\\', '|', '/' }, ConsoleColor.Gray, 2, 10);
        static MapElement HiddenPathElement = new(new char[] { '\\', '|', '/' }, ConsoleColor.DarkGreen, 1, 3);
        static MapElement RoadElement = new(new char[] { '#', '#', '#' }, ConsoleColor.Magenta, 1, 6);
        static MapElement RiverElement = new(new char[] { '\\', '|', '/' }, ConsoleColor.Blue, 3, 3);
        static MapElement ForestElement = new(new char[] { 'T', '@', '(', ')', '!', '%', '*' }, ConsoleColor.Green, 1, 1);
        //The Map method. Takes width and heigth to create a 2 dimensional grid. Static because it is intended to be a standalone method that does not operate on an instance of a class.
        static void Map(int width, int height)
        {
            // is needed to create an instance of the Random class, which is used to generate random numbers in the program.
            Random random = new Random();
            //Declare grid data structures (the grid will be filled with characters, so a grid of chars, and a grid detailing which color the chars should be, resulting in a kickass map.
            char[,] grid = new char[width, height];
            ConsoleColor[,] gridColor = new ConsoleColor[width, height];
            //This method prepares the information to be written onto the map before we send it on to be drawn.
            void SetGridCharAndColor(char symbol, ConsoleColor color, int x, int y)
            {
                grid[x, y] = symbol;
                gridColor[x, y] = color;
            }
            //Prepare the Bridge starting point and subsequent markerpoints for all other elements:
            int bridgeStartx = width * 3 / 4;
            int bridgeStarty = random.Next(height / 4, height * 3 / 4);

            int markerPointxForRoadGoingLeft = bridgeStartx - 1;
            int markerPointyForRoadGoingLeft = bridgeStarty + 1;

            int MarkerPointxForRoadGoingRight = markerPointxForRoadGoingLeft + 5;
            int MarkerPointyForRoadGoingRight = markerPointyForRoadGoingLeft;

            int markerxForRiverFlowingDown = bridgeStartx + 2;
            int markeryForRiverFlowingDown = bridgeStarty + 3;

            int markerxForRiverFlowingUp = bridgeStartx + 2;
            int markeryForRiverFlowingUp = bridgeStarty - 1;

            int sideRoadMarkerx = markerPointxForRoadGoingLeft;
            int sideRoadMarkery = markerPointyForRoadGoingLeft + 1;

            int mapQuarter = width / 4;
            int quarterQuarter = mapQuarter / 4;
            int doubleQuarterQuarter = quarterQuarter * 2;
            int threeQuarterQuarter = quarterQuarter * 3;

            //This method takes care of randomly snaking the various elements into a singular direction.
            void Snake(int x, int y, Direction direction, MapElement mapElement)
            {
                while (true)
                {
                    int directionModifier = random.Next(mapElement.CurveChance) - 1;
                    if (directionModifier > 1)
                    {
                        directionModifier = 0;
                    }
                    switch (direction)
                    {
                        case Direction.North:
                            y--;
                            x += directionModifier;
                            break;
                        case Direction.South:
                            y++;
                            x += directionModifier;
                            break;
                        case Direction.West:
                            x--;
                            if (mapElement == RoadElement && x == mapQuarter + 1 || x == mapQuarter || x == mapQuarter - 1)
                            {
                                directionModifier = 0;
                            }
                            else
                            {
                                y += directionModifier;
                            }
                            break;
                        case Direction.East:
                            x++;
                            y += directionModifier;
                            break;
                    }
                    if (direction == Direction.West && mapElement == RoadElement && x == mapQuarter)
                    {
                        int northernGuardtower = y - 1;
                        int southernGuardtower = y + 1;
                        SetGridCharAndColor('\u25A0', ConsoleColor.White, x, northernGuardtower);
                        SetGridCharAndColor('\u25A0', ConsoleColor.White, x, southernGuardtower);
                        Snake(x, southernGuardtower, Direction.South, WallElement);
                        Snake(x, northernGuardtower, Direction.North, WallElement);
                    }
                    if (direction == Direction.West && mapElement == RoadElement && x == mapQuarter * 2)
                    {
                        Snake(x, y + 1, Direction.North, HiddenPathElement);
                    }
                    char symbol;
                    if (direction == Direction.South)
                    {
                        symbol = mapElement.Symbols[mapElement.Symbols.Length - 1 - (directionModifier + 1)];
                    }
                    else
                    {
                        symbol = mapElement.Symbols[directionModifier + 1];
                    }

                    int xOffSetEnd = mapElement.Width / 2;
                    int xOffSetStart = -(mapElement.Width - 1) / 2;

                    for (int xOffset = xOffSetStart; xOffset <= xOffSetEnd; xOffset++)
                    {
                        SetGridCharAndColor(symbol, mapElement.Color, x + xOffset, y);
                    }
                    if (direction == Direction.South && mapElement == RiverElement)
                    {
                        SetGridCharAndColor('#', ConsoleColor.Magenta, x - 4, y);
                    }
                    if (mapElement == HiddenPathElement && y == 5)
                    {
                        SetGridCharAndColor('X', ConsoleColor.Red, x, y);
                    }
                    //These are to make sure the snaking stops in time to not disturb the border, or go out of bounds of the grid.
                    if (y == 1 || y == height - 2 || x == 1 || x == width - 2 || (mapElement == HiddenPathElement && y == 5))
                    {
                        break;
                    }
                }
            }
            //Prepare the forest, progressively writing less symbols to make it less dense as it moves along the x axis.
            for (int y = 0; y < height; y++)
            {
                for (int x = 1; x < mapQuarter - 1; x++)
                {
                    int randomForestSymbol = random.Next(0, 10);

                    if ((randomForestSymbol > 1 && x < quarterQuarter) ||
                        (randomForestSymbol > 2 && x < doubleQuarterQuarter && x < quarterQuarter) ||
                        (randomForestSymbol > 3 && x < threeQuarterQuarter && x < doubleQuarterQuarter) ||
                        (randomForestSymbol > 6 && x < mapQuarter && x < threeQuarterQuarter))
                    {
                        int forestSymbolIndex = random.Next(ForestElement.Symbols.Length);
                        grid[x, y] = ForestElement.Symbols[forestSymbolIndex];
                        gridColor[x, y] = ForestElement.Color;
                    }
                }
            }
            //Prepare the border characters:
            SetGridCharAndColor('+', ConsoleColor.Yellow, 0, 0);
            SetGridCharAndColor('+', ConsoleColor.Yellow, 0, height - 1);
            SetGridCharAndColor('+', ConsoleColor.Yellow, width - 1, 0);
            SetGridCharAndColor('+', ConsoleColor.Yellow, width - 1, height - 1);
            for (int x = 1; x < width - 1; x++)
            {
                SetGridCharAndColor('-', ConsoleColor.Yellow, x, 0);
                SetGridCharAndColor('-', ConsoleColor.Yellow, x, height - 1);
            }
            for (int y = 1; y < height - 1; y++)
            {
                SetGridCharAndColor('|', ConsoleColor.Yellow, 0, y);
                SetGridCharAndColor('|', ConsoleColor.Yellow, width - 1, y);
            }
            //Prepare the Title:
            string title = "ADVENTURE MAP";
            int titleX = width / 2 - title.Length / 2;
            for (int i = 0; i < title.Length; i++)
            {
                grid[titleX + i, 1] = title[i];
                gridColor[titleX + i, 1] = ConsoleColor.Red;
            }
            //Bridge:
            for (int i = 0; i < 5; i++)
            {
                SetGridCharAndColor('#', ConsoleColor.Magenta, bridgeStartx + i, bridgeStarty + 1);
                SetGridCharAndColor('=', ConsoleColor.Gray, bridgeStartx + i, bridgeStarty);
                SetGridCharAndColor('=', ConsoleColor.Gray, bridgeStartx + i, bridgeStarty + 2);
            }
            //Sideroad Startpoint:
            SetGridCharAndColor(RoadElement.Symbols[0], RoadElement.Color, sideRoadMarkerx, bridgeStarty + 1);
            SetGridCharAndColor(RoadElement.Symbols[0], RoadElement.Color, sideRoadMarkerx, sideRoadMarkery);

            //Road going Left, snaking randomly
            Snake(markerPointxForRoadGoingLeft, markerPointyForRoadGoingLeft, Direction.West, RoadElement);
            //Road going right, snaking randomly
            Snake(MarkerPointxForRoadGoingRight, MarkerPointyForRoadGoingRight, Direction.East, RoadElement);
            //River flowing down, snaking randomly
            Snake(markerxForRiverFlowingDown, markeryForRiverFlowingDown - 1, Direction.South, RiverElement);
            //River flowing up, snaking randomly
            Snake(markerxForRiverFlowingUp, markeryForRiverFlowingUp + 1, Direction.North, RiverElement);

            //Drawing the map to console with all of the preperation from earlier, going line by line and layer by layer
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (grid[x, y] == '\0')
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.ForegroundColor = gridColor[x, y];
                        Console.Write(grid[x, y]);
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
            Map(75, 25);
        }
    }
}
