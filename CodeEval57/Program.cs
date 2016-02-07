using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval57
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    //Console.WriteLine(line);
                    var matrix = BuildMatrix(line);
                    return Spiralize(matrix).Aggregate("", (a, e) => a == "" ? e : a + " " + e);
                })
                .ToList()
                .ForEach(el => Console.WriteLine(el));
        }

        private static IEnumerable<string> Spiralize(string[,] matrix)
        {
            int width = matrix.GetLength(1);
            int height = matrix.GetLength(0);
            int startRowIndex = 0, startColumnIndex = 0;
            while (startRowIndex < height && startColumnIndex < width)
            {
                for (int i = startColumnIndex; i < width; ++i)
                {
                    yield return matrix[startRowIndex, i];
                }
                startRowIndex++;

                for (int i = startRowIndex; i < height; ++i)
                {
                    yield return matrix[i, width - 1];
                }
                width--;

                if (startRowIndex < height)
                {
                    for (int i = width - 1; i >= startColumnIndex; --i)
                    {
                        yield return matrix[height - 1, i];
                    }
                    height--;
                }

                if (startColumnIndex < width)
                {
                    for (int i = height - 1; i >= startRowIndex; --i)
                    {
                        yield return matrix[i, startColumnIndex];
                    }
                    startColumnIndex++;
                }
            }
        }

        private static string[,] BuildMatrix(string line)
        {
            var splitted = line.Split(';');
            var elems = splitted[2].Split(' ');
            var h = int.Parse(splitted[0]);
            var w = int.Parse(splitted[1]);
            
            var matrix = new string[h, w];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    matrix[j, i] = elems[i + j * w];
                }
            }
            return matrix;
        }
    }
}
