using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

internal class Program
{
    private static IDictionary<string, string> _codes
        = new Dictionary<string, string>()
        {
            {".-", "A"},
            {"-...", "B"},
            {"-.-.", "C"},
            {"-..", "D"},
            {".", "E"},
            {"..-.", "F"},
            {"--.", "G"},
            {"....", "H"},
            {"..", "I"},
            {".---", "J"},
            {"-.-", "K"},
            {".-..", "L"},
            {"--", "M"},
            {"-.", "N"},
            {"---", "O"},
            {".--.", "P"},
            {"--.-", "Q"},
            {".-.", "R"},
            {"...", "S"},
            {"-", "T"},
            {"..-", "U"},
            {"...-", "V"},
            {".--", "W"},
            {"-..-", "X"},
            {"-.--", "Y"},
            {"--..", "Z"},

            {".----", "1"},
            {"..---", "2"},
            {"...--", "3"},
            {"....-", "4"},
            {".....", "5"},
            {"-....", "6"},
            {"--...", "7"},
            {"---..", "8"},
            {"----.", "9"},
            {"-----", "0"},

            {"", " "},
        };
    private static void Main(string[] args)
    {
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line =>
                line.Split(' ')
                    .Select(code => _codes[code])
                    .Aggregate(
                        string.Empty, 
                        (seed, str) => string.IsNullOrEmpty(seed) ? str : seed + str
                    )
            )
            .ToList()
            .ForEach(answ => Console.WriteLine(answ));
    }
}