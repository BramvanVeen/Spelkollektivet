using System;

namespace ConsoleApp11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            int tankDistance = random.Next(40, 71);
            int ground = 2;


            Console.WriteLine("DANGER! A tank is approaching our position. Your artillery unit is our only hope!");
            Console.WriteLine();
            Console.WriteLine("What is your name, commander?");
            Console.Write("Enter name: ");
            string name;
            name = Console.ReadLine();
            Console.WriteLine("Here is the map of the battlefield: " + name);
            Console.WriteLine();
            Console.Write("_/");

            do
            {
                if (ground == tankDistance)
                {
                    Console.Write("T");
                }
                else
                {
                    Console.Write("_");
                }
                ground++;
            } while (ground < 81);

            Console.WriteLine();
            Console.WriteLine();

            for (int shells = 0; shells < 5; shells++)


            {
                Console.WriteLine();
                Console.WriteLine("Aim your shot, " + name + "!)");
                Console.Write("Enter distance: ");
                int distance = Convert.ToInt32(Console.ReadLine());

                for (int ground2 = 0; ground2 < 81; ground2++)
                {
                    if (ground2 == distance)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                }
                //Hier proberen we het schot te laten zien
                if (distance < tankDistance)
                {
                    Console.WriteLine();
                    Console.WriteLine("Oh no, your shot was too short.");
                }

                if (distance > tankDistance)
                {
                    Console.WriteLine();
                    Console.WriteLine("Alas, the shell flies past the tank.");
                }
                if (distance == tankDistance)
                {
                    Console.WriteLine();
                    Console.WriteLine("Death rains from above!");
                    Console.WriteLine();
                    Console.WriteLine($"Using {shells + 1} shots you destroyed the tank.");
                    Console.WriteLine("Winner, you rock!!!");
                    break;
                }

                if (shells == 4)
                {
                    Console.WriteLine();
                    Console.WriteLine("You wasted all ammo, run to the hills!");
                    Console.WriteLine("**************GAME OVER**************");
                    break;
                }
            }
        }
    }
}
