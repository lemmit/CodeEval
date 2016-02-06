using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeEval82
{
    class Program
    {
        private static int SumPoweredDigits(int n, int pow)
        {
            if (n == 0) return 0;
            return ((int)Math.Pow(n % 10, pow)) + SumPoweredDigits(n / 10, pow);
        }

        static void Main(string[] args)
        {
            var input = (args.Length > 0 && !string.IsNullOrEmpty(args[0])) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var number = int.Parse(line);
                    var candidate = SumPoweredDigits(number, (int)Math.Log10(number)+1);
                    return number == candidate;
                })
                .ToList()
                .ForEach(substituted => Console.WriteLine(substituted));
        }
    }
}
