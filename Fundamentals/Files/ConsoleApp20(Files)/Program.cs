using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace ConsoleApp20_Files_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string playerNameFilename = "traveler.txt";
            string backersFilename = "backers.txt";
            string[] backers = File.ReadAllLines(backersFilename);
            if (File.Exists(playerNameFilename))
            {
                string traveler = File.ReadAllText(playerNameFilename);
                Console.WriteLine($"The esteemed {traveler} returns!");
                if (backers.Contains(traveler))
                {
                    Console.WriteLine($"You successfully enter Dr. Fred's secret laboratory and are greeted with a warm welcome for backing the game's Kickstarter!");
                }
                else
                {
                    Console.WriteLine($"Unfortunately I cannot let you into Dr. Fred's secret laboratory.");
                }
            }
            else
            {
                Console.WriteLine("What is your name, traveler?");
                string traveler = Console.ReadLine();
                File.WriteAllText(playerNameFilename, traveler);
                Console.WriteLine($"Nice to meet you, {traveler}!");

                if (backers.Contains(playerNameFilename))
                {
                    Console.WriteLine($"You successfully enter Dr. Fred's secret laboratory and are greeted with a warm welcome for backing the game's Kickstarter!");
                }
                else
                {
                    Console.WriteLine($"Unfortunately I cannot let you into Dr. Fred's secret laboratory.");
                }
            }
        }
    }
}
