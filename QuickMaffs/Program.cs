using System;
using System.IO;
using System.Numerics;

namespace QuickMaffs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                Equation eq = new(args[0]);
                Console.WriteLine(eq.ToString() + "=");
                Console.WriteLine(eq.Solve());
                return;
            }
            
            Console.ResetColor();

            while (true)
            {
                string input = Console.ReadLine();
                Equation eq = new(input);
                Console.WriteLine(eq.ToString() + "=");
                Console.WriteLine(eq.Solve());
            }
        }
    }
}
