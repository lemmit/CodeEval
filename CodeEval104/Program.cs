using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

internal class Program
{
    private static readonly IDictionary<string, int> _dict = new Dictionary<string, int>
    {
        {"zero", 0},
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9}
    };

    private static void Main(string[] args)
    {
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line =>
                ConvertToNumber(line.Split(';'))
            )
            .ToList()
            .ForEach(answ => Console.WriteLine(answ));
    }

    private static string ConvertToNumber(string[] digits)
    {
        var sb = new StringBuilder();
        foreach (var digit in digits)
        {
            sb.Append(_dict[digit]);
        }
        return sb.ToString();
    }
}