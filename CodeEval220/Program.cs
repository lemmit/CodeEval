using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeEval220
{
    public static class Ext
    {
        public static string RemoveWhitespaces(this string str)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                if (!string.IsNullOrWhiteSpace(ch.ToString())) sb.Append(ch);
            }
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var vals = line
                                .RemoveWhitespaces()
                                .Split(',')
                                .Select(house => int.Parse(house.Split(':')[1]))
                                .ToArray();
                    var kids = vals[0] + vals[1] + vals[2];
                    return (int) (vals[0]*3 + vals[1]*4 + vals[2]*5)*vals[3]/kids;
                })
                .ToList()
                .ForEach(check => Console.WriteLine(check));
        }
    }
}
