using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeEval137
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            var grouped = File.ReadAllLines(input).SelectMany(line =>
            {
                var triad = "(([1-2][0-9][0-9])|([1-9][0-9])|[1-9])";
                var dot = "[.]";
                var regex = new Regex(triad + dot + triad + dot + triad + dot + triad);
                var matches = regex.Matches(line);
                var matchedValues = new List<string>();
                for (var i = 0; i < matches.Count; i++)
                {
                    matchedValues.Add(matches[i].Value);
                }
                return matchedValues;
            }).GroupBy(k => k).ToList();
            var max = grouped.Max(c => c.Count());
            grouped .Where(gr => isValidIpAddress(gr.Key))
                    .Where(c => c.Count() == max)
                    .Select(grouping => grouping.Key)
                    .ToList()
                    .ForEach(Console.WriteLine);
            
        }

        private static bool isValidIpAddress(string key)
        {
            var ip = key.Split('.').Select(int.Parse).ToArray();
            if (ip.Length < 4) return false;
            //1.0.0.0 to 255.255.255.254
            if (
                (ip[0] > 0 && ip[0] < 256) &&
                (ip[1] > 0 && ip[1] < 256) &&
                (ip[2] > 0 && ip[2] < 256) &&
                (ip[3] > 0 && ip[3] < 255)
                )
                return true;
            return false;
        }
    }
}
