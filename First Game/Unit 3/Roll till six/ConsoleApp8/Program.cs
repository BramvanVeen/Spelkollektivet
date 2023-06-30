using System;
using System.Xml.Schema;

namespace ConsoleApp8
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int d6;
            var random = new Random();
            int sum = 0;

            do

            {
                d6= random.Next(1, 7);
                sum += d6;

               
                    Console.WriteLine($"The player rolled: {d6}");
               
            } 
            
            while (d6 < 6);

            Console.WriteLine($"For a total of: {sum}");

        }
    }
}
