using System;
using System.Collections.Generic;

namespace AdventOfCode2018
{
    class Program
    {
        // dotnet run < input.txt
        static void Main(string[] args)
        {
            var changes = new List<int>();
            string line = null;

            while ((line = Console.ReadLine()) != null)
            {
                changes.Add(int.Parse(line));
            }

            var frequency = 0;

            var frequencies = new HashSet<int>();
            frequencies.Add(frequency); // Add starting frequency

            for (var i = 0; ; i++)
            {
                if (i >= changes.Count)
                {
                    // Reached the end of the input, so start from the beginning
                    i = 0;
                }

                frequency += changes[i];

                if (frequencies.Contains(frequency))
                {
                    // Reached first frequency twice, so stop
                    break;
                }

                frequencies.Add(frequency);
            }

            Console.WriteLine(frequency);
        }
    }
}
