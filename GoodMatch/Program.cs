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
            Logger.LogAppStart();

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

            Logger.LogAppFinish();
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
                Logger.LogMatchPair();
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
            string path;

            Console.WriteLine("Multi input mode.");

            bool fileExists, pathIsCsv;
            do
            {
                Console.WriteLine("Please provide a valid path to a CSV file with all the players...");
                path = Console.ReadLine();

                fileExists = File.Exists(path);
                pathIsCsv = path.ToLower().EndsWith(".csv");

                if (!fileExists)
                {
                    Logger.LogFileDontExist(path);
                }
                else
                {
                    if (!pathIsCsv)
                    {
                        Logger.LogFileNotCsv(path);
                    }
                }
            }
            while (!fileExists || !pathIsCsv);

            List<string> malePlayerList = new();
            List<string> femalePlayerList = new();

            ReadInputFromFile(path, malePlayerList, femalePlayerList);

            bool invalidInputFound = false;
            List<MatchResult?> originalResults = MatchPlayerGroups(malePlayerList, femalePlayerList, ref invalidInputFound);
            List<MatchResult?> reverseResults = MatchPlayerGroups(femalePlayerList, malePlayerList, ref invalidInputFound);
            List<MatchResult?> averagesResults = CalculatePairAverages(originalResults, reverseResults);

            SortResults(originalResults);
            SortResults(averagesResults);

            WriteResultsToFile(originalResults, averagesResults);

            if (invalidInputFound)
            {
                Console.WriteLine("\nSome of the names were invalid and as a result they were ignored.");
            }

            Logger.LogMatchGroups();
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
            Logger.LogReadFromFile(path);

            string line = sr.ReadLine();

            while (line != null)
            {
                string[] player = line.Split(',');

                if (player.Length >= 2)
                {
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
                }
                else
                {
                    Logger.LogInvalidInput(line);
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
        /// Writes the match results and averages to a text file named output.txt.
        /// </summary>
        /// <param name="results">The match results of pairs of players.</param>
        /// <param name="averages">The average match results of pairs of players.</param>
        static void WriteResultsToFile(List<MatchResult?> results, List<MatchResult?> averages = null)
        {
            StreamWriter sw = new("output.txt");
            Logger.LogWriteToFile("output.txt");

            for (int i = 0; i < results.Count; i++)
            {
                sw.WriteLine(results[i].Value.resultMessage);
            }

            if (averages != null)
            {
                sw.WriteLine();

                for (int i = 0; i < averages.Count; i++)
                {
                    sw.WriteLine(averages[i].Value.resultMessage);
                }
            }

            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Matches each player from playerList1 with each player from playerList2. If an invalid name(s) is/are found, invalidInputFound will become true and those names will be ignored.
        /// </summary>
        /// <param name="playerList1"></param>
        /// <param name="playerList2"></param>
        /// <param name="invalidInputFound"></param>
        /// <returns>A list of match results between the two player lists.</returns>
        static List<MatchResult?> MatchPlayerGroups(List<string> playerList1, List<string> playerList2, ref bool invalidInputFound)
        {
            List<MatchResult?> results = new();

            foreach (string player1 in playerList1)
            {
                foreach (string player2 in playerList2)
                {
                    MatchResult? matchResult = PlayerMatcher.MatchPlayers(player1, player2);

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

            return results;
        }

        /// <summary>
        /// Calculates the average good match percentage for each pair from both lists of results.
        /// </summary>
        /// <param name="originalOrderResults"></param>
        /// <param name="resverseOrderResults"></param>
        /// <returns>A list of averages for each pair.</returns>
        static List<MatchResult?> CalculatePairAverages(List<MatchResult?> originalOrderResults, List<MatchResult?> reverseOrderResults)
        {
            List<MatchResult?> averages = new();

            for (int i = 0; i < originalOrderResults.Count; i++)
            {
                int j = 0;
                while (reverseOrderResults[j].Value.name1 != originalOrderResults[i].Value.name2 ||
                        reverseOrderResults[j].Value.name2 != originalOrderResults[i].Value.name1)
                {
                    ++j;
                }

                MatchResult average = new();
                average.name1 = originalOrderResults[i].Value.name1;
                average.name2 = originalOrderResults[i].Value.name2;
                average.percentage = (originalOrderResults[i].Value.percentage +
                                        reverseOrderResults[j].Value.percentage) / 2;
                average.resultMessage = $"The average match score for {average.name1} and {average.name2} is {average.percentage}%";

                averages.Add(average);
            }

            return averages;
        }
    }
}
