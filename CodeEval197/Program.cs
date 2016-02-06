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
                var columnNr = int.Parse(line) - 1;
                var column = "";
                var first = true;
                while (columnNr > 0 || first)
                {
                    first = false;
                    var m = 26;
                    column += ((char) ('A' + (columnNr - 1)%m)).ToString();
                    columnNr = columnNr/m;
                }
                //adjust last
                column = (char) (column.First() + 1) + column.Substring(1);
                return column.Reverse().Aggregate(string.Empty, (seed, str) => seed + str);
            })
            .ToList()
            .ForEach(column => Console.WriteLine(column));
    }
}