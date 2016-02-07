using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval41
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    try
                    {
                        line.Split(';')[1].Split(',').OrderBy(elem => elem)
                            .Aggregate("-1", (last, current) =>
                            {
                                if(last == current) throw new DuplicateFoundException(current);
                                return current;
                            });
                    }
                    catch (DuplicateFoundException e)
                    {
                        return e.Message;
                    }
                    return "Never happened?";
                }).ToList()
                .ForEach(l => Console.WriteLine(l));
        }
    }

    [Serializable]
    internal class DuplicateFoundException : Exception
    {
        public DuplicateFoundException()
        {
        }

        public DuplicateFoundException(string message) : base(message)
        {
        }

        public DuplicateFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
