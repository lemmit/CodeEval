using System;
using System.IO;
using System.Linq;

namespace CodeEval92
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var splitted = line.Split(' ');
                    return splitted[splitted.Length - 2];
                })
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}