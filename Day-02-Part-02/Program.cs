using System;
using System.Collections.Generic;

namespace AdventOfCode2018
{
    class Program
    {
        // dotnet run < input.txt
        static void Main(string[] args)
        {
            var ids = new List<string>();

            string line = null;

            while ((line = Console.ReadLine()) != null)
            {
                ids.Add(line);
            }

            for (var i = 0; i < ids.Count; i++)
            {
                for (var j = 0; j < ids.Count; j++)
                {
                    var diff = 0;

                    for (var k = 0; k < ids[i].Length; k++)
                    {
                        if (ids[i][k] != ids[j][k])
                        {
                            diff++;
                        }

                        if (diff > 1)
                        {
                            break;
                        }
                    }

                    if (diff == 1)
                    {
                        for (var k = 0; k < ids[i].Length; k++)
                        {
                            if (ids[i][k] == ids[j][k])
                            {
                                Console.Write(ids[i][k]);
                            }
                        }

                        Console.WriteLine();

                        return;
                    }
                }
            }
        }
    }
}
