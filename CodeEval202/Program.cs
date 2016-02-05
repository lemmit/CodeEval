using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval202
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = (args.Length > 0) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .ToList()
                .ForEach(line =>
                {
                    var splitted = line.Split(' ');
                    var maxLen = splitted.Max(word => word.Length);
                    var longest = splitted.First(elem => elem.Length == maxLen);
                    for (int i = 0; i < maxLen; i++)
                    {
                        Console.Write(longest[i].ToString().PadLeft(i+1, '*'));
                        if(i!=maxLen-1) Console.Write(' ');
                    }
                    Console.WriteLine();
                });
        }
    }
}
