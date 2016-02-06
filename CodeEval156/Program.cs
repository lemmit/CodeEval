using System;
using System.IO;
using System.Linq;

namespace CodeEval199
{
    internal class Program
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

        private static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var str = "";
                    var pos = 0;
                    for (var i = 0; i < line.Length; i++)
                    {
                        var ch = line[i].ToString();
                        if (IsLetter(ch))
                        {
                            str += pos%2 == 0 ? ch.ToUpper() : ch.ToLower();
                            pos++;
                        }
                        else
                        {
                            str += ch;
                        }
                    }
                    return str;
                })
                .ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }
}