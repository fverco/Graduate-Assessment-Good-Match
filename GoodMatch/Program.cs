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

            int percentage = PlayerMatcher.MatchPlayers(name1, name2);

            if (percentage != -1)
            {
                Console.WriteLine(OutputPercentageMessage(name1, name2, percentage));
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

            StreamReader sr = new(path);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);

            List<string> malePlayerList = new();
            List<string> femalePlayerList = new();
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

            List<MatchResult> results = new();

            foreach (string male in malePlayerList)
            {
                foreach (string female in femalePlayerList)
                {
                    MatchResult matchResult = new();
                    int percentage = PlayerMatcher.MatchPlayers(male, female);
                    matchResult.name1 = male;
                    matchResult.name2 = female;
                    matchResult.percentage = percentage;
                    matchResult.resultMessage = OutputPercentageMessage(male, female, percentage);

                    results.Add(matchResult);
                }
            }

            results.Sort((x, y) => {
                if (x.percentage == y.percentage)
                {
                    if (x.name1 == y.name1)
                    {
                        return x.name2.CompareTo(y.name2);
                    }
                    else
                    {
                        return x.name1.CompareTo(y.name1);
                    }
                }
                else
                {
                    return y.percentage.CompareTo(x.percentage);
                }
            });

            StreamWriter sw = new("output.txt");

            for (int i = 0; i < results.Count; i++)
            {
                sw.WriteLine(results[i].resultMessage);
            }

            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Creates a message for the user about the match percentage between two players.
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <param name="matchPercentage"></param>
        /// <returns>A string message</returns>
        static string OutputPercentageMessage(in string name1, in string name2, in int matchPercentage)
        {
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
    }
}
