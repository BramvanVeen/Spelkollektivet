using System;

namespace Adventure_map
{
    public class Program
    {
        static Random random = new Random();
        static void Map(int height, int width)
        {
            //Declare grid data structure
            char[,] grid = new char[width, height];

            //Prepare the grid
            int mapQuarter = width / 4;

            //Prepare the border
            grid[0, 0] = '+';
            grid[0, height - 1] = '+';
            grid[width - 1, 0] = '+';
            grid[width-1, height-1] = '+';

            for (int x = 1; x < width - 1; x++)
            {
                grid[x, 0] = '-';
            }
            for (int y = 1; y < height - 1; y++)
            {
                grid[0, y] = '|';
            }

            for (int y = 1; y < height - 1; y++)
            {
                grid[width-1, y] = '|';
            }

            for (int x = 1; x < width - 1; x++)
            {
                grid[x, height - 1] = '-';
            }

            //Prepare the Title:
            grid[width / 2 - 6, 1] = 'A';
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

            //Prepare the Bridge:
            int bridgeStartx = width * 3 / 4;
            var bridgeStarty = random.Next(7, height - 7);

            for (int y = 0; y < 5; y++)
            { 
                grid[bridgeStartx, bridgeStarty] = '=';
                bridgeStartx++;
            }
            for (int y = 0; y < 5; y++)
            { 
                grid[bridgeStartx-5, bridgeStarty + 2] = '='; 
                bridgeStartx++;
            }

            /*//Prepare the Road:
            
            How to determine where to jump?
            For: the amount of jumps to be made: bridgeStartx -1 or 2 or something..?

            Determine current location!
            char currentLocation = [bridgeStartx-1, bridgeStarty+1];

            Random jump to the left:

            For loop..

            {grid[currentLocation] = '#';

            random.Next nw w sw.. pull this randomly from an array?
            char[] directionPossibilities = new char[northWest, west, southWest];
            direction = random.Next[directionPossibilities];

            Then determine new current location (because choosing w or sw did something to x and y..
            }


            some sort of compass???
            nw n ne
            w     e
            sw s se

            nw is current location x-1, y-1
            w  is current location x-1, y
            sw is current location x-1, y+1
            n  is current location x, y-1
            ne is current location x+1, y-1
            e  is current location x+1, y
            se is current location x+1, y+1
            s  is current location x, y+1

            sneaky start with road since road is always going over bridge..
            grid[bridgeStartx, bridgeStarty + 1] = '#';
            grid[bridgeStartx + 1, bridgeStarty + 1] = '#';
            grid[bridgeStartx + 2, bridgeStarty + 1] = '#';
            grid[bridgeStartx + 3, bridgeStarty + 1] = '#';
            grid[bridgeStartx + 4, bridgeStarty + 1] = '#';


            int roadGoingLeft = width - bridgeStartx - 1;
            for (int i = 1; i < width-10; i++)
            {
                int leftOrRight = random.Next(bridgeStarty, bridgeStarty + 2);
                grid[bridgeStartx -1, leftOrRight] = '#';
                bridgeStartx--;
            }*/





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
