using System;
using MatchUpLibrary;
using System.IO;
using System.Collections.Generic;

namespace GoodMatch
{
    internal class Program
    {
        static void Main()
        {
            string inp = "";
            Console.WriteLine("Welcome to Good Match.");
            
            while (inp.ToLower() != "s" && inp.ToLower() != "m")
            {
                Console.WriteLine("Single input mode or Multi input mode? <S or M>");
                inp = Console.ReadLine();
            }
            Console.WriteLine();

            if (inp.ToLower() == "s")
            {
                RunSingleInputMode();
            }
            else
            {
                RunMultiInputMode();
            }
        }

        /// <summary>
        /// Executes Single input mode where only a single pair of players are matched.
        /// </summary>
        static void RunSingleInputMode()
        {
            string name1, name2;

            Console.WriteLine("Single input mode.\nPlease provide two player names...\n");

            Console.Write("Name 1: ");
            name1 = Console.ReadLine();

            Console.WriteLine();

            Console.Write("Name 2: ");
            name2 = Console.ReadLine();

            Console.WriteLine();

            MatchResult? result = PlayerMatcher.MatchPlayers(name1, name2);

            if (result != null)
            {
                Console.WriteLine(result.Value.resultMessage);
            }
            else
            {
                Console.WriteLine("One or both names contain non-alphabetic characters.");
            }
        }

        /// <summary>
        /// Executes Multi input mode where multiple pairs of players are matched.
        /// </summary>
        static void RunMultiInputMode()
        {
            string path = "";

            Console.WriteLine("Multi input mode.");

            while (!File.Exists(path) || !path.ToLower().EndsWith(".csv"))
            {
                Console.WriteLine("Please provide a valid path to a CSV file with all the players...");
                path = Console.ReadLine();
            }

            List<string> malePlayerList = new();
            List<string> femalePlayerList = new();

            ReadInputFromFile(path, malePlayerList, femalePlayerList);

            List<MatchResult?> results = new();
            bool invalidInputFound = false;

            // Match each male player with each female player.
            foreach (string male in malePlayerList)
            {
                foreach (string female in femalePlayerList)
                {
                    MatchResult? matchResult = PlayerMatcher.MatchPlayers(male, female);

                    if (matchResult != null)
                    {
                        results.Add(matchResult);
                    }
                    else
                    {
                        if (!invalidInputFound)
                        {
                            invalidInputFound = true;
                        }
                    }
                }
            }

            SortResults(results);

            WriteResultsToFile(results);

            if (invalidInputFound)
            {
                Console.WriteLine("\nSome of the names were invalid and as a result they were ignored.");
            }
        }

        /// <summary>
        /// Reads players names from a csv file and splits them into male and female player lists.
        /// </summary>
        /// <param name="path">The path to the csv file.</param>
        /// <param name="malePlayerList">The list for the male players.</param>
        /// <param name="femalePlayerList">The list for the female players.</param>
        static void ReadInputFromFile(string path, List<string> malePlayerList, List<string> femalePlayerList)
        {
            StreamReader sr = new(path);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);

            string line = sr.ReadLine();

            while (line != null)
            {
                string[] player = line.Split(',');

                if (player[1] == "m" && !malePlayerList.Contains(player[0]))
                {
                    malePlayerList.Add(player[0]);
                }
                else
                {
                    if (player[1] == "f" && !femalePlayerList.Contains(player[0]))
                    {
                        femalePlayerList.Add(player[0]);
                    }
                }

                line = sr.ReadLine();
            }

            sr.Close();
        }

        /// <summary>
        /// Sorts a list of match results in descending order by percentage, then in alphabetical order by name.
        /// </summary>
        /// <param name="results"></param>
        static void SortResults(List<MatchResult?> results)
        {
            results.Sort((x, y) => {
                if (x.Value.percentage == y.Value.percentage)
                {
                    if (x.Value.name1 == y.Value.name1)
                    {
                        return x.Value.name2.CompareTo(y.Value.name2);
                    }
                    else
                    {
                        return x.Value.name1.CompareTo(y.Value.name1);
                    }
                }
                else
                {
                    return y.Value.percentage.CompareTo(x.Value.percentage);
                }
            });
        }

        /// <summary>
        /// Writes the match results to a text file named output.txt.
        /// </summary>
        /// <param name="results"></param>
        static void WriteResultsToFile(List<MatchResult?> results)
        {
            StreamWriter sw = new("output.txt");

            for (int i = 0; i < results.Count; i++)
            {
                sw.WriteLine(results[i].Value.resultMessage);
            }

            sw.Flush();
            sw.Close();
        }
    }
}
