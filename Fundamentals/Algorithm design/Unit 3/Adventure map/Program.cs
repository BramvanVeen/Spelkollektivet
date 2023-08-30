using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

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

            void SetGridCharAndColor(char symbol, ConsoleColor color, int x, int y)
            {
                grid[x, y] = symbol;
                gridColor[x, y] = color;
            }

            //Wondering if there's a way to put one method into the other for properties
            /*void SnakeLeft(SetGridCharAndColor)
            {

            }*/

            void SnakeLeft(int x, int y, ConsoleColor color, char symbol)
            {
                while (x > 1)
                {
                    y += random.Next(0, 3) - 1;
                    x--;
                    grid[x, y] = symbol;
                    gridColor[x, y] = color;
                    if (y == height - 2)
                        break;
                    if (y == 1)
                        break;
                }
            }

            void SnakeRight(int x, int y, ConsoleColor color, char symbol)
            {
                while (x < width - 1)
                {
                    y += random.Next(0, 3) - 1;
                    x++;
                    grid[x, y] = symbol;
                    gridColor[x, y] = color;
                    if (y == height - 2)
                        break;
                    if (y == 1)
                        break;
                    if (x == width - 2)
                        break;
                }
            }

            //Not working for some reason. 
            /*void SnakeUp(int x, int y, ConsoleColor color, char symbol)
            {
                while (y > 1)
                {
                    int directionModifier2 = random.Next(0, 3) - 1;
                    x = +directionModifier2;
                    y--;
                    grid[x, y] = symbol;
                    gridColor[x, y] = color;
                    if (directionModifier2 > 0)
                    {
                        symbol = '/';
                    }
                    else if (directionModifier2 < 0)
                    {
                        symbol = '\\';
                    }
                    else
                    {
                        symbol = '|';
                    }
                    if (y == 2)
                        break;
                    if (x == width - 2)
                        break;
                    if (x == 2)
                        break;
                }
            }*/

            void SnakeDown(int x, int y, ConsoleColor color, char symbol)
            { 
            
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
                    if (randomForestSymbol > 1 && x < quarterQuarter)
                    {
                        int forestSymbolIndex = random.Next(forestSymbols.Length);
                        grid[x, y] = forestSymbols[forestSymbolIndex];
                        gridColor[x, y] = ConsoleColor.Green;
                    }
                    else if (randomForestSymbol > 2 && x < doubleQuarterQuarter && x < quarterQuarter)
                    {
                        int forestSymbolIndex = random.Next(forestSymbols.Length);
                        grid[x, y] = forestSymbols[forestSymbolIndex];
                        gridColor[x, y] = ConsoleColor.Green;
                    }
                    else if (randomForestSymbol > 3 && x < threeQuarterQuarter && x < doubleQuarterQuarter)
                    {
                        int forestSymbolIndex = random.Next(forestSymbols.Length);
                        grid[x, y] = forestSymbols[forestSymbolIndex];
                        gridColor[x, y] = ConsoleColor.Green;
                    }
                    else if (randomForestSymbol > 6 && x < mapQuarter && x < threeQuarterQuarter)
                    {
                        int forestSymbolIndex = random.Next(forestSymbols.Length);
                        grid[x, y] = forestSymbols[forestSymbolIndex];
                        gridColor[x, y] = ConsoleColor.Green;
                    }
                    continue;
                    /*else (randomForestSymbol > 2)
                    {
                        int forestSymbolIndex = random.Next(forestSymbols.Length);
                        grid[x, y] = forestSymbols[forestSymbolIndex];
                    }*/
                }
            }

            //Prepare the border

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

            grid[width / 2 - 6, 1] = 'A';
            gridColor[width / 2 - 6, 1] = ConsoleColor.Red;
            grid[width / 2 - 5, 1] = 'D';
            gridColor[width / 2 - 5, 1] = ConsoleColor.Red;
            grid[width / 2 - 4, 1] = 'V';
            gridColor[width / 2 - 4, 1] = ConsoleColor.Red;
            grid[width / 2 - 3, 1] = 'E';
            gridColor[width / 2 - 3, 1] = ConsoleColor.Red;
            grid[width / 2 - 2, 1] = 'N';
            gridColor[width / 2 - 2, 1] = ConsoleColor.Red;
            grid[width / 2 - 1, 1] = 'T';
            gridColor[width / 2 - 1, 1] = ConsoleColor.Red;
            grid[width / 2, 1] = 'U';
            gridColor[width / 2, 1] = ConsoleColor.Red;
            grid[width / 2 + 1, 1] = 'R';
            gridColor[width / 2 + 1, 1] = ConsoleColor.Red;
            grid[width / 2 + 2, 1] = 'E';
            gridColor[width / 2 + 2, 1] = ConsoleColor.Red;
            grid[width / 2 + 3, 1] = ' ';
            gridColor[width / 2 + 3, 1] = ConsoleColor.Red;
            grid[width / 2 + 4, 1] = 'M';
            gridColor[width / 2 + 4, 1] = ConsoleColor.Red;
            grid[width / 2 + 5, 1] = 'A';
            gridColor[width / 2 + 5, 1] = ConsoleColor.Red;
            grid[width / 2 + 6, 1] = 'P';
            gridColor[width / 2 + 6, 1] = ConsoleColor.Red;

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
            SnakeLeft(markerPointx, markerPointy, ConsoleColor.Magenta, '#');

            //Road going right
            SnakeRight(secondMarkerPointxForRoadGoingRight, secondMarkerPointyForRoadGoingRight, ConsoleColor.Magenta, '#');


            //River flowing down
            while (rivertMarkery < height - 1)
            {
                int directionModifier = random.Next(0, 3) - 1;
                if (directionModifier < 0)
                {
                    grid[riverMarkerx, rivertMarkery] = '/';
                    gridColor[riverMarkerx, rivertMarkery] = ConsoleColor.Blue;
                    grid[riverMarkerx - 1, rivertMarkery] = '/';
                    gridColor[riverMarkerx - 1, rivertMarkery] = ConsoleColor.Blue;
                    grid[riverMarkerx + 1, rivertMarkery] = '/';
                    gridColor[riverMarkerx + 1, rivertMarkery] = ConsoleColor.Blue;
                    grid[riverMarkerx - 3, rivertMarkery] = '#';
                    gridColor[riverMarkerx - 3, rivertMarkery] = ConsoleColor.Magenta;
                }
                else if (directionModifier > 0)
                {
                    grid[riverMarkerx, rivertMarkery] = '\\';
                    gridColor[riverMarkerx, rivertMarkery] = ConsoleColor.Blue;
                    grid[riverMarkerx - 1, rivertMarkery] = '\\';
                    gridColor[riverMarkerx - 1, rivertMarkery] = ConsoleColor.Blue;
                    grid[riverMarkerx + 1, rivertMarkery] = '\\';
                    gridColor[riverMarkerx + 1, rivertMarkery] = ConsoleColor.Blue;
                    grid[riverMarkerx - 3, rivertMarkery] = '#';
                    gridColor[riverMarkerx - 3, rivertMarkery] = ConsoleColor.Magenta;
                }
                else
                {
                    grid[riverMarkerx, rivertMarkery] = '|';
                    gridColor[riverMarkerx, rivertMarkery] = ConsoleColor.Blue;
                    grid[riverMarkerx - 1, rivertMarkery] = '|';
                    gridColor[riverMarkerx - 1, rivertMarkery] = ConsoleColor.Blue;
                    grid[riverMarkerx + 1, rivertMarkery] = '|';
                    gridColor[riverMarkerx + 1, rivertMarkery] = ConsoleColor.Blue;
                    grid[riverMarkerx - 3, rivertMarkery] = '#';
                    gridColor[riverMarkerx - 3, rivertMarkery] = ConsoleColor.Magenta;
                }
                riverMarkerx = riverMarkerx + directionModifier;
                rivertMarkery++;
                if (riverMarkerx == width - 1)
                    break;
                if (riverMarkerx == 1)
                    break;
            }

            //This one is connected to a method that doesn't work:
            //River flowing up
            /*SnakeUp(secondRiverMarkerx, secondRiverMarkery, ConsoleColor.Blue, '|');*/

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
            Map(75, 20);
        }
    }
}
