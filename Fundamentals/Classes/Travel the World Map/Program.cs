using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;

namespace Travel_the_World_Map
{
    internal class Program
    {
        class Location
        {
            public string Name;
            public string Description;
            public List<Location> Neighbors = new List<Location>();
        }
        static void ConnectLocations(Location a, Location b)
        {
            a.Neighbors.Add(b);
            b.Neighbors.Add(a);
        }
 
        static void Main(string[] args)
        {
            List<Location> locations = new List<Location>();

            Location pyke = new Location
            {
                Name = "Pyke",
                Description = "the stronghold and seat of House Greyjoy",
                Neighbors = new List<Location>()
            };
            locations.Add(pyke);

            Location riverrun = new Location
            {
                Name = "Riverrun",
                Description = "a large castle located in the central-western part of the Riverlands",
                Neighbors = new List<Location>()
            };
            locations.Add(riverrun);

            Location winterfell = new Location
            {
                Name = "Winterfell",
                Description = "the capital of the Kingdom of the North",
                Neighbors = new List<Location>()
            };
            locations.Add(winterfell);

            Location theTrident = new Location
            {
                Name = "The Trident",
                Description = "one of the largest and most well-known rivers on the continent of Westeros",
                Neighbors = new List<Location>()
            };
            locations.Add(theTrident);

            Location kingsLanding = new Location
            {
                Name = "King's Landing",
                Description = "the capital, and largest city, of the Seven Kingdoms",
                Neighbors = new List<Location>()
            };
            locations.Add(kingsLanding);

            Location highgarden = new Location
            {
                Name = "Highgarden",
                Description = "the seat of House Tyrell and the regional capital of the Reach",
                Neighbors = new List<Location>()
            };
            locations.Add(highgarden);

            ConnectLocations(pyke, winterfell);
            ConnectLocations(pyke, riverrun);
            ConnectLocations(pyke, highgarden);
            ConnectLocations(winterfell, theTrident);
            ConnectLocations(theTrident, riverrun);
            ConnectLocations(theTrident, kingsLanding);
            ConnectLocations(kingsLanding, riverrun);
            ConnectLocations(kingsLanding, highgarden);
            ConnectLocations(highgarden, riverrun);

            Location currentLocation = locations[5];

            bool continueTravel = true;

            while (continueTravel)
            {
                Console.WriteLine($"You are currently in: {currentLocation.Name}, {currentLocation.Description}\n");
                Console.WriteLine($"Your possible destinations are:\n");

                for (int i = 0; i < currentLocation.Neighbors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {currentLocation.Neighbors[i].Name}");
                }
                Console.WriteLine();
                Console.WriteLine($"Where do you wish to travel? (Type number and press enter..");
                int chosenDestination = Convert.ToInt32(Console.ReadLine()) - 1;

                if (chosenDestination >= 0 && chosenDestination < currentLocation.Neighbors.Count)
                {
                    currentLocation = currentLocation.Neighbors[chosenDestination];
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid destination.");
                }
            }
        }
    }
}
