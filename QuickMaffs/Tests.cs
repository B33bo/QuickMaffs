using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs
{
    public static class Test
    {
        public static void TestMethods()
        {
            int test = 0;
            try
            {
                test++;
                Assert(new Equation("1+1").Solve(), "2", 1);
                test++;
                Assert(new Equation("(1+1)*5").Solve(), "10", 2);
                test++;
                Assert(new Equation("5--2").Solve(), "7", 3);
                test++;
                Assert(new Equation("5+i").Solve(), "i + 5", 4);
                test++;
                Assert(new Equation("5+3 * (4 - 6.1) / 4").Solve()[..5], "3.425", 5);
                test++;
                Assert(new Equation("5+3 * (4 - 6.1) / 4").ToString(), "5+3*(4-6.1)/4", 6);
                test++;
                Assert(new Equation("sin(50)").Solve(), "-0.26237485370392877", 7);
                test++;
                Assert(new Equation("e").Solve().StartsWith("2.71"), $"{new Equation("e").Solve()} != 2.71", 8);
                test++;
                Assert(new Equation("5e").ToString().StartsWith("5*2.71"), $"{new Equation("5e")} != 5*2.71", 9);
                test++;
                Assert(new Equation("5e").Solve().StartsWith("13.59"), $"{new Equation("5e").Solve()} != 13.59", 10);
                test++;
                Assert(new Equation("5i+3").Solve() == "5i + 3", $"{new Equation("5i+3").Solve()} != 5i + 3", 11);
                test++;
                Assert(new Equation("πe").Solve().StartsWith("8.53"), $"{new Equation("πe").Solve()} != 8.53", 12);
                test++;
                Assert(new Equation("1.2π").Solve().StartsWith("3.76"), $"{ new Equation("1.2π").Solve()} != 3.76", 13);
                test++;
                Assert(new Equation("πππ").Solve().StartsWith("31"), $"{ new Equation("πππ").Solve()} != 31", 14);
                test++;
                Assert(new Equation("log(5,3)+1").Solve().StartsWith("2.46"), $"{ new Equation("log(5,3)+1").Solve()} != 2.46", 15);
                test++;
                Assert(new Equation("total(e, 5)+1").Solve().StartsWith("8.71"), $"{ new Equation("total(e, 5)+1").Solve()} != 8.71", 16);
                test++;
                Assert(new Equation("convert(\"angle\", 5, \"degree\", \"radian\")").Solve().StartsWith("0.0872"), $"{ new Equation("convert(\"angle\", 5, \"degree\", \"radian\")").Solve()} !=0.0872", 17);
                test++;
                Assert(new Equation("mod(mod(4553,65),mod(2344,454))").Solve(), "3", 18);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"error on {test}.{exc}");
            }

            Console.WriteLine($"--END--");
            Console.ResetColor();
        }

        public static void Assert(bool value, string ExceptionMessage, int test)
        {
            if (value)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ Test {test} passed.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"X Test {test} failed. {ExceptionMessage}");
        }

        public static void Assert(string value, string expected, int test)
        {
            if (value == expected)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ Test {test} passed.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"X Test {test} failed. {value} != {expected}");
        }
    }
}
