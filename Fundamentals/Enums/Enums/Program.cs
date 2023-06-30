using System;
using System.Linq.Expressions;

namespace Enums
{
    internal class Program
        
    {
        enum Suits
        {
            Heart,     //0
            Diamond,   //1
            Club,      //2
            Spade      //3
        }
        static void DrawAce(Suits suit)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            switch (suit)
            {
                case Suits.Heart:
                    {
                        Console.WriteLine($"╭───────╮");
                        Console.WriteLine($"│A      │");
                        Console.WriteLine($"│♥      │");
                        Console.WriteLine($"│   ♥   │");
                        Console.WriteLine($"│      ♥│");
                        Console.WriteLine($"│      A│");
                        Console.WriteLine($"╰───────╯");
                        return;
                    }
                case Suits.Diamond:
                    {
                        Console.WriteLine($"╭───────╮");
                        Console.WriteLine($"│A      │");
                        Console.WriteLine($"│♦      │");
                        Console.WriteLine($"│   ♦   │");
                        Console.WriteLine($"│      ♦│");
                        Console.WriteLine($"│      A│");
                        Console.WriteLine($"╰───────╯");
                        return;
                    }
                case Suits.Spade:
                    {
                        Console.WriteLine($"╭───────╮");
                        Console.WriteLine($"│A      │");
                        Console.WriteLine($"│♠      │");
                        Console.WriteLine($"│   ♠   │");
                        Console.WriteLine($"│      ♠│");
                        Console.WriteLine($"│      A│");
                        Console.WriteLine($"╰───────╯");
                        return;
                    }
                case Suits.Club:
                    {
                        Console.WriteLine($"╭───────╮");
                        Console.WriteLine($"│A      │");
                        Console.WriteLine($"│♣      │");
                        Console.WriteLine($"│   ♣   │");
                        Console.WriteLine($"│      ♣│");
                        Console.WriteLine($"│      A│");
                        Console.WriteLine($"╰───────╯");
                        return;
                    }

            }

        }
        static void Main(string[] args)
        {
            DrawAce(Suits.Diamond);
        }
    }
}
