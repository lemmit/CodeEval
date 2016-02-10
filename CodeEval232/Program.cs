using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval232
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var splitted = line.Split('|');
                    var nrs = splitted[0].Trim().Split(' ').Select(int.Parse).ToArray();
                    var n = int.Parse(splitted[1]);
                    while (n-- > 0 && StupidSort(nrs)) ;
                    
                    return nrs.Aggregate("", (s, e) => s == "" ? e+"" : s + " " + e);
                })
                .ToList()
                .ForEach(Console.WriteLine);
        }

        private static bool StupidSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i + 1] < array[i])
                {
                    var temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                    return true;
                }
            }
            return false;
        }
    }
}
