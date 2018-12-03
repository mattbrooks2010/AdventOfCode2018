using System;

namespace AdventOfCode2018
{
    class Program
    {
        // dotnet run < input.txt
        static void Main(string[] args)
        {
            var frequency = 0;
            string line = null;

            while ((line = Console.ReadLine()) != null)
            {
                frequency += int.Parse(line);
            }

            Console.WriteLine(frequency);
        }
    }
}
