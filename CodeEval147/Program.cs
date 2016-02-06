using System;
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
                var chars = line
                    .ToList();
                var upperChars = chars.Select(c => c.ToString().ToUpper() == c.ToString())
                    .Count(upper => upper);
                return ((double) upperChars/(double) chars.Count())*100.0f;
            })
            .ToList()
            .ForEach(answ => Console.WriteLine($"lowercase: {Math.Round(100.0f-answ,2), 0:0.00} uppercase: {Math.Round(answ,2), 0:0.00}"));
    }
}