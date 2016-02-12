using System;
using System.IO;
using System.Linq;

namespace CodeEval191
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input2.txt";
           File.ReadAllLines(input)
                .ToList()
                .ForEach(line =>
                {
                    var splitted = line.Split(' ');
                    var M = int.Parse(splitted[0]);
                    var N = int.Parse(splitted[1]);
                    var matrix =
                        splitted[2].Split('|')
                            .Select(row => row.ToList().Select(cell => cell != '.').ToArray())
                            .ToArray();
                    //Console.WriteLine("TEST: " + input.Substring(0, Math.Min(input.Length, 30)));
                    //PrintGameMatrix(M, N, matrix);

                    //try to turn off all lights (don't care about last line)
                    var movesMatrix = new int[M, N];
                    var matrixCopy = matrix.Select(row => row.ToArray()).ToArray();

                    //Console.WriteLine("Before:");
                    //PrintGameMatrix(M, N, matrixCopy);
                    //Console.WriteLine("Solving...");
                    Solve(M, N, matrixCopy, movesMatrix);
                    
                    /*Console.WriteLine("After (1st) solving:");
                    PrintGameMatrix(M, N, matrixCopy);
                    Console.WriteLine("Move matrix:");
                    PrintMovementMatrix(M, N, movesMatrix);
                    Console.WriteLine("Before:");
                    PrintGameMatrix(M, N, matrix);
                    Console.WriteLine("Simulation...");
                    var nextMatrixCopy = matrix.Select(row => row.ToArray()).ToArray();
                    SimulateMoves(M, N, movesMatrix, nextMatrixCopy);
                    Console.WriteLine("After simulation...[should be same as after 1st solving)");
                    PrintGameMatrix(M, N, nextMatrixCopy);
                    
                    Console.WriteLine("Moves after solving(without N-1th row):" + Sum(M, N, movesMatrix));
                    */
                    int[,] solutionMovesMatrix = null;
                    //check last line for lucky case
                    if (matrixCopy[M - 1].All(cell => !cell))
                    {
                        //UHU! 
                        solutionMovesMatrix = movesMatrix;
                    }
                    else
                    {
                        solutionMovesMatrix = BruteforceFirstRowAndSolve(M, N, matrixCopy, movesMatrix);
                    }

                    var sum = 0;
                    if (solutionMovesMatrix != null)
                    {
                       /* Console.WriteLine("Final test -> Game Matrix -> Movements");
                        PrintGameMatrix(M, N, matrix);
                        PrintMovementMatrix(M, N, solutionMovesMatrix);*/
                        sum = Sum(M, N, solutionMovesMatrix);
                        /*Console.WriteLine("Simulating...");
                        SimulateMoves(M, N, solutionMovesMatrix, matrix);
                        Console.WriteLine("Solved matrix (should be all .)");
                        PrintGameMatrix(M, N, matrix);*/
                    }
                    else
                    {
                        sum = -1;
                    }
                    
                    Console.WriteLine(sum);
                });
        }

        private static int[,] BruteforceFirstRowAndSolve(int M, int N, bool[][] gameMatrix, int[,] movesMatrix)
        {
            int[,] solutionMovesMatrix = null;
            int solution;
            int minBits = int.MaxValue;
            var searching = true;
            for (int i = 0; i < 2 << N && searching; i++)
            {
                var newMatrix = gameMatrix.Select(row => row.ToArray()).ToArray();
                var newMoveMatrix = (int[,]) movesMatrix.Clone();
                var bits = 0;
                for (int j = 0; j < N && searching; j++)
                {
                    var lights = (1 << j & i) != 0;
                    //newMatrix[0][j] = lights;
                    if (lights)
                    {
                        Toggle(newMatrix, 0, j);
                        newMoveMatrix[0, j]++;
                        bits++;
                    }
                }
                // PrintGameMatrix(M, N, newMatrix);
                //Solve
                //Console.WriteLine("Trying to solve:");
                //PrintGameMatrix(M, N, newMatrix);
                Solve(M, N, newMatrix, newMoveMatrix);
                //check solved
                if (newMatrix[M - 1].All(cell => !cell))
                {
                    bits = Math.Min(bits, minBits);
                   // Console.WriteLine("Solved!");
                   // Console.WriteLine("number: " + i + " min bits: " + bits); // + " moves sum: "+ solutionSum);
                   // PrintGameMatrix(M, N, newMatrix);

                    searching = false;
                    solution = i;
                    solutionMovesMatrix = newMoveMatrix;
                }
                else
                {
                   // Console.WriteLine("Not solved... number: " + i + " min bits: " + bits); // + " moves sum: "+ solutionSum);
                   // PrintGameMatrix(M, N, newMatrix);
                }
            }
            return solutionMovesMatrix;
        }

        private static void SimulateMoves(int M, int N, int[,] solutionMovesMatrix, bool[][] matrix)
        {
            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                    if (solutionMovesMatrix[i, j]%2 == 1)
                        Toggle(matrix, i, j);
        }

        private static int Sum(int M, int N, int[,] solutionMovesMatrix)
        {
            int sum = 0;
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    sum += (solutionMovesMatrix[i, j]%2);
                }
            }
            return sum;
        }

        private static void Solve(int M, int N, bool[][] matrix, int[,] moveMatrix)
        {
            for (int i = 0; i < M - 1; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (matrix[i][j])
                    {
                        Toggle(matrix, i + 1, j);
                        moveMatrix[i + 1, j]++;
                    }
                }
            }
        }

        private static void PrintMovementMatrix(int m, int n, int[,] moveMatrix)
        {
            Console.WriteLine("------");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(moveMatrix[i,j]%2 == 0 ? '.' : '*');
                }
                Console.WriteLine();
            }
            Console.WriteLine("------");
        }

        private static void Toggle(bool[][] matrix, int i, int j)
        {
            if (i > 0)
                matrix[i - 1][j] = !matrix[i - 1][j];

            if (i < matrix.GetLength(0) - 1)
                matrix[i + 1][j] = !matrix[i + 1][j];

            if (j > 0)
                matrix[i][j - 1] = !matrix[i][j - 1];

            if(j < matrix[0].GetLength(0) - 1)
                matrix[i][j + 1] = !matrix[i][j + 1];

            matrix[i][j] = !matrix[i][j];
        }

        private static void PrintGameMatrix(int M, int N, bool[][] matrix)
        {
            Console.WriteLine("------");
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(matrix[i][j] ? 'o' : '.');
                }
                Console.WriteLine();
            }
            Console.WriteLine("------");
        }
    }
}
