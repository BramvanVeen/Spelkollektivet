using System;
using System.Runtime.CompilerServices;

namespace Adventure_map
{
    internal class Program
    {
        static Random random = new Random();

        string[] Forest = { "T", "@", "(", ")", "!", "%", "*" };
        string[] River = { "/", "|", "╲" };
        string[] Road = { "*" };
        string[] Bridge = { "=" };
        string[] Empty = { " " };

        //In this order (order doesnt matter maybe for methods)
        /*Method to determine if a bridge symbol should be placed in the grid*/
        /*Method for borders*/
        /*Method for road*/
        /*Method for River*/
        /*Method for extra road*/
        /*Method for Forest*/

        static void Map(int width, int height)
        {
            int horizontal = width;
            int vertical = height;
            int[,] grid = new int[horizontal, vertical];

            // //Drawing the map to console with all of the preperation from earlier, going line by line and layer by layer
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    /*switch (location on map)
                    {
                        case 0: River
                                code block (Maybe it's own method?) 
                                (Then if river is true for that position, place the correct symbol)
                                break;
                        case 1: Forest
                                code block (If Forest is true for that position, place the correct symbol)
                                break;    
                        case 3: Road 
                                code block (If Road is true for that position, place the correct symbol)
                                break;
                        case 4: Bridge
                                code block (If Bridge is true for that position, place the correct symbol)
                                break;
                        case 5: Border
                                code block (If Border is true for that position, place the correct symbol)
                                break;
                        default: Empty
                            code block(If Empty is true for that position, consolewriteline" ";)
                            break;*/
    }



}
                Console.WriteLine();
            }

        }
        static void Main(string[] args)
        {
            //Calling the map method with sizes, first for width, second for height. 
            Map(20, 10);
        }

    }
}
