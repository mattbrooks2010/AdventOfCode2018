using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    class Program
    {
        static readonly Regex ClaimPattern = new Regex(@"^#\d+ @ (?<x>\d+),(?<y>\d+): (?<w>\d+)x(?<h>\d+)$");

        // dotnet run < input.txt
        static void Main(string[] args)
        {
            var claims = new List<Rectangle>();

            string line = null;

            while ((line = Console.ReadLine()) != null)
            {
                var claim = ParseClaim(line);
                claims.Add(claim);
            }

            var overlapping = new HashSet<Point>();

            // Compare each pair of claims exactly once
            for (var i = 0; i < claims.Count; i++)
            {
                for (var j = i + 1; j < claims.Count; j++)
                {
                    // Calculate any intersection, i.e. any overlap
                    var overlap = Rectangle.Intersect(claims[i], claims[j]);

                    // Derive the all the X and Y coordinates of the overlap
                    for (var k = 0; k < overlap.Width; k++)
                    {
                        var x = k + overlap.X;

                        for (var l = 0; l < overlap.Height; l++)
                        {
                            var y = l + overlap.Y;

                            // Track unique points (i.e. squares) only
                            overlapping.Add(new Point(x, y));
                        }
                    }
                }
            }

            Console.WriteLine(overlapping.Count);
        }

        static Rectangle ParseClaim(string claim)
        {
            var match = ClaimPattern.Match(claim);

            return new Rectangle(
                int.Parse(match.Groups["x"].Value),
                int.Parse(match.Groups["y"].Value),
                int.Parse(match.Groups["w"].Value),
                int.Parse(match.Groups["h"].Value));
        }
    }
}
