using System;
using System.Linq;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        
        var input = (args.Length > 0) ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line => {
                var splitted = line.Split(',').Select(elem => int.Parse(elem)).ToArray();
                var first = ((splitted[0] >> (splitted[1] - 1)) & 1);
                var second = ((splitted[0] >> (splitted[2] - 1)) & 1);
                return (first ^ second) == 0;
            })
            .ToList()
            .ForEach(answ => Console.WriteLine(answ.ToString().ToLower()));
    }
}