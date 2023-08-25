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
        static Random random = new Random();
        static void Map(int width, int height)
        {
            //Declare grid data structure
            char[,] grid = new char[width, height];
            ConsoleColor[,] gridColor = new ConsoleColor[width, height];

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
                    else if (randomForestSymbol > 2 && x < doubleQuarterQuarter && x< quarterQuarter)
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
            
            grid[0, 0] = '+';
            gridColor[0, 0] = ConsoleColor.Yellow;
            grid[0, height - 1] = '+';
            gridColor[0, height - 1] = ConsoleColor.Yellow;
            grid[width - 1, 0] = '+';
            gridColor[width - 1, 0] = ConsoleColor.Yellow;
            grid[width - 1, height - 1] = '+';
            gridColor[width - 1, height - 1] = ConsoleColor.Yellow;

            for (int x = 1; x < width - 1; x++)
            {
                grid[x, 0] = '-';
                grid[x, height - 1] = '-';
                gridColor[x, 0] = ConsoleColor.Yellow;
                gridColor[x, height - 1] = ConsoleColor.Yellow;
            }
            for (int y = 1; y < height - 1; y++)
            {
                grid[0, y] = '|';
                grid[width - 1, y] = '|';
                gridColor[0, y] = ConsoleColor.Yellow;
                gridColor[width - 1, y] = ConsoleColor.Yellow;
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
           
            grid[bridgeStartx, bridgeStarty + 1] = '#';
            gridColor[bridgeStartx, bridgeStarty + 1] = ConsoleColor.Magenta;
            grid[bridgeStartx + 1, bridgeStarty + 1] = '#';
            gridColor[bridgeStartx + 1, bridgeStarty + 1] = ConsoleColor.Magenta;
            grid[bridgeStartx + 2, bridgeStarty + 1] = '#';
            gridColor[bridgeStartx + 2, bridgeStarty + 1] = ConsoleColor.Magenta;
            grid[bridgeStartx + 3, bridgeStarty + 1] = '#';
            gridColor[bridgeStartx + 3, bridgeStarty + 1] = ConsoleColor.Magenta;
            grid[bridgeStartx + 4, bridgeStarty + 1] = '#';
            gridColor[bridgeStartx + 4, bridgeStarty + 1] = ConsoleColor.Magenta;
            grid[sideRoadMarkerx, sideRoadMarkery] = '#';
            gridColor[sideRoadMarkerx, sideRoadMarkery] = ConsoleColor.Magenta;

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
            while (markerPointx > 0)
            {
                grid[markerPointx, markerPointy] = '#';
                gridColor[markerPointx, markerPointy] = ConsoleColor.Magenta;
                markerPointy += random.Next(0, 3) - 1;
                markerPointx--;
                if (markerPointy == height - 1)
                    break;
                if (markerPointy == 1)
                    break;
            }
            //Road going right
            while (secondMarkerPointxForRoadGoingRight < width - 1)
            {
                grid[secondMarkerPointxForRoadGoingRight, secondMarkerPointyForRoadGoingRight] = '#';
                gridColor[secondMarkerPointxForRoadGoingRight, secondMarkerPointyForRoadGoingRight] = ConsoleColor.Magenta;
                secondMarkerPointyForRoadGoingRight += random.Next(0, 3) - 1;
                secondMarkerPointxForRoadGoingRight++;
                if (markerPointy == height - 1)
                    break;
                if (markerPointy == 1)
                    break;
            }
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
            //River flowing up
            while (secondRiverMarkery > 1)
            {
                int directionModifier2 = random.Next(0, 3) - 1;
                if (directionModifier2 > 0)
                {
                    grid[secondRiverMarkerx, secondRiverMarkery] = '/';
                    gridColor[secondRiverMarkerx, secondRiverMarkery] = ConsoleColor.Blue;
                    grid[secondRiverMarkerx - 1, secondRiverMarkery] = '/';
                    gridColor[secondRiverMarkerx - 1, secondRiverMarkery] = ConsoleColor.Blue;
                    grid[secondRiverMarkerx + 1, secondRiverMarkery] = '/';
                    gridColor[secondRiverMarkerx + 1, secondRiverMarkery] = ConsoleColor.Blue;
                }
                else if (directionModifier2 < 0)
                {
                    grid[secondRiverMarkerx, secondRiverMarkery] = '\\';
                    gridColor[secondRiverMarkerx, secondRiverMarkery] = ConsoleColor.Blue;
                    grid[secondRiverMarkerx - 1, secondRiverMarkery] = '\\';
                    gridColor[secondRiverMarkerx - 1, secondRiverMarkery] = ConsoleColor.Blue;
                    grid[secondRiverMarkerx + 1, secondRiverMarkery] = '\\';
                    gridColor[secondRiverMarkerx + 1, secondRiverMarkery] = ConsoleColor.Blue;
                }
                else
                {
                    grid[secondRiverMarkerx, secondRiverMarkery] = '|';
                    gridColor[secondRiverMarkerx, secondRiverMarkery] = ConsoleColor.Blue;
                    grid[secondRiverMarkerx - 1, secondRiverMarkery] = '|';
                    gridColor[secondRiverMarkerx - 1, secondRiverMarkery] = ConsoleColor.Blue;
                    grid[secondRiverMarkerx + 1, secondRiverMarkery] = '|';
                    gridColor[secondRiverMarkerx + 1, secondRiverMarkery] = ConsoleColor.Blue;
                }
                secondRiverMarkerx = secondRiverMarkerx + directionModifier2;
                secondRiverMarkery--;
            }
                
            //Drawing the map to console with all of the preperation from earlier, going line by line and layer by layer
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (grid[x, y] == '\0')
                    {

                        Console.Write(" ");
                    }

                    Console.ForegroundColor = gridColor[x, y];
                    Console.Write(grid[x, y]);
                    

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
            Map(40, 25);
        }
    }
}
