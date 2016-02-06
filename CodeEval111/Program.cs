using System;
using System.Linq;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var input = (args.Length > 0) ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line =>
            {
                var splitted = line.Split(' ');
                return splitted.First(el => el.Length == splitted.Max(elem => elem.Length));
            })
            .ToList()
            .ForEach(answ => Console.WriteLine(answ));
    }
}