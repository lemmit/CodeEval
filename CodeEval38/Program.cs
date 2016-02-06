using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeEval14
{
    public static class EnumerableExt
    {
        public static IEnumerable<string> CombinationsWithRepetition(this IEnumerable<char> input, int length)
        {
            if (length <= 0)
                yield return "";
            else
            {
                foreach (var i in input)
                    foreach (var c in CombinationsWithRepetition(input, length - 1))
                        yield return i + c;
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 && !string.IsNullOrEmpty(args[0]) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(
                    line =>
                    {
                        var splitted = line
                            .Split(',');
                        return splitted[1].ToList()
                            .CombinationsWithRepetition(int.Parse(splitted[0]))
                            .Select(comb => comb
                                .Aggregate(string.Empty,
                                    (seed, str) => seed + str)
                            )
                            .OrderBy(perm => perm)
                            .Distinct()
                            .Aggregate(string.Empty, (seed, str) => string.IsNullOrEmpty(seed) ? str : seed + "," + str);
                    }).ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}