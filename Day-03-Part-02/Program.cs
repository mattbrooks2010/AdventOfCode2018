using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    class Program
    {
        // dotnet run < input.txt
        static void Main(string[] args)
        {
            var claims = new List<Claim>();

            string line = null;

            while ((line = Console.ReadLine()) != null)
            {
                var claim = Claim.Parse(line);
                claims.Add(claim);
            }

            // Compare every claim to every other claim
            for (var i = 0; i < claims.Count; i++)
            {
                // Assume this claim is the candidate
                var candidate = true;

                for (var j = 0; j < claims.Count; j++)
                {
                    // Don't compare a claim to itself
                    if (i == j)
                    {
                        continue;
                    }

                    var overlap = Rectangle.Intersect(claims[i].Rectangle, claims[j].Rectangle);

                    if (overlap.Width > 0 && overlap.Height > 0)
                    {
                        // Stop once we encounter an overlap
                        candidate = false;
                        break;
                    }
                }

                if (candidate)
                {
                    // The current claim does not overlap with any other claim
                    Console.WriteLine(claims[i].ID);

                    return;
                }
            }
        }
    }

    class Claim
    {
        static readonly Regex ClaimPattern = new Regex(@"^#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<w>\d+)x(?<h>\d+)$");

        public Claim(string id, Rectangle rectangle)
        {
            ID = id;
            Rectangle = rectangle;
        }

        public string ID { get; private set; }
        public Rectangle Rectangle { get; private set; }

        public static Claim Parse(string claim)
        {
            var match = ClaimPattern.Match(claim);

            return new Claim(
                match.Groups["id"].Value,
                new Rectangle(
                    int.Parse(match.Groups["x"].Value),
                    int.Parse(match.Groups["y"].Value),
                    int.Parse(match.Groups["w"].Value),
                    int.Parse(match.Groups["h"].Value)));
        }
    }
}
