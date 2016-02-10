using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval113
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var splitted = line.Split('|');
                    var left = splitted[0].Trim().Split(' ').Select(int.Parse);
                    var right = splitted[1].Trim().Split(' ').Select(int.Parse);
                    return left.Zip(right, (l, r) => l*r)
                        .Aggregate("", (s, e)=> s=="" ? e+"" : s+" "+e);
                }).ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
