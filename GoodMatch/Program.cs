using System;
using MatchUpLibrary;

namespace GoodMatch
{
    internal class Program
    {
        static void Main(string[] args)
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
                string name1, name2;

                Console.WriteLine("Single input mode.\nPlease provide two player names...\n");

                Console.Write("Name 1: ");
                name1 = Console.ReadLine();

                Console.WriteLine();

                Console.Write("Name 2: ");
                name2 = Console.ReadLine();

                Console.WriteLine();

                Console.WriteLine(PlayerMatcher.MatchPlayers(name1, name2));
            } else
            {
                Console.WriteLine("Multi output mode.");
            }
        }
    }
}
