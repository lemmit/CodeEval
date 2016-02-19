using System;
using System.IO;
using System.Linq;

namespace CodeEval6
{
    class Program
    {
        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[j, i]);
                }
                Console.WriteLine();
            }
        }
        private static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var splitted = line.Split(';');
                    var subject = splitted[0].ToArray();
                    var n = subject.Length;
                    var search = splitted[1].ToArray();
                    var m = search.Length;
                    var matrix = new int[n+1, m+1];
                    for (int i = 1; i <= n; i++)
                    {
                        for (int j = 1; j <= m; j++)
                        {
                            if (subject[i - 1] == search[j - 1])
                            {
                                matrix[i, j] = matrix[i - 1, j - 1] + 1;
                            }
                            else matrix[i, j] = Math.Max(matrix[i - 1, j], matrix[i, j - 1]);
                        }
                    }
                    //PrintMatrix(matrix);
                    int x = n, y = m;
                    var answ = "";
                    while (x != 0 && y != 0)
                    {
                        if (matrix[x - 1, y] == matrix[x, y]) x--;
                        else if (matrix[x, y - 1] == matrix[x, y]) y--;
                        else
                        {
                            answ = subject[x-1] + answ;
                            x--;
                            y--;
                        }

                    }
                    return answ;
                })
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
