using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeEval14
{
    public static class EnumerableExt
    {
        public static IEnumerable<T> RemoveAt<T>(this IEnumerable<T> source, int indexToRemove)
        {
            var index = 0;
            foreach (var item in source)
            {
                if (index != indexToRemove)
                {
                    yield return item;
                }
                index++;
            }
        }

        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> source)
        {
            var length = source.Count();
            if (length != 0)
            {
                var index = 0;
                foreach (var item in source)
                {
                    var allOtherItems = source.RemoveAt(index);
                    foreach (var permutation in allOtherItems.Permutations())
                    {
                        yield return new[] {item}.Concat(permutation);
                    }
                    index++;
                }
            }
            else
            {
                yield return new T[0];
            }
        }
    }

    internal class PermutationComparer : IComparer<string>
    {
        public int Compare(string xString, string yString)
        {
            for (var i = 0; i < xString.Length; i++)
            {
                var x = xString[i].ToString();
                var y = yString[i].ToString();

                int temp;
                var xIsInt = int.TryParse(x, out temp);
                var yIsInt = int.TryParse(y, out temp);
                var xIsUpperLetter = x == x.ToUpper();
                var yIsUpperLetter = y == y.ToUpper();
                if (xIsInt && !yIsInt) return -1;
                if (!xIsInt && yIsInt) return 1;
                if (xIsUpperLetter && !yIsUpperLetter) return -1;
                if (!xIsUpperLetter && yIsUpperLetter) return 1;
                var comp = x.CompareTo(y);
                if (comp != 0) return comp;
            }
            return 0;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 && !string.IsNullOrEmpty(args[0]) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(
                    line => line
                        .ToList()
                        .Permutations()
                        .Select(perm => perm
                            .Aggregate(string.Empty,
                                (seed, str) => seed + str)
                        )
                        .OrderBy(perm => perm, new PermutationComparer())
                        .Aggregate(string.Empty, (seed, str) => string.IsNullOrEmpty(seed) ? str : seed + "," + str)
                )
                .ToList()
                .ForEach(line => Console.WriteLine(line));
        }
    }
}