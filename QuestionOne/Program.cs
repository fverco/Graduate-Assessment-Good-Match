using System;
using System.Collections.Generic;
using MatchUpLibrary;

namespace QuestionOne
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name1, name2;

            Console.WriteLine("Hello! Let's get started.\n");
            
            Console.Write("Name 1: ");
            name1 = Console.ReadLine();

            Console.WriteLine();

            Console.Write("Name 2: ");
            name2 = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine(PlayerMatcher.MatchPlayers(name1, name2));
        }
    }
}
