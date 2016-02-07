using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval15
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(BitConverter.IsLittleEndian ? "LittleEndian" : "BigEndian");
        }
    }
}
