using System;
using System.Numerics;

//FIX FACTORIALS
//1--e is broken
namespace QuickMaffs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ResetColor();

            Test.TestMethods();
            //Console.WriteLine(new Equation("14+1-4+-3+sin(4) - 5 + sin(5 + tan(3), 2345) + 2 * (5+3) - 2").Solve());
            //Console.WriteLine(new Equation("tan(sin(3))").Solve());
            //Console.Write(2d / 3d);

            while (true)
            {
                Equation eq = new(Console.ReadLine());
                Console.WriteLine(eq.ToString() + "=");
                Console.WriteLine(eq.Solve());
            }
        }
    }
}
