using System;
using System.Collections.Generic;

namespace AdventOfCode2018
{
    class Program
    {
        // dotnet run < input.txt
        static void Main(string[] args)
        {
            var pairCount = 0;
            var tripletCount = 0;

            string line = null;

            while ((line = Console.ReadLine()) != null)
            {
                var info = GetBoxIdInfo(line);

                if (info.HasPair)
                {
                    pairCount++;
                }

                if (info.HasTriplet)
                {
                    tripletCount++;
                }
            }

            Console.WriteLine(pairCount);
            Console.WriteLine(tripletCount);
            Console.WriteLine(pairCount * tripletCount);
        }

        static BoxIdInfo GetBoxIdInfo(string line)
        {
            var letterCounts = new Dictionary<char, int>();

            foreach (var letter in line)
            {
                if (letterCounts.ContainsKey(letter))
                {
                    letterCounts[letter] += 1;
                }
                else
                {
                    letterCounts[letter] = 1;
                }
            }

            var info = new BoxIdInfo();

            foreach (var entry in letterCounts)
            {
                if (entry.Value == 2)
                {
                    info.HasPair = true;
                }

                if (entry.Value == 3)
                {
                    info.HasTriplet = true;
                }
            }

            return info;
        }
    }

    struct BoxIdInfo
    {
        public bool HasPair { get; set; }
        public bool HasTriplet { get; set; }
    }
}
