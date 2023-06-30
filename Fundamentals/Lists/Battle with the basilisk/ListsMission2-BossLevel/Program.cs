using System;
using System.Collections.Generic;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace ListsMission2_BossLevel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var monsterhp = 0;
            var consave = 0;

            var names = new List<string> { "Buck", "Brick", "Chuck", "Larry" };

            Console.Write($"Fighters ");
            Console.Write(String.Join(", ", names));
            Console.WriteLine($" descend into the dungeon.");
            Console.WriteLine("");

            for (int i = 0; i < 8; i++)
            {
                monsterhp += random.Next(1, 9);
            }
            monsterhp += 16;
            Console.WriteLine($"A basilisk with {monsterhp} hitpoints appears.");
            Console.WriteLine("");

            while (monsterhp > 0)
            {
                foreach (var name in names)
                {
                    int total = 0;
                    for (int i = 0; i < 1; i++)
                    {
                        total += random.Next(1, 5);
                    }

                    monsterhp -= total;

                    if (monsterhp < 0)
                    {
                        monsterhp = 0;
                        Console.Write(name);
                        Console.WriteLine($" hits the basilisk for {total} damage. The basilisk has {monsterhp} HP left");
                        Console.WriteLine("");
                        Console.WriteLine($"The mighty warriors stab the beast to death.");
                        Console.WriteLine("");
                        break;
                    }

                    Console.Write(name);
                    Console.WriteLine($" hits the basilisk for {total} damage. The basilisk has {monsterhp} HP left");
                    Console.WriteLine("");
                }
                if (monsterhp == 0)

                {
                    Console.WriteLine("All hail the mighty warriors!");
                }

                else
                {
                    var victim = names[random.Next(names.Count)];
                    Console.Write($"The basilisk uses petrifying gaze on: " + $"{victim}");
                    Console.WriteLine("");

                    consave = random.Next(1, 21) + 3;

                    if (consave < 12)
                    {
                        Console.WriteLine($"{victim} rolls a total of {consave} and fails to be saved. {victim} is turned into stone.");
                        Console.WriteLine("");
                        names.Remove(victim);
                    }

                    else

                        Console.WriteLine($"{victim} rolls a total of {consave} and succeeds. {victim} shruggs it off like a champ.");
                    Console.WriteLine("");

                    if (names.Count == 0)
                    {
                        Console.WriteLine($"The party has failed and the basilisk continues to turn unsuspecting adventurers to stone.");
                        break;
                    }
                }

            }
        }










    }



}


