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

            Console.WriteLine(PlayerMatcher.MatchPlayers(name1, name2));
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
                } else
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
    }
}
