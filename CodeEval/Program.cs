using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var input = (args.Length > 0 && !string.IsNullOrEmpty(args[0])) ? args[0] : "../../input.txt";
        File.ReadAllLines(input).Select(line =>
        {
            var splitted = line.Split(' ').Select(elem => int.Parse(elem)).ToArray();
            var X = splitted[0];
            var Y = splitted[1];
            var upTo = splitted[2];
            return Enumerable.Range(1, upTo).Select(iter =>
            {
                if (iter%X == 0 && iter%Y == 0)
                {
                    return "FB";
                }
                if (iter%X == 0)
                {
                    return "F";
                }
                if (iter%Y == 0)
                {
                    return "B";
                }
                return iter.ToString();
            }).Aggregate(string.Empty, (seed, str) => string.IsNullOrEmpty(seed) ? str : seed + " " + str); 
        }).ToList().ForEach(line =>
        {
           Console.WriteLine(line);
        });
    }
}
