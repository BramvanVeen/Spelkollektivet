using System;
using System.Drawing;
using static Game;
public class Game
{
    private char[,] map;
    private Player player;
    private Opponent opponent;
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
                map[x, y] = '0'; // This needs to be changed to something colorful
            }
        }
        // Fill the fourth and last row with '═'
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
        // Set the corners to their respective symbols:
        map[0, mapHeight - 1] = '╚';
        map[mapWidth - 1, mapHeight - 1] = '╝';
        map[0, 3] = '╔';
        map[mapWidth - 1, 3] = '╗';
        //Player, opponents and symbols
        player = new Player(this);
        opponent = new Opponent(map);
    }

    private void DrawMap()
    {
        Console.Clear();

        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                char cell = map[x, y];
                Console.Write(cell);
            }
            Console.WriteLine();
        }
    }

    private void UpdateMap()
    {
        Console.Clear();

        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                char cell = map[x, y];

                // Check if the current cell is the player's position
                if (x == player.Position.X && y == player.Position.Y)
                {
                    cell = player.Symbol; // player symbol
                }
                else if (cell == 'G')
                {
                    // Check if the player is on a 'G' symbol
                    if (x == player.Position.X && y == player.Position.Y)
                    {
                        // Increment player's Gasoline attribute by 1 (up to a maximum of 5)
                        player.Gasoline = Math.Min(player.Gasoline + 1, 5);
                    }

                    // Replace the 'G' symbol with an empty space
                    cell = ' ';
                }
                else if (cell == 'H')
                {
                    // Check if the player is on an 'H' symbol
                    if (x == player.Position.X && y == player.Position.Y)
                    {
                        // Increment player's Health attribute by 1 (up to a maximum of 5)
                        player.Health = Math.Min(player.Health + 1, 5);
                    }

                    // Replace the 'H' symbol with an empty space
                    cell = ' ';
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
                else if (x == opponent.Position.X && y == opponent.Position.Y)
                {
                    cell = opponent.Symbol; // Render the opponent symbol
                }

                Console.Write(cell);
            }
            Console.WriteLine();
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

            if (map[x, y] == ' ')
            {
                // Check if it's a valid empty space on the map
                if (healthSymbolsToPlace > 0)
                {
                    HealthSymbol healthSymbol = new HealthSymbol();
                    map[x, y] = healthSymbol.Symbol; // Place a health symbol
                    healthSymbolsToPlace--;
                }
                else if (gasSymbolsToPlace > 0)
                {
                    GasolineSymbol gasolineSymbol = new GasolineSymbol();
                    map[x, y] = gasolineSymbol.Symbol; // Place a gasoline symbol
                    gasSymbolsToPlace--;
                }
            }
        }
    }




    public class Player
    {
        public Point Position;
        public int Health;
        public int Gasoline;
        public int RemainingSteps;
        public char Symbol;
        private Game game;

        public Player(Game game, int initialHealth = 5, int initialGasoline = 1, int initialRemainingSteps = 5)
        {
            Health = initialHealth;
            Gasoline = initialGasoline;
            RemainingSteps = initialRemainingSteps;
            Position = new Point(game.map.GetLength(0) / 2, game.map.GetLength(1) / 2);
            Symbol = 'P'; // Change this symbol later
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

                    // Check if the player and opponent occupy the same square
                    if (Position == game.opponent.Position)
                    {
                        // Check the gas bar to determine win or lose
                        if (Gasoline >= game.opponent.Health)
                        {
                            Console.WriteLine("You win!"); // Player wins
                        }
                        else
                        {
                            Console.WriteLine("You lose!"); // Player loses
                        }
                        return; // Exit the method
                    }

                    // Set the player's symbol at the new position
                    game.map[Position.X, Position.Y] = Symbol;

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
        public Point Position;
        public readonly int Health = 1; // Fixed health of 1 (it's level 1)
        public readonly char Symbol;
        public int RemainingMoves { get; set; } // Public property to control access

        public Opponent(char[,] arena, char symbol = 'O')
        {
            Symbol = symbol;
            InitializeRandomPosition(arena);
            RemainingMoves = 3; // Initialize remaining moves
        }

        private void InitializeRandomPosition(char[,] arena)
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
        public char Symbol { get; } = 'H';

        public void ApplyEffect(Player player)
        {
            // Implement the logic for applying the health symbol's effect on the player here
            // For example, you can increment the player's health attribute
            player.Health = Math.Min(player.Health + 1, 5); // Increment health, capped at 5
        }
    }

    public class GasolineSymbol
    {
        public char Symbol { get; } = 'G';

        public void ApplyEffect(Player player)
        {
            // Implement the logic for applying the gasoline symbol's effect on the player here
            // For example, you can increment the player's gasoline attribute
            player.Gasoline = Math.Min(player.Gasoline + 1, 5); // Increment gasoline, capped at 5
        }
    }


    public void RunGameLoop()
    {
        while (true) // Infinite loop to keep the game running
        {
            // Player's turn
            player.RemainingSteps = 5; // Reset player's steps to 5 at the beginning of their turn

            Console.Clear();
            UpdateMap();
            Console.WriteLine($"Health bar: ({player.Health}) | Gas bar: ({player.Gasoline}) | Remaining Steps: {player.RemainingSteps}");
            Console.WriteLine("Player's turn: Use arrow keys to move.");

            for (int step = 0; step < 5; step++)
            {
                // Read the player's input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Handle player movement
                player.MovePlayer(keyInfo.Key);

                // Check if the player and opponent occupy the same square
                if (player.Position == opponent.Position)
                {
                    // Check the gas bar to determine win or lose
                    if (player.Gasoline >= opponent.Health)
                    {
                        Console.WriteLine("Congratulations! You won!"); // Player wins
                        return; // Exit the method
                    }
                    else
                    {
                        Console.WriteLine("Game over! You lost."); // Player loses
                        return; // Exit the method
                    }
                }

                // Clear the console and update the map after each move
                Console.Clear();
                UpdateMap();
                Console.WriteLine($"Health bar: ({player.Health}) | Gas bar: ({player.Gasoline}) | Remaining Steps: {player.RemainingSteps}");
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
                    // Check the gas bar to determine win or lose
                    if (player.Gasoline >= opponent.Health)
                    {
                        Console.WriteLine("Congratulations! You won!"); // Player wins
                        return; // Exit the method
                    }
                    else
                    {
                        Console.WriteLine("Game over! You lost."); // Player loses
                        return; // Exit the method
                    }
                }

                // Clear the console and update the map after each opponent move
                Console.Clear();
                UpdateMap();
                Console.WriteLine($"Health bar: ({player.Health}) | Gas bar: ({player.Gasoline}) | Remaining Steps: {player.RemainingSteps}");
                Console.WriteLine("Opponent's turn:");
            }
        }
    }



    public static void Main(string[] args)
    {
        Console.WriteLine("Step up, brave gladiator.");
        Console.Write("State your name and prepare to be judged! (and press Enter to start: ");
        // Read the player's name from the console input
        string playerName = Console.ReadLine();
        Console.WriteLine($"Well met, {playerName}. Get ready to rev up that chainsaw and start the slaughter!");
        Console.WriteLine("Press Enter to begin...");
        Console.ReadLine(); // Wait for the player to press Enter
        // Clear the console and start the game loop
        Console.Clear();
        Game game = new Game();
        game.RunGameLoop();
        // Add your game logic here
    }
}
