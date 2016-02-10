using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval119
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    var chainElems = line.Split(';').Select(e => e.Split('-')).ToList();
                    //Console.WriteLine(line);
                    Dictionary<string, string> dict;
                    Dictionary<string, string> reverseDict;
                    try
                    {
                        dict = chainElems.ToDictionary(elem => elem[0], elem => elem[1]);
                        reverseDict = chainElems.ToDictionary(elem => elem[1], elem => elem[0]);
                    }
                    catch (Exception)
                    {
                        return "BAD";
                    }
                    var firstElem = "BEGIN";//dict.First().Value;
                    var onElem = firstElem;
                    var stillChecking = true;
                    while (dict.Count > 0 && stillChecking)
                    {
                        try
                        {
                            var temp = onElem;
                            onElem = dict[onElem];
                            dict.Remove(temp);
                            reverseDict.Remove(onElem);
                        }
                        catch (Exception)
                        {
                            stillChecking = false;
                        }
                    }
                    /*stillChecking = true;
                    onElem = firstElem;
                    while (reverseDict.Count > 0 && stillChecking)
                    {
                        try
                        {
                            var temp = onElem;
                            onElem = reverseDict[onElem];
                            dict.Remove(onElem);
                            reverseDict.Remove(temp);
                        }
                        catch (Exception)
                        {
                            stillChecking = false;
                        }
                    }*/

                    return dict.Count == 0 ? "GOOD" : "BAD";
                })
                .Select(output => { Console.WriteLine(output); return 0; })
                .ToList();
        }
    }
}
