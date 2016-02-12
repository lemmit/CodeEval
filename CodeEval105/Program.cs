using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval105
{
    class Program
    {
        public static int[,] findMaximumSubMatrix(int[][] matrix)
        {
            var n = matrix.GetLength(0);
            var ps = CalculateVerticalPrefixSum(matrix);

            var maxSum = matrix[0][0];
            int top = 0, left = 0, bottom = 0, right = 0;

            var sum = new int[n];
            var pos = new int[n];

            for (int i = 0; i < n; i++)
            {
                for (int k = i; k < n; k++)
                {
                    ResetTab(sum);
                    ResetTab(pos);
                    int localMax = 0;
                    sum[0] = ps[k,0] - (i == 0 ? 0 : ps[i - 1,0]);
                    for (int j = 1; j < n; j++)
                    {
                        if (sum[j - 1] > 0)
                        {
                            sum[j] = sum[j - 1] + ps[k,j] - (i == 0 ? 0 : ps[i - 1,j]);
                            pos[j] = pos[j - 1];
                        }
                        else
                        {
                            sum[j] = ps[k,j] - (i == 0 ? 0 : ps[i - 1,j]);
                            pos[j] = j;
                        }
                        if (sum[j] > sum[localMax])
                        {
                            localMax = j;
                        }
                    }

                    if (sum[localMax] > maxSum)
                    {
                        maxSum = sum[localMax];
                        top = i;
                        left = pos[localMax];
                        bottom = k;
                        right = localMax;
                    }
                }
            }
            
            return CopySubmatrix(matrix, bottom, top, right, left);
        }

        private static int[,] CopySubmatrix(int[][] matrix, int bottom, int top, int right, int left)
        {
            var output = new int[bottom - top + 1, right - left + 1];
            for (int i = top, k = 0; i <= bottom; i++, k++)
            {
                for (int j = left, l = 0; j <= right; j++, l++)
                {
                    output[k, l] = matrix[i][j];
                }
            }
            return output;
        }

        private static int[,] CalculateVerticalPrefixSum(int[][] matrix)
        {
            int n = matrix.Length;
            var prefixSums = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == 0)
                    {
                        prefixSums[j, i] = matrix[j][i];
                    }
                    else
                    {
                        prefixSums[j, i] = matrix[j][i] + prefixSums[j - 1, i];
                    }
                }
            }
            return prefixSums;
        }

        private static void ResetTab(int[] tab)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = 0;
            }
        }

        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            var matrix = File.ReadAllLines(input)
                             .Select(row => 
                                        row .Split(' ')
                                            .Select(int.Parse)
                                            .ToArray()
                              )
                             .ToArray();
            var maxSubmatrix = findMaximumSubMatrix(matrix);
            var sum = 0;
            for (int i = 0; i < maxSubmatrix.GetLength(0); i++)
            {
                for (int j = 0; j < maxSubmatrix.GetLength(1); j++)
                {
                    //Console.Write(maxSubmatrix[i,j] + " ");
                    sum += maxSubmatrix[i, j];
                }
                //Console.WriteLine();
            }

            Console.WriteLine(sum);
        }
    }
}
