using System;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            int roll = 0;
            int total = 0;

            for (int i = 0; i < 3; i++)
            //Generate a character's strength by rolling 3d6 (this follows the Advanced Dungeons & Dragons 2nd Edition rules). Display their strength.
            {
                roll = random.Next(1, 7);
                total += roll;  
            }
            Console.WriteLine($"A character with strength {total} was created.");
            //A gelatinous cube in DnD has 8d10+40 hit points. Create one and display its HP.
            total = 0;
            for (int i = 0; i < 8; i++)
            {
                roll = random.Next(1, 11);
                total += roll;
            }
            Console.WriteLine($"A gelatinous cube with {total + (40)} HP appears!.");

            //Now create an army of 100 gelatinous cubes and display their combined HP.

            total = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int a = 0; a < 8; a++)
                {
                    roll = random.Next(1, 11);
                    total += roll;
                }
                total += 40;
            }

            Console.WriteLine($"Dear gods, an army of 100 cubes decends upon us with a total of {total} HP appears!.");













        }
    }
}
