using System;
using System.IO;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line =>
            {
                var columnNr = int.Parse(line);
                string column = "";
                while (columnNr > 0)
                {
                    var mod = (columnNr - 1)%26;
                    column += ((char)(mod+'A'));
                    columnNr = (columnNr - mod)/26;
                }
                return column.Reverse().Aggregate("", (s,e) => s=="" ? e+"" : s+e);
            })
            .ToList()
            .ForEach(Console.WriteLine);
    }
}