using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval200
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                //deserialize
                .Select(unparsedLineWithSeparators =>
                    unparsedLineWithSeparators.Split('|').SelectMany(col => col.Split(' '))
                                .Where(e => !string.IsNullOrEmpty(e))
                                .Select(elem => int.Parse(elem))
                                .Deserialize()
                )
                //perform sorting by column
                .Select(matrix =>
                        Enumerable
                            .Range(0, matrix.GetLength(0))
                            .Select(columnIndex => matrix.GetColumn(columnIndex))
                            .OrderBy(x => x, new SequenceComparer<int>())
                            .ToArray().To2DArray()
                            .Transpose()
                )
                .ToList()
                //print
                .ForEach(sortedByColumnMatrix => PrintArray(sortedByColumnMatrix.Serialize()));
        }

        private static void PrintArray(IEnumerable<int> serializedArray)
        {
            var arr = serializedArray.ToArray();
            var r = (int) Math.Sqrt(arr.Length);
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    Console.Write(arr[j + i*r]);
                    if (j != r - 1)
                        Console.Write(" ");
                }
                if (i != r - 1)
                    Console.Write(" | ");
            }
            Console.WriteLine();
        }
    }
    
    public static class ArrayExtensions
    {
        public static T[,] Deserialize<T>(this IEnumerable<T> values)
        {
            var vals = values.ToList();
            var rank = (int)Math.Sqrt(vals.Count);
            var matrix = new T[rank, rank];
            for (var i = 0; i < rank; i++)
            {
                for (var j = 0; j < rank; j++)
                {
                    matrix[i, j] = vals[i * rank + j];
                }
            }
            return matrix;
        }

        public static T[,] To2DArray<T>(this T[][] matrix)
        {
            var rank = matrix.GetLength(0);
            var matrix2d = new T[rank, rank];
            for (var i = 0; i < rank; i++)
            {
                for (var j = 0; j < rank; j++)
                {
                    matrix2d[i, j] = matrix[i][j];
                }
            }
            return matrix2d;
        }

        public static T[,] Transpose<T>(this T[,] matrix)
        {
            var rank = matrix.GetLength(0);
            var transposed = new T[rank, rank];
            for (var i = 0; i < rank; i++)
            {
                for (var j = 0; j < rank; j++)
                {
                    transposed[j, i] = matrix[i,j];
                }
            }
            return transposed;
        }

        public static IEnumerable<T> Serialize<T>(this T[,] matrix)
        {
            var rank = matrix.GetLength(0);
            for (var i = 0; i < rank; i++)
            {
                for (var j = 0; j < rank; j++)
                {
                    yield return matrix[i,j];
                }
            }
        }

        public static T[] GetColumn<T>(this T[,] matrix, int column)
        {
            var rows = matrix.GetLength(0);
            var array = new T[rows];
            for (var i = 0; i < rows; ++i)
                array[i] = matrix[i, column];
            return array;
        }
    }

    public class SequenceComparer<T> : IComparer<IEnumerable<T>>
    {
        public int Compare(IEnumerable<T> x, IEnumerable<T> y)
        {
            return x.Compare(y);
        }
    }

    public static class EnumerableExtensions
    {
        public static int Compare<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null || second == null)
                return Comparer.Default.Compare(first, second);

            var elementComparer = Comparer<T>.Default;
            int compareResult;

            using (var firstEnum = first.GetEnumerator())
            using (var secondEnum = second.GetEnumerator())
            {
                do
                {
                    var gotFirst = firstEnum.MoveNext();
                    var gotSecond = secondEnum.MoveNext();

                    // end of collection => equal
                    if (!gotFirst && !gotSecond)
                        return 0;

                    // larger is greater
                    if (gotFirst != gotSecond)
                        return gotFirst ? 1 : -1;

                    compareResult = elementComparer.Compare(firstEnum.Current, secondEnum.Current);
                } while (compareResult == 0);
            }

            return compareResult;
        }
    }
}
