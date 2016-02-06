using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval92
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = (args.Length > 0) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var splitted = line.Split(' ');
                    return splitted[splitted.Length - 2];
                })
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}
