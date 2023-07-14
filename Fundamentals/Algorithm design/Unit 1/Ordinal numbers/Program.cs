using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Ordinal_numbers
{
    internal class Program
    {
        static string OrdinalNumber(int number)
        {
            int lastDigit = number % 10;
            if (number > 10)
            {
                int secondToLastDigit = number / 10 % 10;
                if (secondToLastDigit == 1)
                { 
                    return $"{number}th"; 
                }
            }
            if (lastDigit == 1)
            {
                return $"{number}st";
            }
            if (lastDigit == 2)
            {
                return $"{number}nd";
            }
            if (lastDigit == 3)
            { 
                return $"{number}rd"; 
            }
            else
            {
                return $"{number}th";
            }
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 50; i++) 
            { 
                Console.WriteLine(OrdinalNumber(i)); 
            }
        }
    }
}
