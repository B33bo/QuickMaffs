using System;
using System.Numerics;
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
                //1
                Assert(new Equation("1+1").Solve(), "2", ref test);
                
                Assert(new Equation("(1+1)*5").Solve(), "10", ref test);
                
                Assert(new Equation("5--2").Solve(), "7", ref test);
                
                Assert(new Equation("5+i").Solve(), "i + 5", ref test);
                
                //5
                Assert(new Equation("5+3 * (4 - 6.1) / 4").Solve()[..5], "3.425", ref test);
                
                Assert(new Equation("5+3 * (4 - 6.1) / 4").ToString(), "5+3*(4-6.1)/4", ref test);
                
                Assert(new Equation("sin(50)").Solve(), "-0.2623748537", ref test);
                
                Assert(new Equation("e").Solve().StartsWith("2.71"), $"{new Equation("e").Solve()} != 2.71", ref test);
                
                Assert(new Equation("5e").ToString(), "5*e", ref test);
                
                //10
                Assert(new Equation("5e").Solve().StartsWith("13.59"), $"{new Equation("5e").Solve()} != 13.59", ref test);
                
                Assert(new Equation("5i+3").Solve() == "5i + 3", $"{new Equation("5i+3").Solve()} != 5i + 3", ref test);
                
                Assert(new Equation("πe").Solve().StartsWith("8.53"), $"{new Equation("πe").Solve()} != 8.53", ref test);
                
                Assert(new Equation("1.2π").Solve().StartsWith("3.76"), $"{ new Equation("1.2π").Solve()} != 3.76", ref test);
                
                Assert(new Equation("πππ").Solve().StartsWith("31"), $"{ new Equation("πππ").Solve()} != 31", ref test);
                
                //15
                Assert(new Equation("log(5,3)+1").Solve().StartsWith("2.46"), $"{ new Equation("log(5,3)+1").Solve()} != 2.46", ref test);
                
                Assert(new Equation("total(e, 5)+1").Solve().StartsWith("8.71"), $"{ new Equation("total(e, 5)+1").Solve()} != 8.71", ref test);
                
                Assert(new Equation("convert(\"angle\", 5, \"degree\", \"radian\")").Solve().StartsWith("0.0872"), $"{ new Equation("convert(\"angle\", 5, \"degree\", \"radian\")").Solve()} !=0.0872", ref test);
                
                Assert(new Equation("mod(mod(4553,65),mod(2344,454))").Solve(), "3", ref test);

                if (Variables.variables.ContainsKey('x'))
                    Variables.variables['x'] = "5";
                else
                    Variables.variables.Add('x', "5");

                Equation eq = new("5x+1");
                string v1 = eq.Solve();
                Variables.variables['x'] = "10";
                string v2 = eq.Solve();
                Assert((v1 == "26") && (v2 == "51"), $"{v1} != {v2}", ref test);
                
                //20
                Assert(new Equation("sigma(\"x\", 1, 10, \"x+1\")").Solve(), "65", ref test);
                
                Assert(new Equation("value(\"(\")").Solve(), "(", ref test);
                
                Assert(new Equation("value(\")\")").Solve(), ")", ref test);
                
                Assert(new Equation("value(\"(\")+1").Solve(), "1", ref test);

                Assert(new Equation("(1+1)-2").Solve(), "0", ref test);

                //25
                Assert(new Equation("(i+1)(i+1)").Solve(), "2i", ref test);

                Assert(new Equation("i^2").Solve(), "-1", ref test);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"error on {test}.{exc}");
            }

            Console.WriteLine($"--END--");
            Console.ResetColor();
        }

        public static void Assert(bool value, string ExceptionMessage, ref int test)
        {
            test++;
            if (value)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ Test {test} passed.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"X Test {test} failed. {ExceptionMessage}");
        }

        public static void Assert(string value, string expected, ref int test)
        {
            test++;
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
