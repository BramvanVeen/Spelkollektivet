using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading.Tasks.Dataflow;

namespace Adventure_map
{

    public class Program
    {

        static void Map(int width, int height)
        {
            Random random = new Random();

            //Declare grid data structure
            char[,] grid = new char[width, height];
            ConsoleColor[,] gridColor = new ConsoleColor[width, height];

            void SetGridCharAndColor(char symbol, ConsoleColor color, int x, int y)
            {
                grid[x, y] = symbol;
                gridColor[x, y] = color;
            }

            //Prepare the Bridge and subsequent markerpoints:
            int bridgeStartx = width * 3 / 4;
            int markerPointx = bridgeStartx - 1;
            int bridgeStarty = random.Next(7, height - 7);
            int markerPointy = bridgeStarty + 1;
            int secondMarkerPointxForRoadGoingRight = markerPointx + 5;
            int secondMarkerPointyForRoadGoingRight = markerPointy;
            int riverMarkerx = bridgeStartx + 2;
            int rivertMarkery = bridgeStarty + 3;
            int secondRiverMarkerx = bridgeStartx + 2;
            int secondRiverMarkery = bridgeStarty - 1;
            int sideRoadMarkerx = markerPointx;
            int sideRoadMarkery = markerPointy + 1;



            void Snake(int x, int y, ConsoleColor color, char symbol, string direction, string type)
            {
                {
                    grid[x, y] = symbol;
                    gridColor[x, y] = color;
                }
                while (true)
                {

                    if ((direction == "left" || direction == "right") && (x > 1 && x < width - 2))
                    {
                        y += random.Next(0, 3) - 1;
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
                        int directionModifier = random.Next(0, 3) - 1;
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

                    if (type == "road")
                    {
                        SetGridCharAndColor('#', ConsoleColor.Magenta, x, y);

                    }
                    else if (type == "river")
                    {
                        SetGridCharAndColor('|', ConsoleColor.Blue, x, y);

                        if (directionModifier > 0)
                        {
                            grid[x - 1, y] = '/';
                            gridColor[x - 1, y] = color;
                            grid[x + 1, y] = '/';
                            gridColor[x + 1, y] = color;
                        }
                        else if (directionModifier < 0)
                        {
                            grid[x - 1, y] = '\\';
                            gridColor[x - 1, y] = color;
                            grid[x + 1, y] = '\\';
                            gridColor[x + 1, y] = color;
                        }
                        else
                        {
                            grid[x - 1, y] = '|';
                            gridColor[x - 1, y] = color;
                            grid[x + 1, y] = '|';
                            gridColor[x + 1, y] = color;
                        }
                    }
                    else if (type == "wall")
                    {
                        SetGridCharAndColor('|', ConsoleColor.Gray, x, y);
                    }

                    if ((direction == "up" || direction == "down" || direction == "left" || direction == "right") &&
                       ((y == 1 || y == height - 2) || (x == 1 || x == width - 2)))
                    {
                        break;
                    }
                }
            }

            //Prepare the forest
            char[] forestSymbols = { 'T', '@', '(', ')', '!', '%', '*' };
            int mapQuarter = width / 4;
            int quarterQuarter = mapQuarter / 4;
            int doubleQuarterQuarter = quarterQuarter * 2;
            int threeQuarterQuarter = quarterQuarter * 3;

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

            //Prepare the border:
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

            //Road on bridge
            for (int i = 0; i < 5; i++)
            {
                SetGridCharAndColor('#', ConsoleColor.Magenta, bridgeStartx + i, bridgeStarty + 1);
            }

            //Sideroad Startpoint
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

            //Road going Left
            Snake(markerPointx, markerPointy, ConsoleColor.Magenta, '#', "left", "road");
            //Road going right
            Snake(secondMarkerPointxForRoadGoingRight, secondMarkerPointyForRoadGoingRight, ConsoleColor.Magenta, '#', "right", "road");
            //River flowing down
            Snake(riverMarkerx, rivertMarkery - 1, ConsoleColor.Blue, '|', "down", "river");
            //River flowing up
            Snake(secondRiverMarkerx, secondRiverMarkery + 1, ConsoleColor.Blue, '|', "up", "river");

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
