using System;
using System.IO;
using System.Linq;

namespace CodeEval50
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 && !string.IsNullOrEmpty(args[0]) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var buckets = new int[10];
                    var nrs = line.ToList().Select(c => int.Parse(c.ToString()));
                    nrs.ToList().ForEach(nr => buckets[nr]++);
                    return nrs.Zip(buckets, (bucket, nr) => bucket == nr).All(x => x) ? "1" : "0";
                })
                .ToList()
                .ForEach(isSelfDescr => Console.WriteLine(isSelfDescr));
        }
    }
}