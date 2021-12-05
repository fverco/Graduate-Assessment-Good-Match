using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MatchUpLibrary
{
    public static class PlayerMatcher
    {
        /// <summary>
        /// Calculates the match percentage between two people's first names.
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        public static void matchPlayers(string name1, string name2)
        {
            string sentance = $"{name1} matches {name2}";

            if (verifyName(name1) && verifyName(name2))
            {
                List<int> charCount = countChars(sentance);
            }
            else
            {
                Console.WriteLine("Some or both of the names contain non-alphabetic characters.");
            }
        }

        /// <summary>
        /// Return true if the name only contains alphabetic characters.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static bool verifyName(string name)
        {
            return Regex.IsMatch(name, "^[a-zA-Z]*$");
        }

        /// <summary>
        /// Counts the occurence of each character in the sentance.
        /// </summary>
        /// <param name="sentance"></param>
        /// <returns>A List<int> with the count of each character.</returns>
        static List<int> countChars(string sentance)
        {
            List<int> charCount = new List<int>();
            List<char> sentanceLetters = new List<char>();
            sentanceLetters.AddRange(sentance.Replace(" ", string.Empty));

            

            return charCount;
        }
    }
}
