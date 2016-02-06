using System;
using System.IO;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line =>
            {
                var coord = line.Split(' ').Select(coordinate => int.Parse(coordinate)).ToArray();
                return GetDirection(coord);
            })
            .ToList()
            .ForEach(answ => Console.WriteLine(answ));
    }

    private static string GetDirection(int[] coord)
    {
        if (coord[0] == coord[2] && coord[1] == coord[3])
            return "here";
        if (coord[0] == coord[2])
        {
            if (coord[1] < coord[3])
            {
                return "N";
            }
            return "S";
        }
        if (coord[1] == coord[3])
        {
            if (coord[0] < coord[2])
            {
                return "E";
            }
            return "W";
        }
        if (coord[0] < coord[2])
        {
            if (coord[1] < coord[3])
            {
                return "NE";
            }
            return "SE";
        }
        if (coord[1] < coord[3])
        {
            return "NW";
        }
        return "SW";
    }
}