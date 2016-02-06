using System;
using System.IO;
using System.Linq;

namespace CodeEval103
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var buckets = new int[10];
                    line.Split(' ').ToList().ForEach(el => buckets[int.Parse(el)]++);

                    try
                    {
                        return line.Split(' ').ToList().IndexOf(
                            Enumerable.Range(0, 10).Where(index => buckets[index] == 1).Min().ToString()
                            ) + 1;
                    }
                    catch (Exception)
                    {
                    }
                    return 0;
                })
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}