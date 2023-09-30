using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> winterOlympicHostCountries = new Dictionary<int, string>();

            winterOlympicHostCountries[2002] = "United States";
            winterOlympicHostCountries[2006] = "Italy";
            winterOlympicHostCountries[2010] = "Canada";
            winterOlympicHostCountries[2014] = "Russia";
            winterOlympicHostCountries[2018] = "South Korea";
            winterOlympicHostCountries[2022] = "China";

            Random random = new Random();
            int year = 2000 + random.Next(winterOlympicHostCountries.Count) * 2;

            Console.WriteLine($"Which country hosted the Summer Olympic Games in {year}? Type your answer, uppercase sensitive..");
            string playerAnswer = Console.ReadLine();

            if (winterOlympicHostCountries.TryGetValue(year, out string correctAnswer))
            {
                if (playerAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine($"Incorrect. It was {correctAnswer}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid year or not in the 21st century.");
            }
        }
    }
}
