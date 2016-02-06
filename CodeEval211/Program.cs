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
            {
                var spl = line.Split('|');
                var vines = spl[0].Trim().Split(' ');
                var letters = spl[1].Trim().Select(c => c.ToString()).ToList();
                var possibleVines = vines
                    .Where(vine => letters.All(l => vine.Contains(l)));
                return possibleVines.Any() ? possibleVines
                                                    .Aggregate(
                                                        string.Empty, 
                                                        (seed, vine)=>string.IsNullOrEmpty(seed) ? vine : seed+" "+vine
                                                        )
                                                    : "False";
            }).ToList()
            .ForEach(l => Console.WriteLine(l));
    }
}