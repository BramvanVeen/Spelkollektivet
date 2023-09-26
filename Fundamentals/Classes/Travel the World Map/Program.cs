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

        static Location GoToLocation(Location currentLocation)
        {
            Console.Clear();
            Console.WriteLine($"You have arrived at the gates of {currentLocation.Name}, {currentLocation.Description}.");
            Console.WriteLine($"Possible destinations are:");

            for (int i = 0; i < currentLocation.Neighbors.Count; i++)
            {
                Console.WriteLine($"-{i + 1}.  {currentLocation.Neighbors[i].Name}");
            }

            Console.WriteLine();
            Console.WriteLine($"Where would you like to travel?");
            string playerInput = Console.ReadLine();
            int neighborIndex = Int32.Parse(playerInput) - 1;

            Location selectedNeighbor = currentLocation.Neighbors[neighborIndex];

            return selectedNeighbor;
        }
        static void Main(string[] args)
        {
            var locations = new List<Location>();

            var pyke = new Location
            {
                Name = "Pyke",
                Description = "the stronghold and seat of House Greyjoy",
                Neighbors = new List<Location>()
            };
            locations.Add(pyke);

            var riverrun = new Location
            {
                Name = "Riverrun",
                Description = "a large castle located in the central-western part of the Riverlands",
                Neighbors = new List<Location>()
            };
            locations.Add(riverrun);

            var winterfell = new Location
            {
                Name = "Winterfell",
                Description = "the capital of the Kingdom of the North",
                Neighbors = new List<Location>()
            };
            locations.Add(winterfell);

            var theTrident = new Location
            {
                Name = "The Trident",
                Description = "one of the largest and most well-known rivers on the continent of Westeros",
                Neighbors = new List<Location>()
            };
            locations.Add(theTrident);

            var kingsLanding = new Location
            {
                Name = "King's Landing",
                Description = "the capital, and largest city, of the Seven Kingdoms",
                Neighbors = new List<Location>()
            };
            locations.Add(kingsLanding);

            var highgarden = new Location
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

            var currentLocation = locations[5];

            //Write a welcome message to the player, stating both the name and description of the current location.

            Console.Write($"State your name: "); var playerName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine($"Welcome, {playerName}. You have arrived at the gates of {currentLocation.Name}, {currentLocation.Description}.");
            Console.WriteLine($"Possible destinations are:");
            for (int i = 0; i < currentLocation.Neighbors.Count; i++)
            {
                Console.WriteLine($"-{i + 1}.  {currentLocation.Neighbors[i].Name}");
            }

            Console.WriteLine();
            Console.WriteLine($"Where would you like to travel?");
            string playerInput = Console.ReadLine();
            int neighborIndex = Int32.Parse(playerInput) - 1;


        }
    }
}
