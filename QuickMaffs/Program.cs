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
                string input = Console.ReadLine();
                if (input.ToLower() == "test")
                {
                    Test.TestMethods();
                    continue;
                }
                Equation eq = new(input);
                Console.WriteLine(eq.ToString() + "=");
                Console.WriteLine(eq.Solve());
            }
        }
    }
}
