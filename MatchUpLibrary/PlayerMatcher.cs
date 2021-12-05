using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MatchUpLibrary
{
    public static class PlayerMatcher
    {
        public static void matchPlayers(string name1, string name2)
        {
            string sentance = $"{name1} matches {name2}";

            List<char> sentanceLetters = new List<char>();
            sentanceLetters.AddRange(sentance);

            if (verifyName(name1) && verifyName(name2))
            {
                Console.WriteLine($"{name1} matches {name2}");
            }
            else
            {
                Console.WriteLine("Some or both of the names contain non-alphabetic characters.");
            }
        }

        /// <summary>
        /// Return true if the name only contains alphabetic characters.
        /// </summary>
        /// <param name="name">Name of player</param>
        /// <returns>true or false</returns>
        static bool verifyName(string name)
        {
            return Regex.IsMatch(name, "^[a-zA-Z]*$");
        }
    }
}
