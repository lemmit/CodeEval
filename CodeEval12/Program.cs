using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        File.ReadAllLines(args[0])
            .Select(line => {
                var tab = new char[256];
                line.ToList().ForEach(ch => tab[ch]++);

                for (int i = 0; i < line.Length; i++)
                {
                    if (tab[line[i]] == 1) return line[i];
                }
                return ' ';
            })
            .ToList()
            .ForEach(Console.WriteLine);
    }
}