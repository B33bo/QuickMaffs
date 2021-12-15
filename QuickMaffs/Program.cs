using System;
using System.IO;
using System.Numerics;

//FIX FACTORIALS
//1--e is broken
namespace QuickMaffs
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(File.ReadAllText(StaticMethods.GetPath("HelpPage.txt")));
            Console.ResetColor();

            while (true)
            {
                Equation eq = new(Console.ReadLine());
                Console.WriteLine(eq.ToString() + "=");
                Console.WriteLine(eq.Solve());
            }
        }
    }
}
