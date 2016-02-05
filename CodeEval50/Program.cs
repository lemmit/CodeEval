using System;
using System.IO;
using System.Linq;

namespace CodeEval50
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = (args.Length > 0 && !string.IsNullOrEmpty(args[0])) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var splitted = line.Split(';');
                    var pairs = splitted.Skip(1).First().Split(',');
                    var subject = splitted.First();
                    return ReplaceAll(subject, pairs.ToArray());
                })
                .ToList()
                .ForEach(substituted => Console.WriteLine(substituted));
        }

        private static string ReplaceAll(string subject, string[] pairs)
        {
            string[] tab = new string[subject.Length];
            for (var i = 0; i < pairs.Count(); i += 2)
            {
                var key = pairs[i];
                var value = pairs[i + 1];
                int index = -1;
                while ((index = subject.IndexOf(key)) >= 0)
                {
                    tab[index] = value;
                    //Console.WriteLine($"{subject} {key}=>{value}");
                    subject = subject.Substring(0, index)
                              + string.Empty.PadLeft(key.Length, 'X')
                              + subject.Substring(index + key.Length);
                    
                }
            }
            for (var i = 0; i < subject.Length; i++)
            {
                if (subject[i] != 'X')
                    tab[i] = subject[i].ToString();
            }
            return tab.Aggregate("", (seed, str) => seed + str);
        }
    }
}
