using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Adventure_map
{
    public class Program
    {
        static Random random = new Random();
        static void Map(int height, int width)
        {
            //Declare grid data structure
            char[,] grid = new char[width, height];

            //Prepare the forest
            char[] forestSymbols = { 'T', '@', '(', ')', '!', '%', '*', ' ', ' ', ' ' };
            int mapQuarter = width / 4;
            for (int y = 0; y < height; y++)
            {
                for (int x = 1; x < mapQuarter - 1; x++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    int forestSymbolIndex = random.Next(forestSymbols.Length);
                    grid[x, y] = forestSymbols[forestSymbolIndex];

                    string forestSymbolString = new string(forestSymbols);
                    forestSymbolString.Remove(0);
                    char[] forestSymbols2 = new char(forestSymbolString);

                }
            }

            //Prepare the border
            grid[0, 0] = '+';
            grid[0, height - 1] = '+';
            grid[width - 1, 0] = '+';
            grid[width - 1, height - 1] = '+';

            for (int x = 1; x < width - 1; x++)
            {
                grid[x, 0] = '-';
                grid[x, height - 1] = '-';
            }
            for (int y = 1; y < height - 1; y++)
            {
                grid[0, y] = '|';
                grid[width - 1, y] = '|';
            }

            //Prepare the Title:
            Console.ForegroundColor = ConsoleColor.Yellow;
            grid[width / 2 - 6, 1] =  'A';
            grid[width / 2 - 5, 1] = 'D';
            grid[width / 2 - 4, 1] = 'V';
            grid[width / 2 - 3, 1] = 'E';
            grid[width / 2 - 2, 1] = 'N';
            grid[width / 2 - 1, 1] = 'T';
            grid[width / 2, 1] = 'U';
            grid[width / 2 + 1, 1] = 'R';
            grid[width / 2 + 2, 1] = 'E';
            grid[width / 2 + 3, 1] = ' ';
            grid[width / 2 + 4, 1] = 'M';
            grid[width / 2 + 5, 1] = 'A';
            grid[width / 2 + 6, 1] = 'P';

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
            Console.ForegroundColor = ConsoleColor.Red;
            grid[bridgeStartx, bridgeStarty + 1] = '#';
            grid[bridgeStartx + 1, bridgeStarty + 1] = '#';
            grid[bridgeStartx + 2, bridgeStarty + 1] = '#';
            grid[bridgeStartx + 3, bridgeStarty + 1] = '#';
            grid[bridgeStartx + 4, bridgeStarty + 1] = '#';
            grid[sideRoadMarkerx, sideRoadMarkery] = '#';

            for (int y = 0; y < 5; y++)
            {
                grid[bridgeStartx, bridgeStarty] = '=';
                bridgeStartx++;
            }
            for (int y = 0; y < 5; y++)
            {
                grid[bridgeStartx - 5, bridgeStarty + 2] = '=';
                bridgeStartx++;
            }

            //Road going Left
            while (markerPointx > 0)
            {
                grid[markerPointx, markerPointy] = '#';
                markerPointy += random.Next(0, 3) - 1;
                markerPointx--;
            }
            //Road going right
            while (secondMarkerPointxForRoadGoingRight < width - 1)
            {
                grid[secondMarkerPointxForRoadGoingRight, secondMarkerPointyForRoadGoingRight] = '#';
                secondMarkerPointyForRoadGoingRight += random.Next(0, 3) - 1;
                secondMarkerPointxForRoadGoingRight++;
            }
            //River flowing down
            while (rivertMarkery < height - 1)
            {
                int directionModifier = random.Next(0, 3) - 1;
                if (directionModifier < 0)
                {
                    grid[riverMarkerx, rivertMarkery] = '/';
                    grid[riverMarkerx - 1, rivertMarkery] = '/';
                    grid[riverMarkerx + 1, rivertMarkery] = '/';
                    grid[riverMarkerx - 3, rivertMarkery] = '#';
                }
                else if (directionModifier > 0)
                {
                    grid[riverMarkerx, rivertMarkery] = '\\';
                    grid[riverMarkerx - 1, rivertMarkery] = '\\';
                    grid[riverMarkerx + 1, rivertMarkery] = '\\';
                    grid[riverMarkerx - 3, rivertMarkery] = '#';
                }
                else
                {
                    grid[riverMarkerx, rivertMarkery] = '|';
                    grid[riverMarkerx - 1, rivertMarkery] = '|';
                    grid[riverMarkerx + 1, rivertMarkery] = '|';
                    grid[riverMarkerx - 3, rivertMarkery] = '#';
                }

                riverMarkerx = riverMarkerx + directionModifier;
                rivertMarkery++;
            }
            //River flowing up
            while (secondRiverMarkery > 1)
            {
                int directionModifier2 = random.Next(0, 3) - 1;
                if (directionModifier2 > 0)
                {
                    grid[secondRiverMarkerx, secondRiverMarkery] = '/';
                    grid[secondRiverMarkerx - 1, secondRiverMarkery] = '/';
                    grid[secondRiverMarkerx + 1, secondRiverMarkery] = '/';
                }
                else if (directionModifier2 < 0)
                {
                    grid[secondRiverMarkerx, secondRiverMarkery] = '\\';
                    grid[secondRiverMarkerx - 1, secondRiverMarkery] = '\\';
                    grid[secondRiverMarkerx + 1, secondRiverMarkery] = '\\';
                }
                else
                {
                    grid[secondRiverMarkerx, secondRiverMarkery] = '|';
                    grid[secondRiverMarkerx - 1, secondRiverMarkery] = '|';
                    grid[secondRiverMarkerx + 1, secondRiverMarkery] = '|';
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
            Map(25, 40);
        }
    }
}
