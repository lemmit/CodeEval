using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeEval39
{
    class Program
    {
        private static int SumSquaredDigits(int n)
        {
            if (n == 0) return 0;
            return ((n % 10)* (n % 10)) + SumSquaredDigits(n / 10);
        }

        static void Main(string[] args)
        {
            var input = (args.Length > 0 && !string.IsNullOrEmpty(args[0])) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var number = int.Parse(line);
                    var nextNumber = number;
                    var before = new List<int>();
                    for (;;)
                    {
                        //Console.WriteLine("\t[LOG] " + nextNumber);
                        nextNumber = SumSquaredDigits(nextNumber);
                        if (before.Contains(nextNumber)) return "0";
                        before.Add(nextNumber);
                        if (nextNumber == 1) return "1";
                    }
                })
                .ToList()
                .ForEach(substituted => Console.WriteLine(substituted));
        }
    }
}
