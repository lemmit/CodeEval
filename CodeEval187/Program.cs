using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
IF 
YOU 
WANT
TO
SEE
WORKING 
SOLUTION
GO
TO
CodeEval187_2
*/

namespace CodeEval187
{
    class Program
    {

        private static IEnumerable<int> PrimesUpTo(int n)
        {
            var primes = Enumerable.Range(2, n-1 -2).Where(elem => IsPrime(elem));
            return primes;
        } 

        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var n = int.Parse(line);
                    var nsum = (n+1)*2 * 2;
                    var elems = new List<List<int>>[nsum+1];
                    elems[1] = new List<List<int>>()
                    {
                        new List<int>()
                    };
                    elems[2] = new List<List<int>>()
                    {
                        new List<int>() {2}
                    };

                    for (int i = 3; i <= nsum; i++)
                    {
                        var list = new List<List<int>>();
                        var primes = PrimesUpTo(i).OrderByDescending(p => p);

                        foreach (var prime in primes)
                        {
                            var sublists = elems[i - prime].ToList();
                            foreach (var sublist in sublists)
                            {
                                var sublistn = sublist.ToList();
                                sublistn.Add(prime);
                                list.Add(sublistn);
                            }
                        }
                        
                        if (IsPrime(i))
                        {
                            list.Add(new List<int>() { i });
                        }
                        elems[i] = list;
                    }
                    var possiblePartitions = elems[nsum];
                    /*Console.WriteLine("Partitions:");
                    foreach (var partition in possiblePartitions)
                    {
                        Console.WriteLine(partition.Aggregate("", (s,e)=>s==""? e+"" : s+" "+e));
                    }*/
                   
                    var distinctPossibilities = possiblePartitions
                                                .Distinct(new SequenceComparer<int>())
                                                .ToList();
                    /*
                    Console.WriteLine("Distinct partitions:");
                    foreach (var partition in distinctPossibilities)
                    {
                        Console.WriteLine(partition.Aggregate("", (s, e) => s == "" ? e + "" : s + " " + e));
                    }*/
                    var distinctPossWithLengthOk = distinctPossibilities
                                                        .Where(possiblity => possiblity.Count() == n);

                    var poss = Enumerable
                        .Range(1, nsum*nsum)
                        .Select(i => new Tuple<int, int>(i%n, i/n))
                        .Where(p => IsPrime(p.Item1+p.Item2))
                        .GroupBy(k => k.Item1+k.Item2)
                        .ToDictionary(sumedTo => sumedTo.Key, sumedTo => sumedTo.ToList());

                    var mappedDistinct = distinctPossWithLengthOk.Select(seq => seq.Select(
                            seqElem => poss[seqElem]
                        ));

                    var combs = Combinations(mappedDistinct);

                    var count = combs.Count();
                    Console.WriteLine("COUNT: " + count);
                    return count;

                }).ToList().ForEach(Console.WriteLine);

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
                        yield return new List<T>() { subElem }.Concat(subSolv);
                    }
                }
            }

        }

        public class SequenceComparer<T> : IEqualityComparer<IEnumerable<int>>
        {
            public bool Equals(IEnumerable<int> x, IEnumerable<int> y)
            {
                if (x.Count() != y.Count()) return false;
                return x.OrderBy(a => a)
                    .Zip(y.OrderBy(b => b), (xelem, yelem) => xelem == yelem)
                    .All(e => e);
            }

            public int GetHashCode(IEnumerable<int> obj)
            {
                return obj.Sum(x => x.GetHashCode());
            }
        }

        private static readonly IDictionary<int, bool> _primeCache = new Dictionary<int, bool>();
        private static bool IsPrime(int candidate)
        {
            if (candidate == 2) return true;
            if (candidate%2 == 0) return false;

            bool answ;
            if (_primeCache.TryGetValue(candidate, out answ)) return answ;

            for (int i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0)
                {
                    _primeCache[candidate] = false;
                    return false;
                }
            }
            _primeCache[candidate] = candidate != 1;
            return candidate != 1;
        }
    }    
}