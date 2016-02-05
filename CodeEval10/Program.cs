using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var input = (args.Length > 0) ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line => {
                var splitted = line.Split(' ').ToList();
                var n = int.Parse(splitted.Last());
                var index = splitted.Count - 1 - n;
                return index < 0 ? string.Empty : splitted[index];
            })
            .Where(nToTheLastAndEmpties => !string.IsNullOrEmpty(nToTheLastAndEmpties))
            .ToList()
            .ForEach(nToTheLast => Console.WriteLine(nToTheLast));
    }
}