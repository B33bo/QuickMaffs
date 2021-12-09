using System;
using System.Numerics;

//2(2+2)-1.5(3+3)
//(2+2)(4+4)
namespace QuickMaffs
{
    class Program
    {
        const string equationTest = "5+3 * (4 - 6.1) / 4";
        static void Main(string[] args)
        {
            Console.WriteLine("π = Pi\n" +
                "e = Euler's constant\n" +
                "∞ = infinity\n" +
                "ε = Epsilon\n" +
                "𝜏 = Tau\n" +
                "Φ = Golden Ratio\n" +
                "? = NaN (Not a Number)\n" +
                "i = Imaginary one\n");

            Console.WriteLine(new Equation("14+1-4+-3+sin(4) - 5 + sin(5 + tan(3)) + 2 * (5+3) - 2"));
            while (true)
            {
                //Console.WriteLine(EquationParser.Solve(Console.ReadLine()).ToReadableMathematicalString());
            }
        }
    }
}
