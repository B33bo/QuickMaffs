using System;
using System.Numerics;

namespace QuickMaffs
{
    class Program
    {
        const string equationTest = "5+3 * (4 - 6.1) / 4";
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(EquationParser.Solve(Console.ReadLine()).ToReadableMathematicalString());
            }
        }
    }
}
