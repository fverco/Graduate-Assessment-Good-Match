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
        /// <returns>A string with the match percentage, or an error message if the names are invalid. </returns>
        public static string MatchPlayers(string name1, string name2)
        {
            string sentance = $"{name1} matches {name2}";

            if (VerifyName(name1) && VerifyName(name2))
            {
                List<int> charCountList = CountChars(sentance);
                int matchPercentage = ReduceToPercentage(charCountList);
                string output = $"{name1} matches {name2} {matchPercentage}%";

                if (matchPercentage > 80)
                {
                    return output + ", good match";
                }
                else
                {
                    return output;
                }
            }
            else
            {
                return "Error. One or both of the names contain non-alphabetic characters.";
            }
        }

        /// <summary>
        /// Return true if the name only contains alphabetic characters.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static bool VerifyName(string name)
        {
            return Regex.IsMatch(name, "^[a-zA-Z]*$");
        }

        /// <summary>
        /// Counts the occurence of each character in the sentance.
        /// </summary>
        /// <remarks>
        /// This is not case sensitive and spaces are ignored.
        /// </remarks>
        /// <param name="sentance"></param>
        /// <returns>A List<int> with the count of each character.</returns>
        static List<int> CountChars(string sentance)
        {
            List<int> charCountList = new();
            List<char> sentanceLetters = new();
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

        /// <summary>
        /// Reduces the list of integers to a double digit number.
        /// </summary>
        /// <param name="charCountList">List of integers that represents the count of characters.</param>
        /// <returns>A double digit number representing a percentage.</returns>
        static int ReduceToPercentage(List<int> charCountList)
        {
            List<int> tempCountList = new();

            while (charCountList.Count != 2)
            {
                while (charCountList.Count > 1)
                {
                    AddAsDigits(charCountList[0] + charCountList[^1], tempCountList);
                    charCountList.RemoveAt(0);
                    charCountList.RemoveAt(charCountList.Count - 1);
                }

                if (charCountList.Count == 1)
                {
                    tempCountList.Add(charCountList[0]);
                }

                charCountList = new List<int>(tempCountList);
                tempCountList.Clear();
            }
            
            return Int32.Parse($"{charCountList[0]}{charCountList[1]}");
        }

        /// <summary>
        /// Splits a number into digits and adds them individually into a List<int>.
        /// </summary>
        /// <remarks>
        /// If a single digit number is passed instead, it will be added to the List<int> directly.
        /// </remarks>
        /// <param name="num">The number whose digits will be split.</param>
        /// <param name="digitList">The list to which the digits will be added.</param>
        static void AddAsDigits(in int num, List<int> digitList)
        {
            if (num > 9)
            {
                string numText = num.ToString();

                for (int i = 0; i < numText.Length; i++)
                {
                    digitList.Add(numText[i] - '0');
                }
            } 
            else
            {
                digitList.Add(num);
            }
        }
    }
}
