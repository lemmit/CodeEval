using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeEval60
{
    class Program
    {

        static IDictionary<Tuple<int,int>, bool> _dict 
            = new Dictionary<Tuple<int, int>, bool>();

        private static void FloodFill(int x, int y)
        {
            bool sentinel;
            var pos = new Tuple<int, int>(x, y);
            bool wasThere =_dict.TryGetValue(pos, out sentinel);
            if (!wasThere)
            {
                if (CheckSum(pos))
                {
                    _dict[pos] = true;
                    FloodFill(x - 1, y);
                    FloodFill(x + 1, y);
                    FloodFill(x, y - 1);
                    FloodFill(x, y + 1);
                }
                else
                {
                    _dict[pos] = false;
                }
            }
        }

        private static bool CheckSum(Tuple<int, int> pos)
        {
            var x = Math.Abs(pos.Item1);
            var y = Math.Abs(pos.Item2);
            var answ = SumOfDigits(x) + SumOfDigits(y) <= 19;
            //Console.WriteLine($"Checking [{pos.Item1},{pos.Item2}]: {answ}");
            return answ;
        }

        private static int SumOfDigits(int n)
        {
            if (n == 0) return 0;
            return (n % 10) + SumOfDigits(n / 10);
        }

        static void Main(string[] args)
        {
            FloodFill(0,0);
            Console.WriteLine(_dict.Count(p => p.Value == true));
        }
    }
}
