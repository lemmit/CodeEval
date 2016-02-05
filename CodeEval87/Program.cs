using System;
using System.IO;
using System.Linq;

namespace CodeEval87
{
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

        public static void SetRow<T>(T[,] matrix, int row, T value)
        {
            var columns = matrix.GetLength(1);
            for (int i = 0; i < columns; ++i)
                matrix[row, i] = value;
        }

        public static T[] GetColumn<T>(T[,] matrix, int column)
        {
            var rows = matrix.GetLength(0);
            var array = new T[rows];
            for (int i = 0; i < rows; ++i)
                array[i] = matrix[i, column];
            return array;
        }

        public static void SetColumn<T>(T[,] matrix, int column, T value)
        {
            var rows = matrix.GetLength(0);
            for (int i = 0; i < rows; ++i)
                matrix[i, column] = value;
        }

        private static int[,] Matrix = new int[256, 256];

        static void Main(string[] args)
        {
            var input = (args.Length > 0) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .ToList()
                .ForEach(line =>
                {
                    var splitted = line.Split(' ').ToArray();
                    switch (splitted[0])
                    {
                        case "SetRow":
                            SetRow(int.Parse(splitted[1]), int.Parse(splitted[2]));
                            break;
                        case "SetCol":
                            SetCol(int.Parse(splitted[1]), int.Parse(splitted[2]));
                            break;
                        case "QueryRow":
                            QueryRow(int.Parse(splitted[1]));
                            break;
                        case "QueryCol":
                            QueryCol(int.Parse(splitted[1]));
                            break;
                    }
                });
        }

        private static void QueryCol(int col)
        {
            Console.WriteLine(GetColumn(Matrix, col).Sum());
        }

        private static void QueryRow(int row)
        {
            Console.WriteLine(GetRow(Matrix, row).Sum());
        }

        private static void SetCol(int column, int value)
        {
            SetColumn(Matrix, column, value);
        }

        private static void SetRow(int row, int value)
        {
            SetRow(Matrix, row, value);
        }
    }
}
