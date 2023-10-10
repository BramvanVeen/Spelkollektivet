using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static Game;
public class Game
{
    private char[,] map;
    private Player player;
    private Opponent opponent;
    private Dictionary<char, ConsoleColor> symbolColors = new Dictionary<char, ConsoleColor>
    {
        { '☺', ConsoleColor.Yellow }, // Player symbol color
        { '☻', ConsoleColor.Red },    // Opponent symbol color
        { '♥', ConsoleColor.Green },  // Health symbol color
        { '■', ConsoleColor.Blue },   // Gasoline symbol color
        { '0', ConsoleColor.DarkGray },   // Audience color
        { '═', ConsoleColor.White },  // Border color
        { '║', ConsoleColor.White },  // Border color
        { '╗', ConsoleColor.White },  // Border color
        { '╔', ConsoleColor.White },  // Border color
        { '╝', ConsoleColor.White },  // Border color
        { '╚', ConsoleColor.White },   // Border color
        { '/', ConsoleColor.DarkGray }, // Audience color
        { '\\', ConsoleColor.DarkGray }  // Audience color
    };
    private bool beatLevel1 = false;
    private bool winning = false;
    private int currentLevel = 1; // Initialize the current level to 1
    public Game()
    {
        int mapWidth = 40;
        int mapHeight = 20;
        map = new char[mapWidth, mapHeight];
        // Initialize the entire map with spaces
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                map[x, y] = ' ';
            }
        }
        // Fill the first three rows with audience:
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                char[] audienceChars = { '0', '/', '\\' }; // Define the characters to repeat
                char audienceChar = audienceChars[x % audienceChars.Length]; // Alternate between '0', '/', and '\'
                map[x, y] = audienceChar;
            }
        }
        // Fill the fourth and last row with '═'a
        for (int x = 0; x < mapWidth; x++)
        {
            map[x, 3] = '═';
            map[x, mapHeight - 1] = '═';
        }
        for (int y = 4; y < mapHeight; y++)
        {
            map[0, y] = '║';          // Left side
            map[mapWidth - 1, y] = '║'; // Right side
        }
        // Set the corners to their respective symbols and colors:
        map[0, mapHeight - 1] = '╚';
        map[mapWidth - 1, mapHeight - 1] = '╝';
        map[0, 3] = '╔';
        map[mapWidth - 1, 3] = '╗';
        //Player, opponents and symbols
        player = new Player(this);
        opponent = new Opponent(map);
        AddRandomSymbols();
    }
    private void SetConsoleColorForSymbol(char symbol)
    {
        ConsoleColor symbolColor = ConsoleColor.White; // Default text color (white)
        // Check if the symbol is in the symbolColors dictionary
        if (symbolColors.ContainsKey(symbol))
        {
            symbolColor = symbolColors[symbol]; // Set the symbol's color
        }
        // Set the console text color
        Console.ForegroundColor = symbolColor;
    }
    private void DrawMap()
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                char cell = map[x, y];
                SetConsoleColorForSymbol(cell);

                if (winning == true && y < 3)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                // Write the symbol to the console
                Console.Write(cell);
            }
            Console.WriteLine();
        }
        // Reset the console text color to its default value (usually ConsoleColor.Gray)
        Console.ResetColor();
    }
    private void UpdateMap()
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                char cell = map[x, y];
                if (cell == '■')
                {
                    // Check if the player is on a '■' symbol
                    if (x == player.Position.X && y == player.Position.Y)
                    {
                        // Create an instance of GasolineSymbol and apply its effect to the player
                        GasolineSymbol gasolineSymbol = new GasolineSymbol();
                        gasolineSymbol.ApplyEffect(player);
                        // Remove the '■' symbol from the map
                        cell = ' ';
                    }
                }
                else if (cell == '♥')
                {
                    // Check if the player is on an '♥' symbol
                    if (x == player.Position.X && y == player.Position.Y)
                    {
                        // Increment player's Health attribute by 1 (up to a maximum of 5)
                        player.Health = Math.Min(player.Health + 1, 5);
                        // Remove the '♥' symbol from the map
                        cell = ' ';
                    }
                }
                else if (cell == opponent.Symbol)
                {
                    // Check if the current cell contains the opponent's symbol
                    if (x == player.Position.X && y == player.Position.Y)
                    {
                        if (player.Gasoline >= opponent.Health)
                        {
                            // Player occupies the opponent's square and has enough gas to defeat the opponent
                            // Remove the opponent symbol from the map
                            cell = ' ';
                        }
                        else
                        {
                            // Player occupies the opponent's square but doesn't have enough gas to defeat the opponent
                            // Remove the player symbol from the map
                            cell = ' ';
                        }
                    }
                }
                if (x == opponent.Position.X && y == opponent.Position.Y)
                {
                    map[x, y] = opponent.Symbol; // Render the opponent symbol
                }
                // Check if the current cell is the player's position
                if (x == player.Position.X && y == player.Position.Y)
                {
                    map[x, y] = player.Symbol; // player symbol
                }
            }
        }
    }
    public class Player
    {
        public Point Position;
        public int Health { get; set; }
        public int Gasoline { get; set; }
        public int RemainingSteps;
        public char Symbol;
        private Game game;
        public Player(Game game, int initialHealth = 5, int initialGasoline = 1, int initialRemainingSteps = 5)
        {
            Health = initialHealth;
            Gasoline = initialGasoline;
            RemainingSteps = initialRemainingSteps;
            Position = new Point(game.map.GetLength(0) / 2, game.map.GetLength(1) / 2);
            Symbol = '☺'; // Change this symbol later
            this.game = game; // Store a reference to the Game instance
        }
        // Add player-related methods here
        public void MovePlayer(ConsoleKey key)
        {
            int targetX = Position.X;
            int targetY = Position.Y;
            // Determine the target position based on the arrow key
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    targetY--;
                    break;
                case ConsoleKey.DownArrow:
                    targetY++;
                    break;
                case ConsoleKey.LeftArrow:
                    targetX--;
                    break;
                case ConsoleKey.RightArrow:
                    targetX++;
                    break;
                default:
                    // Handle other keys or invalid input
                    break;
            }
            // Check if the target position is within the bounds of the map
            if (targetX >= 0 && targetX < game.map.GetLength(0) &&
                targetY >= 0 && targetY < game.map.GetLength(1))
            {
                char targetCell = game.map[targetX, targetY];
                // Check if the target position is not occupied by '║' or '═'
                if (targetCell != '║' && targetCell != '═')
                {
                    // Remove player's symbol from the current position
                    game.map[Position.X, Position.Y] = ' ';
                    // Update the player's position and remaining steps
                    Position = new Point(targetX, targetY);
                    RemainingSteps--;
                    // Check if the player is out of steps
                    if (RemainingSteps == 0)
                    {
                        Console.WriteLine("Out of steps. Opponent's turn.");
                        return; // Exit the method, start the opponent's turn
                    }
                }
            }
        }
        private void HandlePlayerInput(ConsoleKeyInfo keyInfo)
        {
            MovePlayer(keyInfo.Key);
        }
    }
    public class Opponent
    {
        public Point Position { get; private set; }
        public int Health { get; internal set; } = 1; // Fixed health of 1 (it's level 1)
        public readonly char Symbol;
        public int RemainingMoves { get; set; }
        public Opponent(char[,] arena, char symbol = '☻')
        {
            Symbol = symbol;
            InitializeRandomPosition(arena);
            RemainingMoves = 3; // Initialize remaining moves
        }
        public void IncreaseHealth(int amount)
        {
            Health += amount;
        }
        public void InitializeRandomPosition(char[,] arena)
        {
            Random random = new Random();
            int x;
            int y;
            // Keep generating random positions until we find a valid one
            do
            {
                x = random.Next(1, arena.GetLength(0) - 1);
                y = random.Next(arena.GetLength(1) - 5, arena.GetLength(1) - 1);
            } while (arena[x, y] == '═' || arena[x, y] == '║' || x == 0 || x == arena.GetLength(0) - 1);
            Position = new Point(x, y);
        }
        public void MoveOpponent(Point playerPosition, char[,] arena)
        {
            if (RemainingMoves <= 0)
            {
                // No more moves left in this turn
                return;
            }
            int targetX = Position.X;
            int targetY = Position.Y;
            // Calculate direction to move toward the player
            if (playerPosition.X < Position.X)
            {
                targetX--;
            }
            else if (playerPosition.X > Position.X)
            {
                targetX++;
            }
            if (playerPosition.Y < Position.Y)
            {
                targetY--;
            }
            else if (playerPosition.Y > Position.Y)
            {
                targetY++;
            }
            // Check if the target position is within the bounds of the map
            if (targetX >= 0 && targetX < arena.GetLength(0) &&
                targetY >= 0 && targetY < arena.GetLength(1))
            {
                char targetCell = arena[targetX, targetY];
                // Check if the target position is not occupied by '║' or '═'
                if (targetCell != '║' && targetCell != '═')
                {
                    // Remove the opponent's symbol from the current position
                    arena[Position.X, Position.Y] = ' ';
                    // Update the opponent's position and remaining moves
                    Position = new Point(targetX, targetY);
                    RemainingMoves--;
                    // Set the opponent's symbol at the new position
                    arena[Position.X, Position.Y] = Symbol;
                }
            }
        }
    }
    public class HealthSymbol
    {
        public char Symbol { get; private set; } = '♥';
        public void RenderHealthSymbol()
        {
            Console.Write(Symbol);
        }
        public void ApplyEffect(Player player)
        {
            player.Health = Math.Min(player.Health + 1, 5); // Increment health, capped at 5
        }
    }
    public class GasolineSymbol
    {
        public char Symbol { get; private set; } = '■';
        public void ApplyEffect(Player player)
        {
            // Increment player's Gasoline attribute by 1 (up to a maximum of 5)
            if (player.Gasoline < 5)
            {
                player.Gasoline++;
            }
        }
    }
    private void AddRandomSymbols()
    {
        Random random = new Random();
        int mapWidth = map.GetLength(0);
        int mapHeight = map.GetLength(1);
        int healthSymbolsToPlace = 3;
        int gasSymbolsToPlace = 3;
        while (healthSymbolsToPlace > 0 || gasSymbolsToPlace > 0)
        {
            int x = random.Next(mapWidth);
            int y = random.Next(4, mapHeight - 1); // Avoid the border and audience area
            Console.WriteLine($"Trying to place symbol at ({x}, {y})");
            if (map[x, y] == ' ')
            {
                Console.WriteLine("Empty space found.");
                if (healthSymbolsToPlace > 0)
                {
                    HealthSymbol healthSymbol = new HealthSymbol();
                    map[x, y] = healthSymbol.Symbol; // Place a health symbol
                    healthSymbolsToPlace--;
                    Console.WriteLine("Placed a health symbol.");
                }
                else if (gasSymbolsToPlace > 0)
                {
                    GasolineSymbol gasolineSymbol = new GasolineSymbol();
                    map[x, y] = gasolineSymbol.Symbol; // Place a gasoline symbol
                    gasSymbolsToPlace--;
                    Console.WriteLine("Placed a gasoline symbol.");
                }
            }
            else
            {
                Console.WriteLine("Position not empty.");
            }
        }
    }
    private void CheckAndSpawnGasCanisters()
    {
        bool gasCanistersExist = false;

        // Check if there are any gas canisters on the map
        foreach (var cell in map)
        {
            if (cell == '■')
            {
                gasCanistersExist = true;
                break;
            }
        }
        // If no gas canisters are found, spawn three new ones
        if (!gasCanistersExist)
        {
            for (int i = 0; i < 3; i++)
            {
                AddRandomGasolineSymbol();
            }
        }
    }
    private void CheckAndSpawnHealthSymbols()
    {
        bool healthSymbolsExist = false;
        // Check if there are any health pickups on the map
        foreach (var cell in map)
        {
            if (cell == '♥')
            {
                healthSymbolsExist = true;
                break;
            }
        }
        // If no health pickups are found, spawn three new ones
        if (!healthSymbolsExist)
        {
            for (int i = 0; i < 3; i++)
            {
                AddRandomHealthSymbol();
            }
        }
    }
    private void AddRandomHealthSymbol()
    {
        Random random = new Random();
        int mapWidth = map.GetLength(0);
        int mapHeight = map.GetLength(1);

        while (true)
        {
            int x = random.Next(mapWidth);
            int y = random.Next(4, mapHeight - 1);

            if (map[x, y] == ' ')
            {
                HealthSymbol healthSymbol = new HealthSymbol();
                map[x, y] = healthSymbol.Symbol; // Place a health symbol
                break;
            }
        }
    }
    private void AddRandomGasolineSymbol()
    {
        Random random = new Random();
        int mapWidth = map.GetLength(0);
        int mapHeight = map.GetLength(1);
        while (true)
        {
            int x = random.Next(mapWidth);
            int y = random.Next(4, mapHeight - 1);

            if (map[x, y] == ' ')
            {
                GasolineSymbol gasolineSymbol = new GasolineSymbol();
                map[x, y] = gasolineSymbol.Symbol; // Place a gasoline symbol
                break;
            }
        }
    }
    public void increaseLevel()
    {
        currentLevel++;
        opponent.Health++; // Increase opponent's health by one
        opponent.InitializeRandomPosition(map); // Respawn the opponent at a random location
        opponent.RemainingMoves = 3; // Reset opponent's remaining moves
        player.RemainingSteps = 5; // Reset player's remaining steps
    }
    private void HandleCollision(Player player, Opponent opponent)
    {
        // Check if the player and opponent occupy the same square after the player's move
        if (player.Position == opponent.Position)
        {
            if (player.Gasoline >= opponent.Health)
            {
                // Player has enough gas to defeat the opponent
                player.Gasoline -= opponent.Health;
                opponent.IncreaseHealth(1); // Increase opponent's health by 1
                player.Health -= 1; // Decrease player's health by 1

                // Check if the opponent's health is now 5 or more
                if (opponent.Health >= 5)
                {
                    Console.Clear(); // Clear the console before displaying the winning message
                    Console.WriteLine("Congratulations! You won!");
                    winning = true;
                    DrawMap();
                    increaseLevel(); // Increase the level by 1
                    return; // Exit the method, indicating victory
                }
                else
                {
                    // Respawn the opponent at a random location
                    opponent.InitializeRandomPosition(map);
                    increaseLevel(); // Increase the level by 1
                }
            }
            else
            {
                Console.Clear(); // Clear the console before displaying the losing message
                Console.WriteLine("Game over! You lost."); // Player loses
                DrawMap();
                return; // Exit the method, indicating loss
            }
        }
    }
    public void RunGameLoop()
    {
        while (true) // Infinite loop to keep the game running
        {
            CheckAndSpawnGasCanisters();
            CheckAndSpawnHealthSymbols();
            // Player's turn
            player.RemainingSteps = 5; // Reset player's steps to 5 at the beginning of their turn
            Console.Clear();
            UpdateMap();
            DrawMap();
            Console.WriteLine($"Health bar: ({player.Health}) | Gas bar: ({player.Gasoline}) | Remaining Steps: {player.RemainingSteps}");
            Console.WriteLine($"Current Level: ({currentLevel}) | Opponent health: {opponent.Health}");
            Console.WriteLine("Player's turn: Use arrow keys to move.");
            for (int step = 0; step < 5; step++)
            {
                // Read the player's input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                // Handle player movement
                player.MovePlayer(keyInfo.Key);
                // Check if the player and opponent occupy the same square after opponent's move
                if (player.Position == opponent.Position)
                {
                    HandleCollision(player, opponent);
                }
                // Clear the console and update the map after each move
                Console.Clear();
                UpdateMap();
                DrawMap();
                Console.WriteLine($"Health bar: ({player.Health}) | Gas bar: ({player.Gasoline}) | Remaining Steps: {player.RemainingSteps}");
                Console.WriteLine($"Current Level: ({currentLevel}) | Opponent health: {opponent.Health}");
                Console.WriteLine("Player's turn: Use arrow keys to move.");
            }
            // Opponent's turn
            opponent.RemainingMoves = 3; // Reset opponent's moves to 3 at the beginning of their turn
            Console.WriteLine("Opponent's turn:");
            // Move the opponent three steps
            for (int i = 0; i < 3; i++)
            {
                opponent.MoveOpponent(player.Position, map); // Call the opponent's movement logic
                // Check if the player and opponent occupy the same square after opponent's move
                if (player.Position == opponent.Position)
                {
                    HandleCollision(player, opponent);
                }
                // Clear the console and update the map after each opponent move
                Console.Clear();
                UpdateMap();
                DrawMap();
                Console.WriteLine($"Health bar: ({player.Health}) | Gas bar: ({player.Gasoline}) | Remaining Steps: {player.RemainingSteps}");
                Console.WriteLine($"Current Level: ({currentLevel}) | Opponent health: {opponent.Health}");
                Console.WriteLine("Opponent's turn:");
            }
        }
    }
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Step up, brave gladiator.");
        Console.Write("State your name and prepare to be judged! (and press Enter to continue: ");
        Console.WriteLine();
        Console.WriteLine();
        // Read the player's name from the console input
        string playerName = Console.ReadLine();
        Console.WriteLine($"Well met, {playerName}. Get ready to rev up that chainsaw and start the slaughter!");
        Console.WriteLine("Press Enter to begin...");
        Console.ReadLine(); // Wait for the player to press Enter
        // Clear the console and start the game loop
        Console.Clear();
        Game game = new Game();
        game.RunGameLoop();
    }
}