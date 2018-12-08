using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    class Program
    {
        static int DATE_TIME_START_INDEX = 1;
        static int DATE_TIME_LENGTH = 16;
        static int MONTH_DAY_START_INDEX = 6;
        static int MONTH_DAY_LENGTH = 5;
        static int MINUTE_START_INDEX = 15;
        static int MINUTE_LENGTH = 2;

        // cmd: dotnet run < input.txt
        // ps:  gc input.txt | dotnet run
        static void Main(string[] args)
        {
            var input = ReadInput();

            // Sort input by date and time, which are already in a sortable string format
            input.Sort((a, b) => a.Substring(DATE_TIME_START_INDEX, DATE_TIME_LENGTH)
                .CompareTo(b.Substring(DATE_TIME_START_INDEX, DATE_TIME_LENGTH)));

            var allData = PopulateAllData(input);
            var totals = CalculateTotals(allData);
            var idMostAsleep = FindIdMostAsleep(totals);
            var sleepMinuteTotals = CalculateSleepMinuteTotals(allData, idMostAsleep);
            var minute = FindMinuteMostAsleep(sleepMinuteTotals);

            Console.WriteLine($"{idMostAsleep} * {minute} = {idMostAsleep * minute}");
        }

        static List<string> ReadInput()
        {
            var input = new List<string>();

            string line = null;

            while ((line = Console.ReadLine()) != null)
            {
                input.Add(line);
            }

            return input;
        }

        // Visual data structure:
        // 
        // Key        Minute array
        //            000000000011111111112222222222333333333344444444445555555555
        //            012345678901234567890123456789012345678901234567890123456789
        // 11-01 #10  .....####################.....#########################.....
        // 11-02 #99  ........................................##########..........
        // 11-03 #10  ........................#####...............................
        // 11-04 #99  ....................................##########..............
        // 11-05 #99  .............................................##########.....
        static Dictionary<string, bool[]> PopulateAllData(List<string> input)
        {
            var allData = new Dictionary<string, bool[]>();

            int id = 0;
            int sleep = 0;
            int wake = 0;
            string date = null;

            // Process the ordered input sequentially
            foreach (var line in input)
            {
                if (HasBegunShift(line))
                {
                    // When the line is 'begins shift' we have a new guard ID
                    id = ParseId(line);
                }
                else if (HasFallenAsleep(line))
                {
                    // When the line is 'falls asleep' we have a new date and new sleep start minute
                    date = ParseDate(line);
                    sleep = ParseMinute(line);
                }
                else
                {
                    // When the line is 'wakes up' we have a sleep end minute
                    wake = ParseMinute(line);

                    var key = $"{date} #{id}";

                    if (!allData.ContainsKey(key))
                    {
                        // Create a new entry in the data structure for this guard and date
                        allData[key] = new bool[60];
                    }

                    // Flag all minutes between sleep and wake time
                    for (var i = sleep; i < wake; i++)
                    {
                        allData[key][i] = true;
                    }
                }
            }

            return allData;
        }

        // Calculates the total minutes asleep, keyed by guard ID
        static Dictionary<int, int> CalculateTotals(Dictionary<string, bool[]> allData)
        {
            var totals = new Dictionary<int, int>();

            foreach (var entry in allData)
            {
                var id = ParseId(entry.Key);
                var minutes = 0;

                for (var i = 0; i < entry.Value.Length; i++)
                {
                    if (entry.Value[i])
                    {
                        minutes++;
                    }
                }

                if (!totals.ContainsKey(id))
                {
                    totals[id] = 0;
                }

                totals[id] += minutes;
            }

            return totals;
        }

        // Returns the ID of the guard that is asleep for the greatest amount of minutes
        static int FindIdMostAsleep(Dictionary<int, int> totals)
        {
            var idMostAsleep = 0;

            foreach (var id in totals.Keys)
            {
                if (idMostAsleep == 0 || totals[id] > totals[idMostAsleep])
                {
                    idMostAsleep = id;
                }
            }

            return idMostAsleep;
        }

        // Returns an array with an index representing each minute [0, .., 59]
        // and containing the number of times that minute was spent asleep
        static int[] CalculateSleepMinuteTotals(Dictionary<string, bool[]> allData, int idMostAsleep)
        {
            var minutes = new int[60];

            foreach (var key in allData.Keys)
            {
                var id = ParseId(key);

                if (id == idMostAsleep)
                {
                    var sleeps = allData[key];

                    for (var i = 0; i < sleeps.Length; i++)
                    {
                        if (sleeps[i])
                        {
                            minutes[i]++;
                        }
                    }
                }
            }

            return minutes;
        }

        // Returns the minute, 0 to 59, that was most often spent asleep
        static int FindMinuteMostAsleep(int[] sleepMinuteTotals)
        {
            var minute = 0;

            for (var i = 0; i < sleepMinuteTotals.Length; i++)
            {
                if (sleepMinuteTotals[i] > sleepMinuteTotals[minute])
                {
                    minute = i;
                }
            }

            return minute;
        }

        static bool HasBegunShift(string input)
        {
            return input.IndexOf("begins shift") >= 0;
        }

        static bool HasFallenAsleep(string input)
        {
            return input.IndexOf("falls asleep") >= 0;
        }

        static int ParseId(string input)
        {
            return int.Parse(Regex.Match(input, @"#(?<id>\d+)").Groups["id"].Value);
        }

        static string ParseDate(string input)
        {
            return input.Substring(MONTH_DAY_START_INDEX, MONTH_DAY_LENGTH);
        }

        static int ParseMinute(string input)
        {
            return int.Parse(input.Substring(MINUTE_START_INDEX, MINUTE_LENGTH));
        }
    }
}
