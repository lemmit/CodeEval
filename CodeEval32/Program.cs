using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval32
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Where(inp => !string.IsNullOrEmpty(inp))
                .Select(line =>
                {
                    var splitted = line.Split(',');
                    var subject = splitted[0];
                    var trailing = splitted[1];
                    if (trailing.Length > subject.Length) return "0";
                    return subject.LastIndexOf(trailing) == subject.Length - trailing.Length
                        ? "1"
                        : "0";
                    
                    ;
                }).ToList().ForEach(l => Console.WriteLine(l));
        }
    }
}
