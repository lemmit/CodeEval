using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval218
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    //Console.WriteLine("Test: " + line.Substring(0, Math.Min(30, line.Length)) + "...");
                   var tab = new HashSet<Tuple<int,int>>();
                   var pipes = line.Split('|')
                            .Select(coordinates => coordinates.Trim().Split(' ').Select(int.Parse).ToArray())
                            .ToList();
                    pipes.ForEach(pipe =>
                    {
                        var from = Math.Min(pipe[0], pipe[1]);
                        var to = Math.Max(pipe[0], pipe[1]);
                        tab.Add(Pipe(from-1, to-1));
                    });
                    
                    var maxX = pipes.Max(pipe => pipe[0])%5;
                    var maxY = pipes.Max(pipe => pipe[1])/5;
                    var maxSize = Math.Min(maxX, maxY);
                    var sqrs = 0;
                    for (int s = 1; s <= 5; s++)
                    {
                        for (int i = 0; i < 5-s; i+=s)
                        {
                            for (int j = 0; j < 5-s; j+=s)
                            {

                                var leftUpper = i + 5 * j;
                                var rightUpper = i + s + 5 * j;
                                var leftLower = i + 5 * (j + s);
                                var rightLower = i + s + 5 * (j + s);

                                //check
                                var sqr = true;
                                for (int offset = 0; offset < s; offset++)
                                {
                                    var t = tab.Contains(Pipe(leftUpper + offset, leftUpper + offset + 1));
                                    var l = tab.Contains(Pipe(leftUpper + 5 * offset, leftUpper + 5 * (offset + 1)));
                                    var r = tab.Contains(Pipe(rightUpper + 5 * offset, rightUpper + 5 * (offset + 1)));
                                    var b = tab.Contains(Pipe(leftLower + offset, leftLower + (offset + 1)));
                                    if (!(t && l && r && b))
                                    {
                                        sqr = false;
                                    }
                                }
                                
                                if (sqr)
                                {
                                    //Console.WriteLine($"Sqrt found: {i} {j} of size {s}");
                                    sqrs++;
                                }
                            }
                        }
                    }
                    return $"{sqrs}";
                }).ToList().ForEach(Console.WriteLine);
        }
        
        private static Tuple<int, int> Pipe(int x, int y)
        {
            return new Tuple<int, int>(x, y);
        }
    }
}
