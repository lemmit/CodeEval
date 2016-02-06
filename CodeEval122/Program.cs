using System;
using System.IO;
using System.Linq;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line => Decrypt(line))
            .Select(decrypted => string.IsNullOrEmpty(decrypted) ? "NONE" : decrypted)
            .ToList()
            .ForEach(answ => Console.WriteLine(answ));
    }

    private static string Decrypt(string line)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < line.Length; i++)
        {
            var elem = line[i];
            if (elem >= '0' && elem <= '9')
                sb.Append(elem);
            if (elem >= 'a' && elem <= 'j')
                sb.Append((elem - 'a').ToString());
        }
        return sb.ToString();
    }
}