using System;
using System.Numerics;

//FIX FACTORIALS
namespace QuickMaffs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ResetColor();
            Console.WriteLine("π = Pi\n" +
                "e = Euler's constant\n" +
                "∞ = infinity\n" +
                "ε = Epsilon\n" +
                "Φ = Golden Ratio\n" +
                "? = NaN (Not a Number)\n" +
                "i = Imaginary one\n");

            Test.TestMethods();
            //Console.WriteLine(new Equation("14+1-4+-3+sin(4) - 5 + sin(5 + tan(3), 2345) + 2 * (5+3) - 2").Solve());
            //Console.WriteLine(new Equation("tan(sin(3))").Solve());
            //Console.Write(2d / 3d);

            Console.WriteLine(new Equation(Console.ReadLine()).Solve());
            Console.WriteLine("DONE");
        }
    }
}
