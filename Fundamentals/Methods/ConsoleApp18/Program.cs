using System;
using System.Collections.Generic;

namespace ConsoleApp18
{
    internal class Program
    {
        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {
            var random = new Random();
            int result = 0;
            for (int i = 0; i < numberOfRolls; i++)
            {
                int roll = random.Next(1, diceSides + 1);
                result += roll;
            }
            result += fixedBonus;
            return result;
        }



        static void SimulateCombat(List<string> characterNames, string monsterName, int monsterHP, int savingThrowDC)
        {
            var random = new Random();
            bool winning = false;

            Console.WriteLine($"Watch out, {monsterName} with {monsterHP} HP appears.");
            Console.WriteLine("");

            while (monsterHP > 0)
            {
                foreach (var name in characterNames)
                {
                    int greatsword = DiceRoll(2, 6);
                    monsterHP -= greatsword;

                    if (monsterHP < 1)
                    {
                        monsterHP = 0;
                        Console.Write(name);
                        Console.WriteLine($" hits the {monsterName} for {greatsword} damage. The {monsterName} has {monsterHP} HP left");
                        Console.WriteLine("");
                        Console.WriteLine($"The mighty warriors stab the beast to death.");
                        Console.WriteLine("");
                        winning = true;
                        break;
                    }
                    if (monsterHP > 0)
                    {
                        Console.Write(name);
                        Console.WriteLine($" hits the {monsterName} for {greatsword} damage. The {monsterName} has {monsterHP} HP left");
                        Console.WriteLine("");
                    }
                }

                if (!winning)
                {
                    var victim = characterNames[random.Next(characterNames.Count)];
                    Console.Write($"The {monsterName} uses its nasty attack on: " + $"{victim}");
                    Console.WriteLine("");

                    
                    int savingThrow = DiceRoll(1, 20);

                    if (savingThrow < savingThrowDC)
                    {
                        Console.WriteLine($"{victim} rolls a total of {savingThrow} and fails to be saved. {victim} dies.");
                        Console.WriteLine("");
                        characterNames.Remove(victim);
                    }

                    else
                    {
                        Console.WriteLine($"{victim} rolls a total of {savingThrow} and succeeds. {victim} shruggs it off like a champ.");
                        Console.WriteLine("");
                    }

                    if (characterNames.Count == 0)
                    {
                        Console.WriteLine($"The party has utterly failed to perform and the {monsterName} goes home to its kids and wife.");
                        break;

                    }


                }




            }



        }

        static void Main(string[] args)
        {
            var characterNames = new List<string> { "Buck", "Brick", "Chuck", "Larry" };

            Console.Write($"Fighters ");
            Console.Write(string.Join(", ", characterNames));
            Console.WriteLine($" descend into the dungeon.");
            Console.WriteLine("");

            SimulateCombat(characterNames, "Orc", DiceRoll(2, 8, 6), 10);
            if (characterNames.Count > 0)
            {
                SimulateCombat(characterNames, "Azer", DiceRoll(6, 8,12), 18);
            }
            if (characterNames.Count > 0)
            {
                SimulateCombat(characterNames, "Troll", DiceRoll(8, 10, 40), 16);
            }
            if (characterNames.Count > 0)
            {
                string output = String.Join(" ", characterNames);
                Console.WriteLine("The survivors, ");
                Console.Write(output);
                Console.WriteLine(" make their way back to the bar.");
            }


        }
    }
}
