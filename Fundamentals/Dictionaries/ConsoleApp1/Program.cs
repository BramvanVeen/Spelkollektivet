using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;

class Program
{
    static void Main(string[] args)
    {
        //Create a Dictionary that maps years to host countries:
        Dictionary<int, string> winterOlympicHostCountries = new Dictionary<int, string>();

        winterOlympicHostCountries[2002] = "United States";
        winterOlympicHostCountries[2006] = "Italy";
        winterOlympicHostCountries[2010] = "Canada";
        winterOlympicHostCountries[2014] = "Russia";
        winterOlympicHostCountries[2018] = "South Korea";
        winterOlympicHostCountries[2022] = "China";

        //To generate a random year, a list of years using the Keys property of the dictionary, as suggested in the hint:
        var year = new List<int>(winterOlympicHostCountries.Keys);
        //Use a random number generator to select a random country from the list:
        Random random = new Random();

        while (true)
        {
            int randomIndex = random.Next(year.Count);
            int randomYear = year[randomIndex];
            //Ask the player to provide the country that hosted the winter olympics:
            Console.Write($"Which country hosted the Winter Olympic Games in {randomYear}? Type your answer, uppercase sensitive..\" ");
            string playerAnswer = Console.ReadLine();
            //Check the player's answer against the dictionary and respond accordingly:

            if ((winterOlympicHostCountries.TryGetValue(randomYear, out string correctAnswer)) && (playerAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine($"Incorrect. It is {correctAnswer}.");
            }

            Console.Write("Play again? (yes/no): ");
            string playAgain = Console.ReadLine().Trim().ToLower();

            if (playAgain != "yes")
            {
                break; // Exit the loop if the player doesn't want to play again
            }
        }
    }
}
