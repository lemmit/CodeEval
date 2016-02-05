using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace CodeEval65
{
    class Program
    {
        private static string[] Matrix =
        {
            "ABCE",
            "SFCS",
            "ADEE"
        };

        private static IEnumerable<Tuple<int, int, char>> ChainedMatrix()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix[0].Length; j++)
                {
                    yield return new Tuple<int, int, char>(i, j, Matrix[i][j]);
                }
            }
        }

        private static IEnumerable<Tuple<int, int, char>> _chained;
        static void Main(string[] args)
        {
            var input = (args.Length > 0 && !string.IsNullOrEmpty(args[0])) ? args[0] : "../../input.txt";
            _chained = ChainedMatrix().ToList();
            File.ReadAllLines(input)
                .Select(line => Check(line))
                .ToList()
                .ForEach(line => Console.WriteLine(line));
        }

        private static bool Check(string line)
        {
            var l = new List<Tuple<int, int, char>>();
            return Check(line, l);
        }

        private static bool Check(string line, List<Tuple<int, int, char>> elems)
        {
            //Console.WriteLine($"{line.PadLeft(15)} : {elems.Aggregate("", (seed,str) => seed +", " + str)}");
            if (line.Length > 0)
            {
                return _chained
                    .Where(elem => 
                        elem.Item3 == line[0] 
                        && (!elems.Any() || IsNear(elem, elems.Last()))
                        && !elems.Contains(elem))
                    .Any(
                        e=>Check(
                            line.Substring(1), 
                            new List<Tuple<int, int, char>>(elems).With(e)
                            )
                    );
            }
            return true;
        }

        private static bool IsNear(Tuple<int, int, char> elem, Tuple<int, int, char> nextElem)
        {
            return (Math.Abs(elem.Item1 - nextElem.Item1) <= 1 && Math.Abs(elem.Item2 - nextElem.Item2) == 0)
                   || 
                   (Math.Abs(elem.Item1 - nextElem.Item1) == 0 && Math.Abs(elem.Item2 - nextElem.Item2) <= 1);
        }
    }

    public static class ListExt
    {
        public static List<T> With<T>(this List<T> list, T elem)
        {
            var l = new List<T>(list);
            l.Add(elem);
            return l;
        } 
    }
}
