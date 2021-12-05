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

                Console.WriteLine();
                foreach (int i in charCount)
                {
                    Console.Write(i);
                }
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
            List<int> charCountList = new List<int>();
            List<char> sentanceLetters = new List<char>();
            sentanceLetters.AddRange(sentance.Replace(" ", string.Empty).ToLower());
            char currentChar;
            int currentCharCount;

            int i = 0;
            while (i < sentanceLetters.Count)
            {
                currentChar = sentanceLetters[i];
                currentCharCount = 1;
                sentanceLetters.RemoveAt(i);

                int j = i;
                while (j < sentanceLetters.Count)
                {
                    if (sentanceLetters[j] == currentChar)
                    {
                        ++currentCharCount;
                        sentanceLetters.RemoveAt(j);
                    }
                    else
                    {
                        ++j;
                    }
                }

                charCountList.Add(currentCharCount);
            }

            return charCountList;
        }
    }
}
