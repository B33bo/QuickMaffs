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
            while (true)
            {
                Console.WriteLine(EquationParser.Solve(Console.ReadLine()).ToReadableMathematicalString());
            }
        }
    }
}
