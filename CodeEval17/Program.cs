using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval17
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(line =>
                {
                    var nrs = line.Split(',').Select(i => int.Parse(i)).ToArray();
                    var dynArray = new int[nrs.Length, nrs.Length];
                    var max = nrs.Max();
                    for (int i = 0; i < nrs.Length; i++) dynArray[i, 0] = nrs[i];
                    for (int i = 1; i < nrs.Length; i++)
                    {
                        for (int j = 0; j < nrs.Length - i; j++)
                        {
                            dynArray[j, i] = dynArray[j, i-1] + dynArray[j+i, 0];
                            max = Math.Max(dynArray[j, i], max);
                        }
                    }
                    return max;
                }).ToList().ForEach(l => Console.WriteLine(l));
        }
    }
}
