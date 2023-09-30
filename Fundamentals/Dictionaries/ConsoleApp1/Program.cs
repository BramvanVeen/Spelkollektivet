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
            int[] values = { 0, 1, 2, 3, 4, 5 };
            int randomIndex = random.Next(values.Length);
            int selectedYear = 0;
            switch (values[randomIndex])
            {
                case 0:
                    selectedYear = 2002;
                    break;
                case 1:
                    selectedYear = 2006;
                    break;
                case 2:
                    selectedYear = 2010;
                    break;
                case 3:
                    selectedYear = 2014;
                    break;
                case 4:
                    selectedYear = 2018;
                    break;
                case 5:
                    selectedYear = 2022;
                    break;
            }
            Console.WriteLine($"Which country hosted the Winter Olympic Games in {selectedYear}? Type your answer, uppercase sensitive..");
            string playerAnswer = Console.ReadLine();

            if (winterOlympicHostCountries.TryGetValue(selectedYear, out string correctAnswer))
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
