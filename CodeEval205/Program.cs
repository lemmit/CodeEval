using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval205
{
    public static class SplitterExt
    {
        public static bool IsLetter(char letter)
        {
            return (letter >= 'A' && letter <= 'Z') ||
                   (letter >= 'a' && letter <= 'z');
        }
        public static bool IsLetter(string letter)
        {
            return IsLetter(letter[0]);
        }

        public static IEnumerable<string> SplitUsingNonLetters(this string str)
        {
            var offset = 0;
            while(offset < str.Length)
            {

                while (offset < str.Length && !IsLetter(str[offset]))
                {
                    offset++;
                }
                var sb = new StringBuilder();
                while (offset < str.Length && IsLetter(str[offset]))
                {
                    sb.Append(str[offset]);
                    offset++;
                }
                yield return sb.ToString();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = (args.Length > 0) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line => 
                        line
                            .SplitUsingNonLetters()
                            .Select(word => word.ToLower())
                            .Aggregate(string.Empty, (seed, str) => string.IsNullOrEmpty(seed) ? str : seed + " " + str)
                    )
                .ToList()
                .ForEach(cleanedUpLine => Console.WriteLine(cleanedUpLine));
                
        }
    }
}
