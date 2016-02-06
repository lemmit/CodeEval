using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CodeEval210
{
    class BrainfuckInterpreter
    {
        
     //   private readonly IDictionary<int, char> _memory = new Dictionary<int, char>();
        const int MemSize = 100000000;
        private readonly byte[] _memory = new byte[MemSize];
        private readonly char[] _program;
        private readonly int EOF;

        private int _ip;
        private int _memPointer = MemSize/2;
        private int _maxMem;

        public BrainfuckInterpreter(char[] program)
        {
            _program = program;
            EOF = _program.Length;
        }

        public void Run()
        {
            try
            {
                runInterpreter();
            }
            catch (Exception e)
            {
                throw new Exception(
                    $"memPtr: {_memPointer}, ip: {_ip} program: {new string(_program)}"
                    , e);
            }
        }
        private void runInterpreter()
        {
            while (_ip < EOF)
            {
                //Console.WriteLine(_program[_ip]);
                //for(int i=0; i<=_maxMem; i++) Console.Write((int)_memory[i]+"|");
                //Console.WriteLine();
                var currentCommand = _program[_ip];
                switch (currentCommand)
                {
                    case '>':
                        _memPointer++;
                        _maxMem = Math.Max(_memPointer, _maxMem);
                        break;
                    case '<':
                        _memPointer--;
                        break;
                    case '+':
                        _memory[_memPointer]++;
                        break;
                    case '-':
                        _memory[_memPointer]--;
                        break;
                    case '.':
                        Console.Write((char)(_memory[_memPointer]));
                        break;
                    case ',':
                        try
                        {
                            _memory[_memPointer] = (byte) Console.Read();
                        }
                        catch (Exception e)
                        {
                            Debug.Write(e.StackTrace);
                        }
                        break;
                    case '[':
                        if (_memory[_memPointer] == 0)
                        {
                            var openedBrackets = 1;
                            while (true)
                            {
                                _ip++;
                                if (_program[_ip] == '[')
                                {
                                    openedBrackets++;
                                }
                                else if (_program[_ip] == ']')
                                {
                                    openedBrackets--;
                                }
                                if (openedBrackets == 0)
                                {
                                    break;
                                }
                            }
                        }
                        break;

                    case ']':
                        if (_memory[_memPointer] != 0)
                        {
                            var closingBrackets = 1;
                            while (true)
                            {
                                _ip--;
                                if (_program[_ip] == ']')
                                {
                                    closingBrackets++;
                                }
                                else if (_program[_ip] == '[')
                                {
                                    closingBrackets--;
                                }
                                if (closingBrackets == 0)
                                {
                                    break;
                                }
                            }
                        }
                        break;
                }
                _ip++;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "../../input.txt";
            File.ReadAllLines(input)
                .Select(line =>
                {
                    new BrainfuckInterpreter(line.ToCharArray()).Run();
                    Console.WriteLine();
                    return true;
                }).ToList();
        }
    }
}
