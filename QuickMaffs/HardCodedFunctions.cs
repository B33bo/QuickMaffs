using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using QuickMaffs.Convert;

namespace QuickMaffs
{
    class HardCodedFunctions
    {
        static Random rnd;

        #region trig
        public static string Sin(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return Complex.Sin(num).ToMathematicalString();
        }

        public static string Cos(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return Complex.Cos(num).ToMathematicalString();
        }

        public static string Tan(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return Complex.Tan(num).ToMathematicalString();
        }

        public static string SinH(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return Complex.Sinh(num).ToMathematicalString();
        }

        public static string CosH(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return Complex.Cosh(num).ToMathematicalString();
        }

        public static string TanH(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return Complex.Tanh(num).ToMathematicalString();
        }

        public static string Asin(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return Complex.Asin(num).ToMathematicalString();
        }

        public static string Acos(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return Complex.Acos(num).ToMathematicalString();
        }

        public static string Atan(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return Complex.Atan(num).ToMathematicalString();
        }

        public static string Csc(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return (1 / Complex.Sin(num)).ToMathematicalString();
        }

        public static string Sec(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return (1 / Complex.Cos(num)).ToMathematicalString();
        }

        public static string Cot(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return (1 / Complex.Tan(num)).ToMathematicalString();
        }

        public static string Csch(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return (1 / Complex.Sinh(num)).ToMathematicalString();
        }

        public static string Sech(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return (1 / Complex.Cosh(num)).ToMathematicalString();
        }

        public static string Coth(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return (1 / Complex.Tanh(num)).ToMathematicalString();
        }

        public static string ACsc(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return (1 / Complex.Asin(num)).ToMathematicalString();
        }

        public static string ASec(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return (1 / Complex.Cos(num)).ToMathematicalString();
        }

        public static string ACot(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);
            return (1 / Complex.Tan(num)).ToMathematicalString();
        }
        #endregion

        public static string Log(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex a);
            _ = ParseComplex.TryParse(parameters[1], out Complex b);
            return Complex.Log(a, b.Real).ToMathematicalString();
        }

        public static string LN(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex a);
            return Complex.Log(a).ToMathematicalString();
        }

        public static string Rand(string[] parameters)
        {
            if (rnd == null)
                rnd = new();

            _ = ParseComplex.TryParse(parameters[0], out Complex a);
            _ = ParseComplex.TryParse(parameters[1], out Complex b);

            double real = rnd.NextDouble() * (b.Real - a.Real) + a.Real;
            double imag = rnd.NextDouble() * (b.Imaginary - a.Imaginary) + a.Imaginary;

            rnd = new(rnd.Next());

            return new Complex(real, imag).ToMathematicalString();
        }

        public static string RandInt(string[] parameters)
        {
            if (rnd == null)
                rnd = new();

            _ = ParseComplex.TryParse(parameters[0], out Complex a);
            _ = ParseComplex.TryParse(parameters[1], out Complex b);

            double real = rnd.Next((int)a.Real, (int)b.Real + 1);
            double imag = rnd.Next((int)b.Imaginary, (int)b.Imaginary + 1);

            rnd = new(rnd.Next());

            return new Complex(real, imag).ToMathematicalString();
        }

        public static string Gamma(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex a);

            return SpecialFunctions.Gamma(a.Real).ToString();
        }

        public static Complex Factorial(Complex a, Complex b)
        {
            return SpecialFunctions.Gamma(a.Real + 1);
        }

        public static string Total(string[] parameters)
        {
            Complex value = 0;

            for (int i = 0; i < parameters.Length; i++)
            {
                if (ParseComplex.TryParse(parameters[i], out Complex newComplex))
                    value += newComplex;
            }
            return value.ToMathematicalString();
        }

        public static string Min(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex min);

            for (int i = 0; i < parameters.Length; i++)
            {
                if (ParseComplex.TryParse(parameters[i], out Complex newComplex))
                {
                    if (newComplex.Real < min.Real)
                        min = new(newComplex.Real, min.Imaginary);
                    if (newComplex.Imaginary < min.Imaginary)
                        min = new(min.Real, newComplex.Imaginary);
                }
            }
            return min.ToMathematicalString();
        }

        public static string Max(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex max);

            for (int i = 0; i < parameters.Length; i++)
            {
                if (ParseComplex.TryParse(parameters[i], out Complex newComplex))
                {
                    if (newComplex.Real > max.Real)
                        max = new(newComplex.Real, max.Imaginary);
                    if (newComplex.Imaginary > max.Imaginary)
                        max = new(max.Real, newComplex.Imaginary);
                }
            }
            return max.ToMathematicalString();
        }

        public static string Mean(string[] parameters)
        {
            Complex total = 0;

            for (int i = 0; i < parameters.Length; i++)
            {
                if (ParseComplex.TryParse(parameters[i], out Complex newComplex))
                {
                    total += newComplex;
                }
            }
            return (total / parameters.Length).ToMathematicalString();
        }

        public static string Len(string[] parameters) => parameters.Length.ToString();

        public static string Real(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);

            return num.Real.ToString();
        }

        public static string Imaginary(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);

            return num.Imaginary.ToString();
        }

        public static string HCF(string[] parameters)
        {
            double[] paramsParsed = new double[parameters.Length];
            double lowest = ParseComplex.Parse(Min(parameters)).Real;

            for (int i = 0; i < parameters.Length; i++)
            {
                if (ParseComplex.TryParse(parameters[i], out Complex result))
                    paramsParsed[i] = result.Real;
            }

            int hcf = 0;
            for (int i = 2; i < Math.Ceiling(lowest) + 1; i++)
            {
                for (int j = 0; j <= paramsParsed.Length; j++)
                {
                    if (j == paramsParsed.Length)
                    {
                        hcf = i;
                        break;
                    }
                    if ((paramsParsed[j] % i) != 0)
                        break;
                }
            }

            return hcf.ToString();
        }

        public static string LCM(string[] parameters)
        {
            double[] paramsParsed = new double[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (ParseComplex.TryParse(parameters[i], out Complex result))
                    paramsParsed[i] = result.Real;
            }

            double lcm = paramsParsed[0];
            double gcd = paramsParsed[0];

            for (int i = 1; i < paramsParsed.Length; i++)
            {
                gcd = findGCD(paramsParsed[i], lcm);
                lcm = (lcm * paramsParsed[i]) / gcd;

            }
            return lcm.ToString();

            static double findGCD(double a, double b)
            {
                if (b == 0)
                    return a;
                return findGCD(b, a % b);
            }
        }

        public static string Mod(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex a);
            _ = ParseComplex.TryParse(parameters[1], out Complex b);

            return (a.Real % b.Real).ToString();
        }

        public static string Sign(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);
            return num.Sign().ToMathematicalString();
        }

        public static string Magnitude(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);
            return num.Magnitude.ToString();
        }

        public static string Phase(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);
            return num.Phase.ToString();
        }

        public static string NpR(string[] parameters)
        {
            Complex n = ParseComplex.Parse(parameters[0]);
            Complex r = ParseComplex.Parse(parameters[1]);

            return (Factorial(n, 0) / Factorial(n - r, 0)).ToMathematicalString();
        }

        public static string NcR(string[] parameters)
        {
            Complex n = ParseComplex.Parse(parameters[0]);
            Complex r = ParseComplex.Parse(parameters[1]);

            return (Factorial(n, 0) / (Factorial(r, 0) * Factorial(n - r, 0))).ToMathematicalString();
        }

        public static string Abs(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);
            return Complex.Abs(num).ToString();
        }

        public static Complex GreaterThan(Complex a, Complex b)
        {
            int real = a.Real > b.Real ? 1 : -1;

            if (a.Imaginary == 0 && b.Imaginary == 0)
                return real;

            int imaginary = a.Imaginary > b.Imaginary ? 1 : -1;
            return new Complex(real, imaginary);
        }

        public static Complex LessThan(Complex a, Complex b)
        {
            int real = a.Real < b.Real ? 1 : -1;

            if (a.Imaginary == 0 && b.Imaginary == 0)
                return real;

            int imaginary = a.Imaginary < b.Imaginary ? 1 : -1;
            return new Complex(real, imaginary);
        }

        public static string Set(string[] parameters)
        {
            char varName = parameters[0][0];
            _ = ParseComplex.TryParse(parameters[1], out Complex value);

            if (Variables.variables.ContainsKey(varName))
                Variables.variables[varName] = value;
            else
                Variables.variables.Add(varName, value);

            return value.ToMathematicalString();
        }

        public static string Conversion(string[] parameters)
        {
            string conversion = parameters[0];
            _ = ParseComplex.TryParse(parameters[1], out Complex num);
            string from = parameters[2];
            string to = parameters[3];

            return conversion switch
            {
                "angle" => Angle.Convert(num, from, to),
                "distance" => Convert.Distance.Convert(num, from, to),
                "energy" => Energy.Convert(num, from, to),
                "temperature" => Temperature.Convert(num, from, to),
                "number" => NumberBaseConversions.Convert(parameters[1], from, to),
                _ => num.ToMathematicalString(),
            };
        }

        public static string TheAnswerToLifeTheUniverseAndEverything() => "42";

        public static string Round(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);

            Complex nearest = 1;
            if (parameters.Length > 1)
                nearest = ParseComplex.Parse(parameters[1]);

            Complex newNum = num / nearest;
            newNum = new Complex(Math.Round(newNum.Real), Math.Round(newNum.Imaginary));
            return (newNum * nearest).ToMathematicalString();
        }

        public static string Floor(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);

            Complex nearest = 1;
            if (parameters.Length > 1)
                nearest = ParseComplex.Parse(parameters[1]);

            Complex newNum = num / nearest;
            newNum = new Complex(Math.Floor(newNum.Real), Math.Floor(newNum.Imaginary));
            return (newNum * nearest).ToMathematicalString();
        }

        public static string Ceiling(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);

            Complex nearest = 1;
            if (parameters.Length > 1)
                nearest = ParseComplex.Parse(parameters[1]);

            Complex newNum = num / nearest;
            newNum = new Complex(Math.Ceiling(newNum.Real), Math.Ceiling(newNum.Imaginary));
            return (newNum * nearest).ToMathematicalString();
        }

        public static string Sigma(string[] parameters)
        {
            char Variable = parameters[0][0];

            if (!Variables.variables.ContainsKey(Variable))
                Variables.variables.Add(Variable, 0);
            Variables.variables[Variable] = ParseComplex.Parse(parameters[1]);

            int Length = int.Parse(parameters[2]);

            int start = (int)Variables.variables[Variable].Real;

            Complex sum = 0;
            Equation sumnationToDo = new(parameters[3]);
            for (int i = start; i < Length + 1; i++)
            {
                Variables.variables[Variable] = i;

                sum += sumnationToDo.SolveComplex();
            }
            return sum.ToMathematicalString();
        }

        public static string Product(string[] parameters)
        {
            char Variable = parameters[0][0];

            if (!Variables.variables.ContainsKey(Variable))
                Variables.variables.Add(Variable, 0);
            Variables.variables[Variable] = ParseComplex.Parse(parameters[1]);

            int Length = int.Parse(parameters[2]);

            int start = (int)Variables.variables[Variable].Real;

            Complex sum = 1;
            Equation sumnationToDo = new(parameters[3]);
            for (int i = start; i < Length + 1; i++)
            {
                Variables.variables[Variable] = i;

                sum *= sumnationToDo.SolveComplex();
            }
            return sum.ToMathematicalString();
        }

        public static string Prime(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);

            if (num.Imaginary != 0)
                return "-1";

            if (num.Real % 1 != 0)
                return "-1";

            if (num.Real == 1 || num.Real == 0)
                return "-1";

            int boundary = (int)Math.Floor(Math.Sqrt(num.Real));

            for (int i = 2; i < boundary + 1; i++)
            {
                if (num.Real % i == 0)
                    return "-1";
            }
            return "1";
        }

        public static string Divisors(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);

            if (num.Real % 1 != 0)
                return "-1";

            if (num.Imaginary != 0)
                return "-1";

            int divisors = 0;

            for (int i = 0; i < num.Real + 1; i++)
            {
                if (num.Real % i == 0)
                    divisors++;
            }
            return divisors.ToString();
        }

        public static string None(string[] parameters)
        {
            return parameters[0];
        }

        public static string Sqrt(string[] parameters)
        {
            return Complex.Sqrt(ParseComplex.Parse(parameters[0])).ToMathematicalString();
        }

        public static string Pow(string[] parameters)
        {
            return Complex.Pow(ParseComplex.Parse(parameters[0]), ParseComplex.Parse(parameters[1])).ToMathematicalString();
        }
    }
}
