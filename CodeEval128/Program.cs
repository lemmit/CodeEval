using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input).ToList().ForEach(line => {
            var splitted = line.Split(' ');
            var last = splitted[0];
            var count = 1;
            for (int i = 1; i < splitted.Length; i++)
            {
                var current = splitted[i];
               // Console.WriteLine("Current: " + current + " last: " + last + " count: " + count);
                if (last == current)
                {
                    count++;
                }
                else
                {
                    Console.Write(count + " " + last + " ");
                    last = current;
                    count = 1;
                }
            }
            Console.Write(count + " " + last + " ");
            Console.WriteLine();
        });

    }
}