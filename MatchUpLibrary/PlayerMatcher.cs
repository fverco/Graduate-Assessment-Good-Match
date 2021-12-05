using System;
using System.Collections.Generic;

namespace MatchUpLibrary
{
    public static class PlayerMatcher
    {
        public static void matchPlayers(string name1, string name2)
        {
            string sentance = $"{name1} matches {name2}";

            List<char> sentanceLetters = new List<char>();
            sentanceLetters.AddRange(sentance);

            foreach (char character in sentanceLetters)
            {
                Console.WriteLine(character);
            }
        }
    }
}
