using System;
using System.IO;
using System.Linq;

namespace CodeEval225
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var left = line.Split('|')[0].Trim();
                    var right = line.Split('|')[1].Trim();
                    var diffs = left.ToList().Zip(
                        right.ToList(),
                        (a, b) => a != b
                        );
                    return diffs.Count(diff => diff);
                })
                .ToList()
                .ForEach(check => Console.WriteLine(ToPriority(check)));
        }

        private static string ToPriority(int check)
        {
            if (check == 0)
            {
                return "Done";
            }
            if (check <= 2)
            {
                return "Low";
            }
            if (check <= 4)
            {
                return "Medium";
            }
            if (check <= 6)
            {
                return "High";
            }
            return "Critical";
        }
    }
}