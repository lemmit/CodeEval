using System;
using System.IO;
using System.Linq;

namespace CodeEval97
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var splitted = line.Split('|').ToArray();
                    var letters = splitted[0].ToArray();
                    var hints = splitted[1]
                        .Substring(1)
                        .Split(' ')
                        .Select(hint => int.Parse(hint))
                        .ToList();
                    return hints.Select(hint => letters[hint - 1]).Aggregate(
                        string.Empty,
                        (seed, str) => string.IsNullOrEmpty(seed) ? str.ToString() : seed + str
                        );
                })
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}