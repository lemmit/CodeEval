using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval67
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 && !string.IsNullOrEmpty(args[0]) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var hexNumber = line.ToArray();
                    int val = 0;
                    var len = hexNumber.Length;
                    for (int i = len-1; i >= 0; i--)
                    {
                        int num = hexNumber[i];
                        if (num >= '0' && num <= '9')
                        {
                            num -= '0';
                        }else if (num >= 'a' && num <= 'f')
                        {
                            num = num - 'a' + 10;
                        }
                        val += num * (int)Math.Pow(16, len - 1 - i);
                    }
                    return val;
                })
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}
