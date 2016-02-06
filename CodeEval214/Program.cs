using System;
using System.IO;
using System.Linq;

namespace CodeEva214
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    return line
                        .Split(' ')
                        //.Select(timestamp => DateTime.Parse(timestamp))
                        .OrderByDescending(timestamp => timestamp)
                        .Aggregate(
                            string.Empty,
                            (seed, str) => string.IsNullOrEmpty(seed) ? str.ToString() : seed + " " + str
                        );
                })
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}