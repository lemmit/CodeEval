using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

class Program
{

    static string[,] Deserialize(IEnumerable<string> values)
    {
        var vals = values.ToList();
        var rank = (int)Math.Sqrt(vals.Count);
        var matrix = new string[rank, rank];
        for (int i = 0; i < rank; i++)
        {
            for (int j = 0; j < rank; j++)
            {
                matrix[i, j] = vals[i * rank + j];
            }
        }
        return matrix;
    }

    static string[,] Transpose(string[,] matrix)
    {
        var rank = matrix.GetLength(0);
        var transposed = new string[rank, rank];
        for (int i = 0; i < rank; i++)
        {
            for (int j = 0; j < rank; j++)
            {
                transposed[j, (rank-1) - i] = matrix[i, j];
            }
        }
        return transposed;
    }

    static IEnumerable<string> Serialize(string[,] matrix)
    {
        var rank = matrix.GetLength(0);
        for (int i = 0; i < rank; i++)
        {
            for (int j = 0; j < rank; j++)
            {
                yield return matrix[i, j];
            }
        }
    }

    static void Main(string[] args)
    {
        var input = (args.Length > 0) ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line => {
                var vals = line.Split(' ');
                var matrix = Deserialize(vals);
                var transposed = Transpose(matrix);
                return Serialize(transposed).Aggregate(string.Empty, (seed, str) => string.IsNullOrEmpty(seed) ? str : seed + " " + str);
            }).ToList()
            .ForEach(l => Console.WriteLine(l));
    }
}