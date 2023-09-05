using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Drawing;
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
            int bridgeStarty = random.Next(7, height - 7);

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

            int wallMarkerx = mapQuarter + 1;
            int wallMarkery = 2;

            //This method takes care of randomly snaking the various elements into a singular direction.
            void Snake(int x, int y, string direction, string type, string curveChance)
            {
                int directionModifier = 0;
                while (true)
                {
                    //The curvechance is divided into three different options as required for wall, river and road, to determine how likely they are to change course.
                    if (curveChance == "riverlike")
                    { directionModifier = random.Next(0, 3) - 1; } //Heavy curvechance (Simulating a natural course)
                    else if (curveChance == "roadlike")
                    {
                        directionModifier = random.Next(0, 6) - 3; //Medium curvechance (Simulating a semi-natural/semi-manmade course)
                        if (directionModifier > 1 || directionModifier < -1)
                        { directionModifier = 0; }
                    }
                    else //"wall-like"
                    {
                        directionModifier = random.Next(0, 10) - 3;  //Low curvechance (Simulating a man-made, planned out course)
                        if (directionModifier > 1 || directionModifier < -1)
                        { directionModifier = 0; }
                    }
                    //These are the directions and the directionmodifiers:
                    if ((direction == "left" || direction == "right") && (x > 1 && x < width - 2))
                    {
                        if (x < 30 && x > 10) //Making sure the road goes straight where it crosses the wall (for esthetic purposes)
                        {
                            directionModifier = 0;
                        }
                        y += directionModifier;
                        if (direction == "left")
                        {
                            x--;
                        }
                        else if (direction == "right")
                        {
                            x++;
                        }
                    }
                    else if ((direction == "up" || direction == "down") && (y > 1 && y < height - 2))
                    {
                        x += directionModifier;
                        if (direction == "up")
                        {
                            y--;
                        }
                        else if (direction == "down")
                        {
                            y++;
                        }
                    }
                    else
                    {
                        break;
                    }
                    //These are for identifying what type is being represented and connected to the right symbol and color for the map.
                    if (type == "road")
                    {
                        SetGridCharAndColor('#', ConsoleColor.Magenta, x, y);
                    }
                    else if (type == "river" && direction == "up")
                    {
                        if (directionModifier > 0)
                        {
                            SetGridCharAndColor('/', ConsoleColor.Blue, x - 1, y);
                            SetGridCharAndColor('/', ConsoleColor.Blue, x, y);
                            SetGridCharAndColor('/', ConsoleColor.Blue, x + 1, y);
                        }
                        else if (directionModifier < 0)
                        {
                            SetGridCharAndColor('\\', ConsoleColor.Blue, x - 1, y);
                            SetGridCharAndColor('\\', ConsoleColor.Blue, x, y);
                            SetGridCharAndColor('\\', ConsoleColor.Blue, x + 1, y);
                        }
                        else
                        {
                            SetGridCharAndColor('|', ConsoleColor.Blue, x - 1, y);
                            SetGridCharAndColor('|', ConsoleColor.Blue, x, y);
                            SetGridCharAndColor('|', ConsoleColor.Blue, x + 1, y);
                        }
                    }
                    else if (type == "river" && direction == "down")
                    {
                        if (directionModifier < 0)
                        {
                            SetGridCharAndColor('/', ConsoleColor.Blue, x - 1, y);
                            SetGridCharAndColor('/', ConsoleColor.Blue, x, y);
                            SetGridCharAndColor('/', ConsoleColor.Blue, x + 1, y);
                            grid[x - 3, y] = '#'; //Detailing the sideroad that always follows the river flowing down
                            gridColor[x - 3, y] = ConsoleColor.Magenta;
                        }
                        else if (directionModifier > 0)
                        {
                            SetGridCharAndColor('\\', ConsoleColor.Blue, x - 1, y);
                            SetGridCharAndColor('\\', ConsoleColor.Blue, x, y);
                            SetGridCharAndColor('\\', ConsoleColor.Blue, x + 1, y);
                            grid[x - 3, y] = '#';
                            gridColor[x - 3, y] = ConsoleColor.Magenta;
                        }
                        else
                        {
                            SetGridCharAndColor('|', ConsoleColor.Blue, x - 1, y);
                            SetGridCharAndColor('|', ConsoleColor.Blue, x, y);
                            SetGridCharAndColor('|', ConsoleColor.Blue, x + 1, y);
                            grid[x - 3, y] = '#';
                            gridColor[x - 3, y] = ConsoleColor.Magenta;
                        }
                    }
                    else if (type == "wall" && direction == "down")
                    {
                        if (directionModifier < 0)
                        {
                            SetGridCharAndColor('/', ConsoleColor.Gray, x, y);
                            SetGridCharAndColor('/', ConsoleColor.Gray, x + 1, y);
                        }
                        else if (directionModifier > 0)
                        {
                            SetGridCharAndColor('\\', ConsoleColor.Gray, x, y);
                            SetGridCharAndColor('\\', ConsoleColor.Gray, x + 1, y);
                        }
                        else
                        {
                            SetGridCharAndColor('|', ConsoleColor.Gray, x, y);
                            SetGridCharAndColor('|', ConsoleColor.Gray, x + 1, y);
                        }
                        //Recognise if the wall will cross the road:
                        if (grid[x, y + 1] == '#')
                        {
                            // Place a square character if the next position is a road
                            SetGridCharAndColor('■', ConsoleColor.Gray, x - 1, y);
                            SetGridCharAndColor('■', ConsoleColor.Gray, x - 1, y + 2);
                            SetGridCharAndColor('■', ConsoleColor.Gray, x, y);
                            SetGridCharAndColor('■', ConsoleColor.Gray, x, y + 2);
                            SetGridCharAndColor('■', ConsoleColor.Gray, x + 1, y);
                            SetGridCharAndColor('■', ConsoleColor.Gray, x + 1, y + 2);
                            y += 2;
                        }
                    }
                    //These exist to make sure the snaking stops in time to not disturb the border, or go out of bounds of the grid.
                    if ((direction == "up" || direction == "down" || direction == "left" || direction == "right") &&
                   ((y == 1 || y == height - 2) || (x == 1 || x == width - 2)))
                    {
                        break;
                    }
                }
            }
            //Prepare the forest, progressively writing less symbols to make it less dense as it moves along the x axis.
            char[] forestSymbols = { 'T', '@', '(', ')', '!', '%', '*' };
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
                        int forestSymbolIndex = random.Next(forestSymbols.Length);
                        grid[x, y] = forestSymbols[forestSymbolIndex];
                        gridColor[x, y] = ConsoleColor.Green;
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
            //Road on bridge, a fixed constant:
            for (int i = 0; i < 5; i++)
            {
                SetGridCharAndColor('#', ConsoleColor.Magenta, bridgeStartx + i, bridgeStarty + 1);
            }
            //Sideroad Startpoint, also a fixed constant:
            SetGridCharAndColor('#', ConsoleColor.Magenta, sideRoadMarkerx, bridgeStarty + 1);
            grid[sideRoadMarkerx, sideRoadMarkery] = '#';
            gridColor[sideRoadMarkerx, sideRoadMarkery] = ConsoleColor.Magenta;
            //Bridge
            for (int y = 0; y < 5; y++)
            {
                grid[bridgeStartx, bridgeStarty] = '=';
                gridColor[bridgeStartx, bridgeStarty] = ConsoleColor.Gray;
                bridgeStartx++;
            }
            for (int y = 0; y < 5; y++)
            {
                grid[bridgeStartx - 5, bridgeStarty + 2] = '=';
                gridColor[bridgeStartx - 5, bridgeStarty + 2] = ConsoleColor.Gray;
                bridgeStartx++;
            }
            //Road going Left, snaking randomly
            Snake(markerPointxForRoadGoingLeft, markerPointyForRoadGoingLeft, "left", "road", "roadlike");
            //Road going right, snaking randomly
            Snake(MarkerPointxForRoadGoingRight, MarkerPointyForRoadGoingRight, "right", "road", "roadlike");
            //River flowing down, snaking randomly
            Snake(markerxForRiverFlowingDown, markeryForRiverFlowingDown - 1, "down", "river", "riverlike");
            //River flowing up, snaking randomly
            Snake(markerxForRiverFlowingUp, markeryForRiverFlowingUp + 1, "up", "river", "riverlike");
            //Wall going down, snaking randomly
            Snake(wallMarkerx, wallMarkery, "down", "wall", "walllike");

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
