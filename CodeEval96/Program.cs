using System;
using System.IO;
using System.Linq;

namespace CodeEval96
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                    line
                        .ToList()
                        .Select(c => c.ToString())
                        .Select(ch => ch.ToUpper() == ch ? ch.ToLower() : ch.ToUpper())
                        .Aggregate(string.Empty, (seed, str) => string.IsNullOrEmpty(seed) ? str : seed + str)
                )
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}