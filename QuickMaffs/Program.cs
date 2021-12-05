﻿using System;
using System.Numerics;

namespace QuickMaffs
{
    class Program
    {
        const string equationTest = "5+3 * (4 - 6) / 4.2";
        static void Main(string[] args)
        {
            Console.WriteLine(equationTest);
            while (true)
            {
                Console.WriteLine(EquationParser.Solve(Console.ReadLine()).ToMathematicalString());
            }
        }
    }
}
