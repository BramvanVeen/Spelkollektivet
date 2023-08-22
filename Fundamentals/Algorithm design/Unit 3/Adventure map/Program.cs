using System;
using System.Runtime.CompilerServices;

namespace Adventure_map
{
    internal class Program
    {
        static Random random = new Random();

        char[] forest = { 'T', '@', '(', ')', '!', '%', '*' };
        char forestSymbol = random.Next(0, forest.Length);


        char[] border = { '-', '+', '|', };
        char borderHorizontal = border[0];
        char borderVertical = border[2];
        char borderCorner = border[1];

        char[] River = { '/', '|', '╲' };
        char[] Road = { '*' };
        char[] Bridge = { '=' };
        char[] Empty = { ' ' };

        //In this order (order doesnt matter maybe for methods)
        /*Method to determine if a bridge symbol should be placed in the grid*/
        /*Method for borders*/

        static void generateBorder(bool[,] border, int width, int height)
        {
            if (height == height / height - 1 && width == width / width - 1)
            {
                Console.WriteLine(border[1]);
            }

            /*switch (borderPlacement)
            {
                case 0: //leftcornerup  
                    {
                        borderCornerLeftsideTop[height/height-1, width/width-1] = true;
                    }
                    break;
                case 1: //leftcornerbottom
                    {
                        borderCornerLeftsideBottom[height, width / width - 1] = true;
                    }
                    break;
                case 2: //rightcornerup ***WRITELINE***
                    {
                        borderCornerRightsideTop[height / height - 1, width] = true;
                    }
                    break;
                case 3: //rightcornerbottom ***WRITELINE***
                    {
                        borderCornerRightsideBottom[height, width] = true;
                    }
                    break;
                case 4: //tophorizontal
                    for (int i = width/width; i < width-1; i++)
                    {
                        borderhorizontalTop[,] = true;
                    }
                    break;
                case 5: //bottomhorizontal
                    for (int i = width/width; i < width-1; i++)
                    {
                        borderhorizontalBottom[startX, up] = true;
                    }
                    break;
                case 6: //leftvertical
                    for (int i = height / height; i < height - 1; i++) 
                    {
                        borderLeftVertical[startX, up] = true;
                    }
                    break;
                case 7: //rightvertical ***WRITELINE***
                    for (int i = height / height; i < height - 1; i++)
                    {
                        borderRightVertical[startX, up] = true;
                    }
                    break;*/
        }
    }
    /*Method for bridge*/
    /*Method for titel*/


    /*Method for road*/
    /*Method for River*/
    /*Method for extra road*/


    /*Method for Forest*/

    static void Map(int width, int height)
    {

        int horizontal = width;
        int vertical = height;

        char[,] grid = new char[horizontal, vertical];

        int mapQuarter = width / 4;
        int bridgeStart = width * 3 / 4;

        // //Drawing the map to console with all of the preperation from earlier, going line by line and layer by layer
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                generateBorder();
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
