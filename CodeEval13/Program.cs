using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval13
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var splitted = line.Split(',');
                    return splitted[0]
                        .ToList()
                        .Where(ch => !splitted[1].Trim().ToList().Contains(ch))
                        .Select(c => c.ToString())
                        .Aggregate("", (a, s) => a == "" ? s : a + s);
                }).ToList().ForEach(l => Console.WriteLine(l));
        }
    }
}
