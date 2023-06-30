using System;

namespace ConsoleApp21
{
    internal class Program
    {
        enum Season
        {
            Spring,     
            Summer,   
            Autumn,     
            Winter     
        }

        static string[] Seasons = { "Spring", "Summer", "Autumn", "Winter" };
        static string CreateDayDescription(int dayNumber, Season season, int yearNumber)
        {
            return $"{dayNumber} day of {Seasons[(int)season]} in the year {yearNumber}";
        }
       
        static void Main(string[] args)
        {
            string dayDescription = CreateDayDescription(26, Season.Summer, 1984);
            Console.WriteLine(dayDescription);
        }
    }
}
