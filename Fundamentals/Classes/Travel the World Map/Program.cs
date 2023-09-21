using System;
using System.Collections.Generic;

namespace Travel_the_World_Map
{
    internal class Program
    {
        class Location
        {
            public string Name;
            public string Description;
        }
        static void Main(string[] args)
        {
            var locations = new List<Location>();

            var winterfell = new Location();
            winterfell.Name = "Winterfell";
            winterfell.Description = "the capital of the Kingdom of the North";
            locations.Add(winterfell);

            var pyke = new Location
            {
                Name = "Pyke",
                Description = "the stronghold and seat of House Greyjoy"
            };

            locations.Add(pyke);


            locations.Add(new Location{Name = "Riverrun",Description = "a large castle located in the central-western part of the Riverlands"});




            Console.WriteLine();

        }
    }
}
