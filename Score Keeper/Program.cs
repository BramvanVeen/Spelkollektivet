using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, int> scores = new Dictionary<string, int>();

        while (true)
        {
            Console.Write("Who won this round? (Enter player's name or press Enter to finish): ");
            string winnerName = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(winnerName))
                break;

            // Check if the player already has a score, and if not, initialize it to 0.
            if (!scores.ContainsKey(winnerName))
            {
                scores[winnerName] = 0;
            }

            // Increase the score for the winner by one.
            scores[winnerName]++;

            // Display the updated rankings.
            Console.WriteLine("\nRANKINGS");
            foreach (var player in GetSortedPlayers(scores))
            {
                Console.WriteLine($"{player} {scores[player]}");
            }
        }
    }

    // Function to get players sorted by their scores.
    static IEnumerable<string> GetSortedPlayers(Dictionary<string, int> scores)
    {
        return scores.Keys.OrderByDescending(player => scores[player]);
    }
}
