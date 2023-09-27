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
        enum Direction
        {
            North,
            South,
            West,
            East
        }
        //Creating a class for the different Elements on the map:
        public class MapElement
        {
            public char[] Symbols;
            public ConsoleColor Color;
            public int Width;
            public int CurveChance;
            public bool StraightAhead;
            public int StraightAheadxStart;
            public int StraightAheadxEnd;
            //MapElement Constructor overloading to make sure there's flexibility to use it for when a snake needs a straight part (Note the road going left)
            public MapElement(char[] symbols, ConsoleColor color, int width, int curveChance)
            {
                Symbols = symbols;
                Color = color;
                Width = width;
                CurveChance = curveChance;
                StraightAhead = false;
            }
            public MapElement(char[] symbols, ConsoleColor color, int width, int curveChance, bool straightAhead, int xStart, int xEnd)
            {
                Symbols = symbols;
                Color = color;
                Width = width;
                CurveChance = curveChance;
                StraightAhead = straightAhead;
                StraightAheadxStart = xStart;
                StraightAheadxEnd = xEnd;
            }
        }
        //The different elements, with their respective symols and colors etc..
        static MapElement WallElement = new(new char[] { '\\', '|', '/', '-' }, ConsoleColor.Gray, 2, 10);
        static MapElement HiddenPathElement = new(new char[] { 'o', 'o', 'o', 'o' }, ConsoleColor.DarkGreen, 1, 3);
        static MapElement RoadElement = new(new char[] { '#', '#', '#', '#' }, ConsoleColor.Magenta, 1, 6);
        static MapElement RiverElement = new(new char[] { '\\', '|', '/', '-' }, ConsoleColor.Blue, 3, 3);
        static MapElement ForestElement = new(new char[] { 'T', '@', '(', ')', '!', '%', '*' }, ConsoleColor.Green, 1, 1);

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

        //Find character location deals with identifying the y coordinate along a snaking element, for hidden path and wall.
        static int FindCharacterYLocation(char characterToFind, char[,] grid, int x, int height)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == characterToFind)
                {
                    return y;
                }
            }
            // Character not found, return a default value
            return -1;
        }

        //The snake method, for creating a list of coordinates for the various elements snaking randomly in a direction:
        static List<Point> Snake(int x, int y, Direction direction, MapElement mapElement)
        {
            Random random = new Random();
            List<Point> coordinates = new List<Point>();
            while (true)
            {
                //directionModifier deals with adjusting the course of the road, river or wall etc..
                int directionModifier = random.Next(mapElement.CurveChance) - 1;
                if (directionModifier > 1)
                {
                    directionModifier = 0;
                }
                //Makes sure the direction modifier stays on-course (0) while the snake needs to go straight.
                if (mapElement.StraightAhead == true)
                {
                    if (x >= mapElement.StraightAheadxStart && x <= mapElement.StraightAheadxEnd)
                    {
                        directionModifier = 0;
                    }
                }
                //The direction, increasing or decreasing the x or y as needed to move the snaking along
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
                        y += directionModifier;
                        break;
                    case Direction.East:
                        x++;
                        y += directionModifier;
                        break;
                }
                //These are to make sure the snaking stops in time to not disturb the border, or go out of bounds of the grid.
                if (y <= 0 || y >= Height - 1 || x <= 0 || x >= Width - 1)
                {
                    break;
                }
                coordinates.Add(new Point(x, y));
            }
            return coordinates;
        }

        //As it says on the tin, the second part of the snake method, it deals with the various elements, symbols and width:
        static void DisplaySnakeOnGrid(List<Point> snakeCoordinates, MapElement mapElement)
        {
            
            for (int coordinateIndex = 0; coordinateIndex < snakeCoordinates.Count; coordinateIndex++)
            {
                // Determine the coordinate
                Point coordinate = snakeCoordinates[coordinateIndex];

                // Determine the symbol
                int symbolIndex;
                if (coordinateIndex == snakeCoordinates.Count - 1)
                {
                    symbolIndex = 1;
                }
                else {
                    Point nextCoordinate = snakeCoordinates[coordinateIndex + 1];

                    int xNextStep = nextCoordinate.X;
                    int yNextStep = nextCoordinate.Y;

                    if (coordinate.X == xNextStep)
                    {
                        symbolIndex = 1;
                    }
                    else if (coordinate.Y == yNextStep)
                    {
                        symbolIndex = 3;
                    }
                    else if (yNextStep == coordinate.Y - 1 && xNextStep == coordinate.X - 1 || yNextStep == coordinate.Y + 1 && xNextStep == coordinate.X + 1)
                    {
                        symbolIndex = 0;
                    }
                    else
                    {
                        symbolIndex = 2;
                    }
                }
                //Draw the symbol at desired width
                int xOffSetEnd = mapElement.Width / 2;
                int xOffSetStart = -(mapElement.Width - 1) / 2;
                for (int xOffset = xOffSetStart; xOffset <= xOffSetEnd; xOffset++)
                { 
                    SetGridCharAndColor(mapElement.Symbols[symbolIndex], mapElement.Color, coordinate.X + xOffset, coordinate.Y);
                }
            }
        }

        //The Map method. Takes width and heigth to create a 2 dimensional grid. Static because it is intended to be a standalone method that does not operate on an instance of a class.
        static void Map(int width, int height)
        {
            Random random = new Random();
            Grid = new char[width, height];
            GridColor = new ConsoleColor[width, height];
            Width = width;
            Height = height;
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

            //This is where all the coordinater go for the road going straight somewhere during the snake method:
            RoadElement.StraightAhead = true;
            RoadElement.StraightAheadxStart = mapQuarter - 1;
            RoadElement.StraightAheadxEnd = mapQuarter + 1;

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
                        Grid[x, y] = ForestElement.Symbols[forestSymbolIndex];
                        GridColor[x, y] = ForestElement.Color;
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
                Grid[titleX + i, 1] = title[i];
                GridColor[titleX + i, 1] = ConsoleColor.Red;
            }

            //Bridge:
            for (int i = 0; i < 5; i++)
            {
                SetGridCharAndColor('#', ConsoleColor.Magenta, bridgeStartx + i, bridgeStarty + 1);
                SetGridCharAndColor('=', ConsoleColor.Gray, bridgeStartx + i, bridgeStarty);
                SetGridCharAndColor('=', ConsoleColor.Gray, bridgeStartx + i, bridgeStarty + 2);
            }

            //Sideroad Startpoint, since there's a step from the road missing until the river starts:
            SetGridCharAndColor(RoadElement.Symbols[0], RoadElement.Color, sideRoadMarkerx, sideRoadMarkery);

            // Now we call the Snake method for each map element and store the coordinates:
            //Road going Left, snaking randomly into a singular direction.  
            var roadCoordinates = Snake(markerPointxForRoadGoingLeft, markerPointyForRoadGoingLeft, Direction.West, RoadElement);
            DisplaySnakeOnGrid(roadCoordinates, RoadElement);
            //Finding the y coordinate to determine where the road is from which the towers start.
            int yCoordinate = FindCharacterYLocation('#', Grid, mapQuarter, height);
            if (yCoordinate != -1)
            //Generating the guardtowers and snaking the wall north and south.
            {
                int northernGuardtower = yCoordinate - 1;
                int southernGuardtower = yCoordinate + 1;
                SetGridCharAndColor('\u25A0', ConsoleColor.White, mapQuarter, northernGuardtower);
                SetGridCharAndColor('\u25A0', ConsoleColor.White, mapQuarter, southernGuardtower);
                var wallNorthCoordinates = Snake(mapQuarter, northernGuardtower, Direction.North, WallElement);
                DisplaySnakeOnGrid(wallNorthCoordinates, WallElement);
                var wallSouthCoordinates = Snake(mapQuarter, southernGuardtower, Direction.South, WallElement);
                DisplaySnakeOnGrid(wallSouthCoordinates, WallElement);
            }

            //Finding the y coordinate for Hiddenpath and subsequently generating it.
            int yCoordinateHiddenPath = FindCharacterYLocation('#', Grid, mapQuarter * 2, height);
            if (yCoordinateHiddenPath != -1)
            {
                // Generate hidden road all the way to the border
                List<Point> hiddenPathCoordinates = Snake(mapQuarter * 2, yCoordinateHiddenPath, Direction.North, HiddenPathElement);
                // delete the last five coordinates out of list
                hiddenPathCoordinates = hiddenPathCoordinates.Take(hiddenPathCoordinates.Count-4).ToList();
                // Tell artists to draw hideen road.
                DisplaySnakeOnGrid(hiddenPathCoordinates, HiddenPathElement);
                // X marks the spot
                Point lastHiddenPathCoordinate = hiddenPathCoordinates.Last();
                SetGridCharAndColor('X', ConsoleColor.Red, lastHiddenPathCoordinate.X, lastHiddenPathCoordinate.Y-1);
            }

            //Road going right, snaking randomly into a singular direction.
            var roadRightCoordinates = Snake(MarkerPointxForRoadGoingRight, MarkerPointyForRoadGoingRight, Direction.East, RoadElement);
            DisplaySnakeOnGrid(roadRightCoordinates, RoadElement);
            //River flowing down, snaking randomly into a singular direction.
            var riverDownCoordinates = Snake(markerxForRiverFlowingDown, markeryForRiverFlowingDown - 1, Direction.South, RiverElement);
            DisplaySnakeOnGrid(riverDownCoordinates, RiverElement);

            //create coordinates for sideroad, following along the river going down:
            List<Point> roadFollowingAlongRiverCoordinates = new List<Point>(riverDownCoordinates);
            for (int coordinateIndex = 0; coordinateIndex < roadFollowingAlongRiverCoordinates.Count; coordinateIndex++)
            {
                Point coordinate = roadFollowingAlongRiverCoordinates[coordinateIndex];
                coordinate.X -= 4;
                roadFollowingAlongRiverCoordinates[coordinateIndex] = coordinate;
            }
            DisplaySnakeOnGrid(roadFollowingAlongRiverCoordinates, RoadElement);

            //River flowing up, snaking randomly into a singular direction.
            var riverUpCoordinates = Snake(markerxForRiverFlowingUp, markeryForRiverFlowingUp + 1, Direction.North, RiverElement);
            DisplaySnakeOnGrid(riverUpCoordinates, RiverElement);

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
            Map(75, 25);
        }
    }
}
