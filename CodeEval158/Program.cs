using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CodeEval158
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .ToList()
                .ForEach(line =>
                {
                    var splitted = line.Split('|');
                    var nrs = splitted[0].Trim().Split(' ').Select(int.Parse).ToArray();
                    var iters = long.Parse(splitted[1]);

                    //Console.WriteLine(iters);
                    //return;
                   // if(iters > maxSize)
                     //   throw new ArgumentException($"Too much iterations :{iters}");

                    for (long it = 0; it < iters; it++)
                    {
                        var exch = false;
                        for (long i = 0; i < nrs.LongLength - 1 - it; i++)
                        {
                            if (nrs[i + 1] < nrs[i])
                            {
                                var temp = nrs[i + 1];
                                nrs[i + 1] = nrs[i];
                                nrs[i] = temp;
                                exch = true;
                            }
                        }
                        if (exch == false) break;
                    }
                    for (long i = 0; i < nrs.LongLength; i++)
                    {
                        if (i == nrs.LongLength - 1)
                        {
                            Console.WriteLine(nrs[i]);
                        }
                        else Console.Write(nrs[i] + " ");
                    }
                });
        }
    }
}
