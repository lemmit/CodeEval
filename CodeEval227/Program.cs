using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeEval227
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
                                .Select(ch => int.Parse(ch.ToString()))
                                .ToArray();
                    var sumOfEveryThird = 0;
                    var notDubbed = 0;
                    for (int i = 0; i < vals.Length; i++)
                    {
                        if (i%2 == 0)
                        {
                            sumOfEveryThird += vals[i];
                        }
                        else notDubbed += vals[i];
                    }
                    return (sumOfEveryThird*2 + notDubbed)%10 == 0 ? "Real" : "Fake";
                })
                .ToList()
                .ForEach(check => Console.WriteLine(check));
        }
    }
}
