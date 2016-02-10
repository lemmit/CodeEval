using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeEval187_2
{
    internal class Program
    {
        private const int MaxN = 18;

        private static IDictionary<int, List<int>> _dict;

        private static int _nrOfChains;

        private static readonly bool[] _primeCache = new bool[MaxN*2];
        private static readonly bool[] _primeCacheCalculated = new bool[MaxN*2];

        private static void InitPairDict(int n)
        {
            _dict = new Dictionary<int, List<int>>();
            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < n; j++)
                {
                    if (IsPrime(i + j))
                    {
                        List<int> list;
                        if (_dict.TryGetValue(i, out list))
                        {
                            list.Add(j);
                            _dict[i] = list;
                        }
                        else
                        {
                            _dict[i] = new List<int> {j};
                        }
                    }
                }
            }
        }

        private static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .ToList()
                .ForEach(line =>
                {
                    var n = int.Parse(line);
                    if (n > MaxN)
                        throw new ArgumentException($"Wrong input: {n}");
                    InitPairDict(n + 1);

                    var chainsCount = GenerateChains(n + 1);
                    Console.WriteLine(chainsCount);
                   // Console.WriteLine($"{{{n}, {chainsCount}}},");
                });
        }

        private static int GenerateChains(int n)
        {
            if (n == 2) return 1;
            if (n == 3) return 1;
            _nrOfChains = 0;
            GenerateChains(new List<int> {1}, n);
            return _nrOfChains;
        }

        private static void GenerateChains(IList<int> chain, int n)
        {
            var count = chain.Count;
            if (count >= n) return;
            var last = chain.Last();
            var nextElems = _dict[last].Where(k => !chain.Contains(k)).ToList();
            if (nextElems.Any() && count < n)
            {
                foreach (var next in nextElems)
                {
                    var deeper = chain.ToList();
                    deeper.Add(next);
                    GenerateChains(deeper, n);
                }
            }
            else if (IsPrime(last + 1) && count == n - 1)
            {
                _nrOfChains++;
                /*foreach (var elem in chain)
                {
                    Console.Write(elem+" ");
                }
                Console.WriteLine(1);*/
            }
        }

        private static bool IsPrime(int candidate)
        {
            if (candidate == 2) return true;
            if (candidate%2 == 0) return false;

            if (_primeCacheCalculated[candidate])
                return _primeCache[candidate];

            for (var i = 3; i*i <= candidate; i += 2)
            {
                if (candidate%i == 0)
                {
                    _primeCache[candidate] = false;
                    _primeCacheCalculated[candidate] = true;
                    return false;
                }
            }
            _primeCache[candidate] = candidate != 1;
            _primeCacheCalculated[candidate] = true;
            return candidate != 1;
        }
    }
}