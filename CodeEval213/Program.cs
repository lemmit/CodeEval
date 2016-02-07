using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval213
{
    public static class Ext
    {
        public static Tuple<int, int> Find<T>(this T[,] matrix, T elem)
        {
            var w = matrix.GetLength(0);
            var h = matrix.GetLength(1);
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                   if(matrix[i, j].Equals(elem)) return new Tuple<int, int>(i, j);
                }
            }
            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var matrix = BuildMatrix(line);
                    return FindAndFloodFillLakes(matrix);
                })
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }

        private static int FindAndFloodFillLakes(char[,] matrix)
        {
            Tuple<int, int> lake;
            int lakes = 0;
            while ((lake = matrix.Find('o')) != null)
            {
                FloodFill(matrix, lake);
                lakes++;
            }
            return lakes;
        }

        private static void FloodFill(char[,] matrix, Tuple<int, int> lake)
        {
            if (lake.Item1 >= 0 && lake.Item1 < matrix.GetLength(0)
                && lake.Item2 >= 0 && lake.Item2 < matrix.GetLength(1))
            {
                if (matrix[lake.Item1, lake.Item2] == 'o')
                {
                    matrix[lake.Item1, lake.Item2] = 'X';
                    FloodFill(matrix, new Tuple<int, int>(lake.Item1 - 1, lake.Item2));
                    FloodFill(matrix, new Tuple<int, int>(lake.Item1 + 1, lake.Item2));
                    FloodFill(matrix, new Tuple<int, int>(lake.Item1, lake.Item2 - 1));
                    FloodFill(matrix, new Tuple<int, int>(lake.Item1, lake.Item2 + 1));
                    FloodFill(matrix, new Tuple<int, int>(lake.Item1 - 1, lake.Item2 - 1));
                    FloodFill(matrix, new Tuple<int, int>(lake.Item1 - 1, lake.Item2 + 1));
                    FloodFill(matrix, new Tuple<int, int>(lake.Item1 + 1, lake.Item2 + 1));
                    FloodFill(matrix, new Tuple<int, int>(lake.Item1 + 1, lake.Item2 - 1));
                }
            }
        }

        private static int Print(char[,] matrix)
        {
            var w = matrix.GetLength(0);
            var h = matrix.GetLength(1);
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            return 0;
        }

        private static char[,] BuildMatrix(string line)
        {
            var splitted = line.Split('|');
            var h = splitted.Length;
            var elems = splitted
                            .SelectMany(el => el.Trim().Split(' '))
                            .Select(e => e[0])
                            .ToArray();
            var w = elems.Length/h;
            var matrix = new char[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    matrix[i, j] = elems[i + j*w];
                }
            }
            return matrix;
        }
    }
}
