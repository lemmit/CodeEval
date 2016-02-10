using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval59
{
    class Program
    {

        private static IDictionary<string, string> _keyboard 
            = new Dictionary<string, string>()
            {
                {"0", "0"},
                {"1", "1"},
                {"2", "abc"},
                {"3", "def"},
                {"4", "ghi"},
                {"5", "jkl"},
                {"6", "mno"},
                {"7", "pqrs"},
                {"8", "tuv"},
                {"9", "wxyz"}
            };

        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var options = line.ToList()
                        .Select(c => _keyboard[c.ToString()].Select(ch => ch.ToString()).ToList());
                    var combinations = Combinations(options);
                    return combinations
                            .Select(comb => comb.Aggregate("", (s,e)=>s==""? e : s+e));
                })
                .ToList()
                .ForEach(combinations =>
                    Console.WriteLine(
                        combinations.Aggregate("", (s, e) => s == "" ? e : s+","+e)
                    )
                );
        }
        private static IEnumerable<IEnumerable<T>> Combinations<T>(IEnumerable<IEnumerable<T>> elems)
        {
            if (!elems.Any())
            {
                yield return new List<T>();
            }
            else
            {
                var singleFirstElem = elems.First();
                foreach (var subElem in singleFirstElem)
                {
                    foreach (var subSolv in Combinations(elems.Skip(1)))
                    {
                        yield return new List<T>() {subElem}.Concat(subSolv);
                    }
                }
            }

        }
    }
}
