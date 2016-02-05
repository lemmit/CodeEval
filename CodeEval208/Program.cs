using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

class Program
{

    public static T[] GetRow<T>(T[,] matrix, int row)
    {
        var columns = matrix.GetLength(1);
        var array = new T[columns];
        for (int i = 0; i < columns; ++i)
            array[i] = matrix[row, i];
        return array;
    }

    static int[,] Deserialize(IEnumerable<int> values, int rows)
    {
        var vals = values.ToList();
        var cols = vals.Count/rows;
        var matrix = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = vals[i * cols + j];
            }
        }
        return matrix;
    }

    static int[,] Transpose(int[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var transposed = new int[cols, rows];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                transposed[j, (rows - 1) - i] = matrix[i, j];
            }
        }
        return transposed;
    }

    static void Main(string[] args)
    {
        var input = (args.Length > 0) ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line => {
                var rows = line
                                .Split('|');
                var rowsCount = rows.Count();
                var vals = rows
                                .SelectMany(elem=> elem.Split(' '))
                                .Where(el => !string.IsNullOrEmpty(el))
                                .Select(e => int.Parse(e))
                                .ToList();
                var matrix = Deserialize(vals, rowsCount);
                var transposed = Transpose(matrix);
                return Enumerable
                    .Range(0, transposed.GetLength(0))
                    .Select(row => GetRow(transposed, row).Max())
                    .Aggregate(string.Empty, (seed, str) => string.IsNullOrEmpty(seed) ? str.ToString() : seed + " " + str);
            }).ToList()
            .ForEach(l => Console.WriteLine(l));
    }
}