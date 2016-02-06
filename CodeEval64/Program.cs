using System;
using System.IO;
using System.Linq;
using System.Numerics;

internal class Program
{
    private static readonly BigInteger[] fibs = new BigInteger[2000];
    private static int upTo = 1;

    private static BigInteger Fib(int n)
    {
        if (n <= upTo)
        {
            return fibs[n];
        }
        fibs[n] = Fib(n - 2) + Fib(n - 1);
        upTo = Math.Max(upTo, n);
        return fibs[n];
    }

    private static void Main(string[] args)
    {
        fibs[0] = 0;
        fibs[1] = 1;
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line => Fib(int.Parse(line) + 1))
            .ToList()
            .ForEach(l => Console.WriteLine(l));
    }
}