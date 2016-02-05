using System;
using System.Linq;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var input = (args.Length > 0) ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line => Trim(line))
            .ToList()
            .ForEach(answ => Console.WriteLine(answ));
    }

    private static string Trim(string line)
    {
        if (line.Length > 55)
        {
            var cropped = line.Substring(0, 40);
            var ind = cropped.LastIndexOf(' ');
            if(ind > 0)
                cropped = cropped.Substring(0, ind);
            return cropped.Trim() + "... <Read More>";
        }
        else return line;
    }
}