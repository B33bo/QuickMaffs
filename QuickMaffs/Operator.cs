using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.Random;

namespace QuickMaffs
{
    public class Operator
    {
        public const int maxPemdasOrder = 3;

        public static Operator Invalid { get; } = new Operator(0, (a, b) => Complex.NaN, 0, "");

        private readonly static Operator[] operators =
        {
            new (2, (a, b) => a + b, 4, "+"),                                 //Plus     (1 + 1 = 2)
            new (2, (a, b) => a * b, 3, "*", "×"),                            //Times    (2 * 3 = 6)
            new (2, (a, b) => a - b, 4, "-"),                                 //Subtract (5 - 3 = 2)
            new (2, (a, b) => a / b, 3, "/", "÷"),                             //Divide   (9 / 3 = 3)
            new (2, Complex.Pow, 2, "^"),                                     //Power    (4 ^ 2 = 16)
            new (2, (a, b) => Complex.Pow(b, 1f / a), 2, "√"),                 //Root     (3 √ 64 = 4)
            new (2, (a, b) => a == b ? 1 : 0, 0, "="),                         //Equals   (1 = 2 = 0)
            new (2, (a, b) => a.Real > b.Real ? 1 : 0, 0, ">"),                //Greater  (1 < 2 = 1)
            new (2, (a, b) => a.Real < b.Real ? 1 : 0, 0, "<"),                //Greater  (1 < 2 = 0)
            new (1, (a, b) => SpecialFunctions.Factorial((int)a.Real), 1, "!"),//Factorial(5! = 120)
            new (2, (a, b) => RandomNumber(a, b), 1, "?"),
        };

        private static Complex RandomNumber(Complex a, Complex b)
        {
            return new Complex(NextDouble(a.Real, b.Real), NextDouble(a.Imaginary, b.Imaginary));

            static double NextDouble(double min, double max)
            {
                System.Random random = new System.Random();
                double val = (random.NextDouble() * (max - min) + min);
                return val;
            }
        }

        public string[] letters;
        public int arguments;
        public int pemdasOrder;
        public Func<Complex, Complex, Complex> onSolve;

        public Operator(int args, Func<Complex, Complex, Complex> solve, int pemdasOrder, string letter)
        {
            arguments = args;
            letters = new string[] { letter };
            onSolve = solve;
            this.pemdasOrder = pemdasOrder;
        }
        public Operator(int args, Func<Complex, Complex, Complex> solve, int pemdasOrder, params string[] letters)
        {
            arguments = args;
            this.letters = letters;
            onSolve = solve;
            this.pemdasOrder = pemdasOrder;
        }

        public static Operator Parse(string s)
        {
            for (int i = 0; i < operators.Length; i++)
            {
                if (operators[i].letters.Contains(s))
                    return operators[i];
            }

            throw new FormatException();
        }

        public static bool TryParse(string s, out Operator result)
        {
            for (int i = 0; i < operators.Length; i++)
            {
                if (operators[i].letters.Contains(s))
                {
                    result = operators[i];
                    return true;
                }
            }

            result = default;
            return false;
        }
    }
}
