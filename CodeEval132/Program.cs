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
            .Select(line => FindMajor(line))
            .ToList()
            .ForEach(answ => Console.WriteLine(answ));
    }

    private static string FindMajor(string line)
    {
        var splitted = line.Split(',').ToList();
        var grouped = splitted.GroupBy(x => x);
        var major = grouped.FirstOrDefault(elem => elem.Count() > splitted.Count/2);
        return major?.Key ?? "None";
    }
}