using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval226
{
    class Program
    {
        /*A | B | C | D | E | F | G | H | I | J | K | L | M
          U | V | W | X | Y | Z | N | O | P | Q | R | S | T*/

        static IDictionary<string, string> Encoding =
            new Dictionary<string, string>()
            {
                {"a", "u"}, {"u", "a"},
                {"b", "v"}, {"v", "b"},
                {"c", "w"}, {"w", "c"},
                {"d", "x"}, {"x", "d"},
                {"e", "y"}, {"y", "e"},
                {"f", "z"}, {"z", "f"},
                {"g", "n"}, {"n", "g"},
                {"h", "o"}, {"o", "h"},
                {"i", "p"}, {"p", "i"},
                {"j", "q"}, {"q", "j"},
                {"k", "r"}, {"r", "k"},
                {"l", "s"}, {"s", "l"},
                {"m", "t"}, {"t", "m"},
                {" ", " "},
            };

        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    return line.ToList()
                        .Select(ch => ch.ToString())
                        .Select(c => Encoding[c])
                        .Aggregate(string.Empty,
                            (seed, encChar) => string.IsNullOrEmpty(seed) ? encChar : seed + encChar);
                }).ToList().ForEach(l => Console.WriteLine(l));
        }
    }
}
