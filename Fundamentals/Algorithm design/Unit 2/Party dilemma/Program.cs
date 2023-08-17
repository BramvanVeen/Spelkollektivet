using System;

namespace Party_dilemma
{
    public class Program
    {
        static int Factorial(int n)
        {
            if (n == 0)
                return 1;
            if (n <= 1)
                return 1;
            return n * Factorial(n - 1);
        }





        public static void Main(string[] args)
        {
            Console.WriteLine(Factorial(5));  
        }
    }
}
