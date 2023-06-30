using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Schema;

namespace FundamentalsListsMission1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            List<int> rolls = new List<int>();
            List<int> abilities = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    rolls.Add(random.Next(1, 7));
                }
                Console.WriteLine(String.Join(", ", rolls));
                
                rolls.Sort();
                rolls.Remove(rolls[0]);
                
                var total = rolls.Sum();

                Console.WriteLine($"The ability score is {total}.");
                abilities.Add(total);
                rolls.Clear();

            }
            abilities.Sort();
            Console.Write("Your available ability scores are: ");
            Console.Write(String.Join(", ", abilities));
            
        }
    }
}

