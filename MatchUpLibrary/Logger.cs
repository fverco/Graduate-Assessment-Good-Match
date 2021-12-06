using System;
using System.IO;

namespace MatchUpLibrary
{
    public class Logger
    {
        static StreamWriter StartWriter()
        {
            StreamWriter sw = new("logs.txt", append: true);
            return sw;
        }

        public static void LogAppStart()
        {
            StreamWriter sw = StartWriter();

            sw.WriteLine($"Application started at: {DateTimeOffset.Now}");

            sw.Flush();
            sw.Close();
        }

        public static void LogAppFinish()
        {
            StreamWriter sw = StartWriter();

            sw.WriteLine($"Application successfully finished at: {DateTimeOffset.Now}");

            sw.Flush();
            sw.Close();
        }

        public static void LogInvalidInput(in string input)
        {
            StreamWriter sw = StartWriter();

            sw.WriteLine($"Invalid input ignored: {input} ({DateTimeOffset.Now})");

            sw.Flush();
            sw.Close();
        }

        public static void LogMatchPair()
        {
            StreamWriter sw = StartWriter();

            sw.WriteLine($"Successfully matched pair at: {DateTimeOffset.Now}");

            sw.Flush();
            sw.Close();
        }

        public static void LogMatchGroups()
        {
            StreamWriter sw = StartWriter();

            sw.WriteLine($"Successfully matched groups at: {DateTimeOffset.Now}");

            sw.Flush();
            sw.Close();
        }

        public static void LogFileDontExist(in string path)
        {
            StreamWriter sw = StartWriter();

            sw.WriteLine($"File at ({path}) don't exist! ({DateTimeOffset.Now})");

            sw.Flush();
            sw.Close();
        }

        public static void LogFileNotCsv(in string path)
        {
            StreamWriter sw = StartWriter();

            sw.WriteLine($"File at ({path}) is not a csv file! ({DateTimeOffset.Now})");

            sw.Flush();
            sw.Close();
        }

        public static void LogReadFromFile(in string path)
        {
            StreamWriter sw = StartWriter();

            sw.WriteLine($"Read from file ({path}) at {DateTimeOffset.Now}");

            sw.Flush();
            sw.Close();
        }

        public static void LogWriteToFile(in string path)
        {
            StreamWriter sw = StartWriter();

            sw.WriteLine($"Write to file ({path}) at {DateTimeOffset.Now}");

            sw.Flush();
            sw.Close();
        }
    }
}
