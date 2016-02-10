using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Program
{
    private static string[,] Deserialize(IEnumerable<string> values)
    {
        var vals = values.ToList();
        var rank = (int) Math.Sqrt(vals.Count);
        var matrix = new string[rank, rank];
        for (var i = 0; i < rank; i++)
        {
            for (var j = 0; j < rank; j++)
            {
                matrix[i, j] = vals[i*rank + j];
            }
        }
        return matrix;
    }

    private static string[,] Rotate(string[,] matrix)
    {
        var rank = matrix.GetLength(0);
        var transposed = new string[rank, rank];
        for (var i = 0; i < rank; i++)
        {
            for (var j = 0; j < rank; j++)
            {
                transposed[j, rank - 1 - i] = matrix[i, j];
            }
        }
        return transposed;
    }

    private static IEnumerable<string> Serialize(string[,] matrix)
    {
        var rank = matrix.GetLength(0);
        for (var i = 0; i < rank; i++)
        {
            for (var j = 0; j < rank; j++)
            {
                yield return matrix[i, j];
            }
        }
    }

    private static void Main(string[] args)
    {
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line =>
            {
                var vals = line.Split(' ');
                var matrix = Deserialize(vals);
                var transposed = Rotate(matrix);
                return Serialize(transposed)
                    .Aggregate(string.Empty, (seed, str) => string.IsNullOrEmpty(seed) ? str : seed + " " + str);
            }).ToList()
            .ForEach(l => Console.WriteLine(l));
    }
}