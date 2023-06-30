using System;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            int diceThrow = random.Next(0, 11);




            int diceThrow2 = random.Next(0, 11 - diceThrow);

            if (diceThrow == 10)

            {
                Console.WriteLine($"X");
                Console.WriteLine($"A Strike was had this round");
            }

            else if (diceThrow == 0)

            {
                Console.WriteLine($"-");
            }

            else { Console.WriteLine($"{diceThrow}"); }

            if ((diceThrow < 10) && (diceThrow2 + diceThrow == 10))

            {

                Console.WriteLine($"/");



            }

            else if (diceThrow < 10)

            { Console.WriteLine($"{diceThrow2}"); }
        }
    }
}
