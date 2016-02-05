using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval199
{
    public static class Ext
    {
        public static string Bitmasked(this string str, string bitmask)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                var current = str[i].ToString();

                if (bitmask[i] == '1')
                {
                    if (current.ToUpper() == current)
                    {
                        sb.Append(current.ToLower());
                    }
                    else
                    {
                        sb.Append(current.ToUpper());
                    }
                }
                else
                {
                    sb.Append(current);
                }
            }
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = (args.Length > 0) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var splitted = line.Split(' ').ToArray();
                    return splitted[0].Bitmasked(splitted[1]);
                })
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}
