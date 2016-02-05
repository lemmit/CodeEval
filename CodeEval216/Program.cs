using System;
using System.IO;
using System.Linq;

namespace CodeEval216
{
    [Flags]
    public enum Rights
    {
        TotalBan = 0,
        Grant = 1,
        Write = 2,
        Read = 4,
    } ;

    class Program
    {

        /*
                file_1   file_2   file_3
        user_1     7        3        0
        user_2     6        2        4
        user_3     5        1        5
        user_4     3        7        1
        user_5     6        0        2
        user_6     4        2        6
         */

        private static int[,] _grants;
        private static void PopulateGrantTable()
        {
            _grants = new [,]
            {
                 {7,        3,        0},
                 {6,        2,        4},
                 {5,        1,        5},
                 {3,        7,        1},
                 {6,        0,        2},
                 {4,        2,        6}
            };
        }

        static void Main(string[] args)
        {
            var input = (args.Length > 0 && !string.IsNullOrEmpty(args[0])) ? args[0] : "../../input.txt";
            File
                .ReadAllLines(input)
                .Select(line => CheckCommands(line.Split(' ')))
                .ToList()
                .ForEach(ok => Console.WriteLine(ok));
        }

        private static bool CheckCommands(string[] commands)
        {
            PopulateGrantTable();
            return commands.All(command => CheckCommand(command));
        }

        private static bool CheckCommand(string command)
        {
            var elems = command
                            .Split('=')
                            .Select(elem => elem[0] == '>' ? elem.Substring(1) : elem)
                            .ToArray();
            if (elems.Length == 3)
            {
                return CheckAccessCommand(elems);
            }
            if (elems.Length == 5)
            {
                return CheckGiveGrantCommand(elems);
            }
            throw new ArgumentException(command);
        }

        private static bool CheckGiveGrantCommand(string[] elems)
        {
            var who = int.Parse(elems[0].Last().ToString()) - 1;
            var file = int.Parse(elems[1].Last().ToString()) - 1;
            var what = elems[3];
            var toWhom = int.Parse(elems[4].Last().ToString()) - 1;
            if ((Rights.Grant & (Rights) _grants[who, file]) == 0)
            {
                return false;
            }
            else
            {
                _grants[toWhom, file] = (int)((Rights)_grants[toWhom, file] | StringToRight(what));
                return true;
            }
        }

        private static Rights StringToRight(string right)
        {
            switch (right)
            {
                case "write":
                    return Rights.Write;
                case "read":
                    return Rights.Read;
                case "grant":
                    return Rights.Grant;
                default:
                    throw new ArgumentException(right);
            }
        }

        private static bool CheckAccessCommand(string[] elems)
        {
            var who = int.Parse(elems[0].Last().ToString()) - 1;
            var file = int.Parse(elems[1].Last().ToString()) - 1;
            var what = elems[2];
            var hasAccess = ((Rights)_grants[who, file] & StringToRight(what)) != 0;
           // Console.WriteLine($"{who} has {what}access:{hasAccess} to {file}");
            return hasAccess;
        }
    }
}
