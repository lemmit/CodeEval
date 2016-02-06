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
                var splitted = line.Split(':');
                var array = splitted[0].Trim().Split(' ').ToArray();
                splitted[1].Trim().Split(',').ToList().ForEach(exch =>
                {
                    int from = int.Parse(exch.Split('-')[0].Trim());
                    int to = int.Parse(exch.Split('-')[1].Trim());
                    var temp = array[from];
                    array[from] = array[to];
                    array[to] = temp;
                });
                return array.Aggregate(
                        string.Empty, 
                        (seed, str) => 
                            string.IsNullOrEmpty(seed) ? str : seed + " " + str
                    );
            })
            .ToList()
            .ForEach(answ => Console.WriteLine(answ));
    }
}