using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line =>
                line.ToList().Aggregate(
                    string.Empty,
                    (acc, ch) => string.IsNullOrEmpty(acc) ? 
                                            ch.ToString() 
                                            : acc + (ch==acc.Last() ? "" : ch.ToString())
                    )
            ).ToList()
            .ForEach(l => Console.WriteLine(l));
    }
}