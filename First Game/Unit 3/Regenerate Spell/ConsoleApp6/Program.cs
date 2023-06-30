using System;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            int hp = random.Next(0, 101);

                Console.WriteLine($"Warrior {hp} HP.");
                

            while (hp < 50) 
            
            {
                Console.WriteLine($"The Mage casts Regenerate!");
                hp = hp + 10;
                Console.WriteLine($"Warrior {hp} HP.");
                
            }

            if (hp > 49)
            {
                Console.WriteLine($"Regenerate succesfull.");
            }









        }
    }
}
