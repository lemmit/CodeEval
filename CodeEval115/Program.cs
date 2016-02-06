using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

internal class Program
{
    
    private static void Main(string[] args)
    {
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line =>
            {
                var temp = 0;
                var splitted = line.Split(',');
                var fruits = splitted
                                .Where(elem => !int.TryParse(elem, out temp))
                                .Aggregate("", (seed, str)=>string.IsNullOrEmpty(seed) ? str : seed+","+str)
                                ;
                var numbers = splitted
                                .Where(elem => int.TryParse(elem, out temp))
                                .Aggregate("", (seed, str) => string.IsNullOrEmpty(seed) ? str : seed + "," + str)
                                ;
                if (string.IsNullOrEmpty(fruits)) return numbers;
                if (string.IsNullOrEmpty(numbers)) return fruits;
                return fruits + "|" + numbers;
            })
            .ToList()
            .ForEach(answ => Console.WriteLine(answ));
    }
}