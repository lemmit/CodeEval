using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval96
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = (args.Length > 0) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                    line
                    .ToList()
                    .Select(c => c.ToString())
                    .Select(ch => ch.ToUpper() == ch ? ch.ToLower() : ch.ToUpper() )
                    .Aggregate(string.Empty, (seed, str)=>string.IsNullOrEmpty(seed) ? str : seed+str)
                )
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}
