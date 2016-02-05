using System;
using System.Linq;
using System.IO;
using System.Numerics;

class Program
{
    static BigInteger[] fibs = new BigInteger[2000];
    static int upTo = 1;

    static BigInteger Fib(int n)
    {
        if (n <= upTo)
        {
            return fibs[n];
        }
        else
        {
            fibs[n] = Fib(n - 2) + Fib(n - 1);
            upTo = Math.Max(upTo, n);
            return fibs[n];
        }
    }
    static void Main(string[] args)
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