using System;
using System.Linq;
using System.IO;

class Program
{
    static void Main(string[] args)
    {

        var input = (args.Length > 0) ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line => 
            {
                var rows = line.Split(',').ToArray();
                var moved = 0;
                while (SimulateStep(rows))
                {
                    moved++;
                }
                return moved;
            })
            .ToList()
            .ForEach(answ => Console.WriteLine(answ));
    }

    private static bool SimulateStep(string[] rows)
    {
        for(int i=0; i<rows.Length; i++)
        {
            if (rows[i].Contains("XY")) return false;
            if (rows[i].Contains("Y"))
            {
                int ind = rows[i].IndexOf("Y");
                rows[i] =
                    rows[i].Substring(0, ind - 1)
                    + "Y"
                    + rows[i].Substring(ind);
            }
        }
        return true;
    }
}