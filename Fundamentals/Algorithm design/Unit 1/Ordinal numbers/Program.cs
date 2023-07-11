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
            int numberModified = number % 10;
            if (number > 10)
            {
                int numberModifiedII = number / 10 % 10;
                if (numberModifiedII == 1)
                { 
                    return $"{number}th"; 
                }
            }
            if (numberModified == 1)
            {
                return $"{number}st";
            }
            if (numberModified == 2)
            {
                return $"{number}nd";
            }
            if (numberModified == 3)
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
