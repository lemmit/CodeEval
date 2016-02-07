using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval43
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var splitted = line.Split(' ').Skip(1).ToArray();
                    if (splitted.Length == 1) return "Jolly";
                    var diffs = splitted
                        .Select(val => int.Parse(val))
                        .Aggregate(new {Diffs = new List<int>(), Last = (int?) null}, (acc, elem) =>
                        {
                            if (acc.Last != null)
                            {
                                acc.Diffs.Add(Math.Abs((int)(acc.Last - elem)));
                                return new { Diffs = acc.Diffs, Last = (int?)elem };
                            }
                            return new { acc.Diffs, Last = (int?) elem};
                        })
                        .Diffs
                        .OrderBy(el => el)
                        .Distinct();
                    return Enumerable
                        .Range(1, splitted.Length - 1)
                        .Zip(diffs, (iter, diff) => iter == diff)
                        .All(a => a)
                        ? "Jolly"
                        : "Not jolly";
                })
                .ToList().ForEach(l => Console.WriteLine(l));
        }
    }
}
