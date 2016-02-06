using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class Ext
{
    public static bool IsEscaped(this string str, int pos, char ch)
    {
        pos--;
        if (pos < str.Length && pos >= 0) return str[pos] == '\\';
        else return false;
    }

    public static string[] Split(this string str, char separator, bool allowEscapedSeparators)
    {
        if (allowEscapedSeparators) return str.Split(separator);
        int pos = 0;
       // Console.WriteLine(str);
        var substrs = new List<string>();
        while (pos < str.Length)
        {
            var substr = "";
            while (pos < str.Length)
            {
                if (str[pos] != separator)
                {
                    substr += str[pos];
                    pos++;
                }
                else
                {
                    if (IsEscaped(str, pos, separator))
                    {
                        var arr = substr.ToArray();
                        arr[pos - 1] = separator;
                        substr = new string(arr);
                        pos++;
                    }
                    else
                    {
                        pos++;
                        break;
                    }
                }
            }
            //Console.WriteLine("\t" + substr);
            substrs.Add(substr);
        }
        return substrs.ToArray();
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        var input = args.Length > 0 && !string.IsNullOrEmpty(args[0]) ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(
                line =>
                {
                    var splitted = line.Split(',');
                    var subject = splitted[0];
                    var search = splitted[1].Trim();
                    var subsearches = search
                        .Split('*', allowEscapedSeparators: false)
                        .Where(subs => !string.IsNullOrEmpty(subs))
                        //to aggregate
                        .Aggregate(
                            new List<int>(),
                            (aggr, subsearch) =>
                            {
                                var startInd = aggr.LastOrDefault();
                                var ind = subject.IndexOf(subsearch, startInd < 0 ? 0 : startInd);
                                aggr.Add(ind);
                                return aggr;
                            }
                        );
                    if (subsearches.Contains(-1)) return "false";
                    var orderedSubsearched = subsearches
                                                .OrderBy(s => s);
                    return subsearches
                                .Zip(orderedSubsearched, (a,b)=>a==b)
                                .All(a=>a) 
                                    ? "true" : "false";
                }).ToList()
            .ForEach(l => Console.WriteLine(l));
    }
}
