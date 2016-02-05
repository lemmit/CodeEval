using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var input = (args.Length > 0) ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line => {
                var splitted = line.Split(',');
                var a = double.Parse(splitted[0]);
                var b = double.Parse(splitted[1]);
                return (int)Math.Pow((int)Math.Ceiling(Math.Log(a, b)), b);
            }).ToList()
            .ForEach(answ => Console.WriteLine(answ));
    }
}