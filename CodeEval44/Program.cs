using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeEval44
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
                        yield return new[] { item }.Concat(permutation);
                    }
                    index++;
                }
            }
            else
            {
                yield return new T[0];
            }
        }

        public static string SimpleAggregate(this IEnumerable<string> source)
        {
            return source.Aggregate(
                string.Empty,
                (acc, letter) => string.IsNullOrEmpty(acc) ? letter : acc + letter
                );
        }

        public static IEnumerable<IEnumerable<string>> ContinuousPermutations(this IEnumerable<string> source, string shouldAdd)
        {
            var sr = source.ToList();
            foreach (var perm in source.Permutations().OrderBy(p => p.SimpleAggregate()))
            {
                yield return perm;
            }
            sr.Add(shouldAdd);
            foreach (var perm in ContinuousPermutations(sr, shouldAdd))
            {
                yield return perm;
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 && !string.IsNullOrEmpty(args[0]) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    return line.Trim().ToList().Select(elem => elem.ToString())
                        .ContinuousPermutations("0")
                        .Select(perm =>  perm.SimpleAggregate())
                        .Where(permStr => permStr[0] != '0')
                        .SkipWhile(integer => integer != line.Trim())
                        .Distinct()
                        .Take(2)
                        .Last();
                })
                .ToList()
                .ForEach(substituted => Console.WriteLine(substituted));
        }
    }
}