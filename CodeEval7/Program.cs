using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/* PREFIX EXPRESSIONS */

namespace CodeEval7
{
    internal interface IOperator
    {
        float Perform(float a, float b);
    }

    internal class AddOperator : IOperator
    {
        public float Perform(float a, float b)
        {
            return a + b;
        }
    }

    internal class MulOperator : IOperator
    {
        public float Perform(float a, float b)
        {
            return a*b;
        }
    }

    internal class DivOperator : IOperator
    {
        public float Perform(float a, float b)
        {
            return a/b;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length > 0 && !string.IsNullOrEmpty(args[0]) ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line => EvaluatePrefixExpression(line))
                .ToList()
                .ForEach(value => Console.WriteLine(value));
        }

        private static int EvaluatePrefixExpression(string line)
        {
            var splitted = line.Split(' ');
            float holder;
            var numbers = splitted
                .Where(token => float.TryParse(token, out holder))
                .Select(intToken => float.Parse(intToken));
            var signs = splitted
                .Where(token => !float.TryParse(token, out holder))
                .Select<string, IOperator>(operationToken =>
                {
                    switch (operationToken)
                    {
                        case "*":
                            return new MulOperator();
                        case "+":
                            return new AddOperator();
                        case "/":
                            return new DivOperator();
                        default:
                            throw new ArgumentException("Operation not supported!: " + operationToken);
                    }
                });
            var signStack = new Stack<IOperator>(signs);
            var numberStack = new Stack<float>(numbers.Reverse());
            while (numberStack.Count > 1 && signStack.Count > 0)
            {
                var a = numberStack.Pop();
                var b = numberStack.Pop();
                numberStack.Push(signStack.Pop().Perform(a, b));
            }
            return (int) numberStack.Pop();
        }
    }
}