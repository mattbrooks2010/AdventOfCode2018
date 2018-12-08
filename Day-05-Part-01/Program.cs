using System;
using System.Collections.Generic;

namespace AdventOfCode2018
{
    class Program
    {
        // cmd: dotnet run < input.txt
        // ps:  gc input.txt | dotnet run
        static void Main(string[] args)
        {
            var input = new List<char>(Console.ReadLine());

            for (var i = 0; i < input.Count - 1; i++)
            {
                var a = (byte)input[i];
                var b = (byte)input[i + 1];

                if ((a ^ b) == 32)
                {
                    input.RemoveRange(i, 2);
                    i -= 2;
                }
            }

            Console.WriteLine(input.Count);
        }
    }
}
