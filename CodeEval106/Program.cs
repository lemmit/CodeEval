using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval106
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line => int.Parse(line).ToRomanLiteral()).ToList().ForEach(Console.WriteLine);
        }
    }

    public static class IntEx
    {
        public static string ToRomanLiteral(this int integer)
        {

            var ages = "".PadLeft(integer/1000, 'M') + "".PadLeft((integer%1000)/100, 'C');
            var d = (integer%100)/10;
            if (d == 4)
            {
                ages +=     
            }else if (d == 9)
            {

            }
            else
            {
                "".PadLeft("")
            }

            return "";
        }
    }
}
