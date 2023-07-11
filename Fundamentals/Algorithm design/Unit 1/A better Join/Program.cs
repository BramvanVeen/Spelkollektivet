using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace A_better_Join
{
    internal class Program
    {
        static string JoinWithAnd(List<string> items, bool useSerialComma = true)
        {
            int count = items.Count;
            if (count == 0)
            {
                // We have no items, simply return an empty string.
                return string.Empty;
            }
            else if (count == 1)
            {
                // We have one item, simply return a single item.
                return items[0];
            }
            else if (count == 2)
            {
                // We have two items, return two items with "and" in the middle.
                return string.Join(" and ", items);
            }
            else
            {
                var itemsCopy = new List<string>(items);
                if (useSerialComma)
                {
                    //prepend "and " to the last item in the copied list.
                    itemsCopy[count - 1] = "and " + itemsCopy[count - 1];
                }
                else
                {
                    //Join the last two items with "and",
                    //and set this text as the second to last item in the copied list.
                    //remove the last item in the copied list
                    itemsCopy[count - 2] = itemsCopy[count - 2] + " and " + itemsCopy[count - 1];
                    itemsCopy.RemoveAt(itemsCopy.Count - 1);
                }
                //return the copied list of items joined with ",".
                return string.Join(", ", itemsCopy);
            }
        }
        static void Main(string[] args)
        {
            var people = new List<string> { "one", "two", "three", "four", "five", "six" };

            while (people.Count >= 0)
            {
                Console.WriteLine(JoinWithAnd(people));
                Console.WriteLine(JoinWithAnd(people, false));

                if (people.Count == 0)
                {
                    break;
                }
                people.RemoveAt(people.Count - 1);
            }


        }
    }
}
