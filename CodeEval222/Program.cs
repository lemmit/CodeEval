using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Ext
{
    public static IEnumerable<T> Cycle<T>(this IEnumerable<T> collection)
    {
        foreach (var elem in collection)
        {
            yield return elem;
        }
        foreach (var elem in collection.Cycle())
        {
            yield return elem;
        }
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line =>
            {
                var pirates = line.Split('|')[0].Trim().Split(' ').ToList();
                var nr = int.Parse(line.Split('|')[1].Trim());
                while (pirates.Count > 1)
                {
                    var blacked = pirates.Cycle().Skip(nr-1).Take(1).Single();
                    pirates.Remove(blacked);
                }
                return pirates.Single();
            })
            .ToList()
            .ForEach(check => Console.WriteLine(check));
    }
}