using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeEval203
{
    public static class Ext
    {
        public static IEnumerable<int> FindAllIndexesOf(this string str, string substr)
        {
            var foundIndex = str.IndexOf(substr);
            while (foundIndex != -1)
            {
                yield return foundIndex;
                str = str.Substring(foundIndex + 1);
                foundIndex = str.IndexOf(substr);
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                    line.FindAllIndexesOf(">>-->").Count()
                    + line.FindAllIndexesOf("<--<<").Count()
                )
                .ToList()
                .ForEach(cleanedUpLine => Console.WriteLine(cleanedUpLine));
        }
    }
}