using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace CodeEval51
{
    public static class EnumerableExt
    {
        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 && !string.IsNullOrEmpty(args[0]) ? args[0] : "../../input.txt";
            var lines = File.ReadAllLines(input);

            /* Set . instead of , as separator while printing floats */
            var customCulture = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;

            ToSets(lines)
                .Select(
                    set => set.Min(elem => set.Except(elem.ToEnumerable()).Min(setElem => Destination(elem, setElem))))
                .ToList()
                .ForEach(setMin =>
                {
                    var printedValue = setMin < 10000.0f ? string.Format("{0:F4}", setMin) : "INFINITY";
                    Console.WriteLine(printedValue);
                });
        }

        private static double Destination(Tuple<int, int> elem, Tuple<int, int> setElem)
        {
            return Math.Sqrt(Math.Pow((double)elem.Item1 - (double) setElem.Item1, 2) +
                                     Math.Pow((double)elem.Item2 - (double) setElem.Item2, 2));
        }

        private static IEnumerable<Tuple<int, int>[]> ToSets(string[] lines)
        {
            var setOffset = 0;
            while (setOffset < lines.Length)
            {
                var set = new List<Tuple<int, int>>();
                var N = intParse(lines[setOffset]);
                if (N == 0) break;
                setOffset++;
                for (var i = 0; i < N; i++)
                {
                    var splitted = lines[setOffset + i].Split(' ');
                    var tuple = new Tuple<int, int>(
                        intParse(splitted[0]),
                        intParse(splitted[1])
                        );
                    set.Add(tuple);
                }
                yield return set.ToArray();
                setOffset += N;
            }
        }

        private static int intParse(string intStr)
        {
            try
            {
                return int.Parse(intStr);
            }
            catch (Exception e)
            {
                throw new FormatException(intStr, e);
            }
        }
    }
}