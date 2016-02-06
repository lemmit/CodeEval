using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeEval199
{
    public static class Ext
    {
        public static int FindIndex<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            var index = 0;
            foreach (var elem in collection)
            {
                if (predicate(elem))
                    return index;
                index++;
            }
            return -1;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var splitted = line.Split(';').ToArray();
                    var words = splitted[0].Split(' ').ToArray();
                    var hints = splitted[1].Split(' ').Select(hint => int.Parse(hint)).ToList();
                    var outwords = new string[words.Length];
                    for (var i = 0; i < hints.Count; i++)
                    {
                        outwords[hints[i] - 1] = words[i];
                        words[i] = string.Empty;
                    }
                    var index = outwords.FindIndex(elem => string.IsNullOrEmpty(elem));
                    outwords[index] = words.Single(str => !string.IsNullOrEmpty(str));
                    return outwords
                        .Aggregate(
                            string.Empty,
                            (seed, str) => string.IsNullOrEmpty(seed) ? str : seed + " " + str
                        );
                })
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}