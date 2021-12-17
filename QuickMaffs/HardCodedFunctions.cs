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
        public static Complex Sin(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex newComplex);
            return Complex.Sin(newComplex);
        }

        public static Complex Cos(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex newComplex);
            return Complex.Cos(newComplex);
        }

        public static Complex Tan(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex newComplex);
            return Complex.Tan(newComplex);
        }

        public static Complex SinH(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex newComplex);
            return Complex.Sinh(newComplex);
        }

        public static Complex CosH(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex newComplex);
            return Complex.Cosh(newComplex);
        }

        public static Complex TanH(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex newComplex);
            return Complex.Tanh(newComplex);
        }

        public static Complex Asin(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex newComplex);
            return Complex.Asin(newComplex);
        }

        public static Complex Acos(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex newComplex);
            return Complex.Acos(newComplex);
        }

        public static Complex Atan(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex newComplex);
            return Complex.Atan(newComplex);
        }
        #endregion

        public static Complex Log(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex a);
            _ = ParseComplex.TryParse(parameters[1], out Complex b);
            return Complex.Log(a, b.Real);
        }

        public static Complex LN(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex a);
            return Complex.Log(a);
        }

        public static Complex Rand(string[] parameters)
        {
            if (rnd == null)
                rnd = new();

            _ = ParseComplex.TryParse(parameters[0], out Complex a);
            _ = ParseComplex.TryParse(parameters[1], out Complex b);

            double real = rnd.NextDouble() * (b.Real - a.Real) + a.Real;
            double imag = rnd.NextDouble() * (b.Imaginary - a.Imaginary) + a.Imaginary;

            rnd = new(rnd.Next());

            return new(real, imag);
        }

        public static Complex RandInt(string[] parameters)
        {
            if (rnd == null)
                rnd = new();

            _ = ParseComplex.TryParse(parameters[0], out Complex a);
            _ = ParseComplex.TryParse(parameters[1], out Complex b);

            double real = rnd.Next((int)a.Real, (int)b.Real + 1);
            double imag = rnd.Next((int)b.Imaginary, (int)b.Imaginary + 1);

            rnd = new(rnd.Next());

            return new(real, imag);
        }

        public static Complex Gamma(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex a);

            return SpecialFunctions.Gamma(a.Real);
        }

        public static Complex Factorial(Complex a, Complex b)
        {
            return SpecialFunctions.Gamma(a.Real + 1);
        }

        public static Complex Total(string[] parameters)
        {
            Complex value = 0;

            for (int i = 0; i < parameters.Length; i++)
            {
                if (ParseComplex.TryParse(parameters[i], out Complex newComplex))
                    value += newComplex;
            }
            return value;
        }

        public static Complex Min(string[] parameters)
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
            return min;
        }

        public static Complex Max(string[] parameters)
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
            return max;
        }

        public static Complex Mean(string[] parameters)
        {
            Complex total = 0;

            for (int i = 0; i < parameters.Length; i++)
            {
                if (ParseComplex.TryParse(parameters[i], out Complex newComplex))
                {
                    total += newComplex;
                }
            }
            return total / parameters.Length;
        }

        public static Complex Len(string[] parameters) => parameters.Length;

        public static Complex Real(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);

            return num.Real;
        }

        public static Complex Imaginary(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);

            return num.Imaginary;
        }

        public static Complex HCF(string[] parameters)
        {
            double[] paramsParsed = new double[parameters.Length];
            double lowest = Min(parameters).Real;

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

            return hcf;
        }

        public static Complex LCM(string[] parameters)
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
            return lcm;

            static double findGCD(double a, double b)
            {
                if (b == 0)
                    return a;
                return findGCD(b, a % b);
            }
        }

        public static Complex Mod(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex a);
            _ = ParseComplex.TryParse(parameters[1], out Complex b);

            return a.Real % b.Real;
        }

        public static Complex Sign(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);
            return num.Sign();
        }

        public static Complex Magnitude(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);
            return num.Magnitude;
        }

        public static Complex Phase(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);
            return num.Phase;
        }

        public static Complex NpR(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex n);
            _ = ParseComplex.TryParse(parameters[1], out Complex r);
            return Factorial(n, 0) / Factorial(n - r, 0);
        }

        public static Complex NcR(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex n);
            _ = ParseComplex.TryParse(parameters[1], out Complex r);
            return Factorial(n, 0) / (Factorial(r, 0) * Factorial(n - r, 0));
        }

        public static Complex Abs(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex num);
            return Complex.Abs(num);
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

        public static Complex Set(string[] parameters)
        {
            char varName = parameters[0][0];
            _ = ParseComplex.TryParse(parameters[1], out Complex value);

            if (Variables.variables.ContainsKey(varName))
                Variables.variables[varName] = value;
            else
                Variables.variables.Add(varName, value);

            return value;
        }

        public static Complex Conversion(string[] parameters)
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
                _ => num,
            };
        }

        public static Complex TheAnswerToLifeTheUniverseAndEverything() => 42;

        public static Complex Round(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);

            Complex nearest = 1;
            if (parameters.Length > 1)
                nearest = ParseComplex.Parse(parameters[1]);

            Complex newNum = num / nearest;
            newNum = new Complex(Math.Round(newNum.Real), Math.Round(newNum.Imaginary));
            return newNum * nearest;
        }

        public static Complex Floor(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);

            Complex nearest = 1;
            if (parameters.Length > 1)
                nearest = ParseComplex.Parse(parameters[1]);

            Complex newNum = num / nearest;
            newNum = new Complex(Math.Floor(newNum.Real), Math.Floor(newNum.Imaginary));
            return newNum * nearest;
        }

        public static Complex Ceiling(string[] parameters)
        {
            Complex num = ParseComplex.Parse(parameters[0]);

            Complex nearest = 1;
            if (parameters.Length > 1)
                nearest = ParseComplex.Parse(parameters[1]);

            Complex newNum = num / nearest;
            newNum = new Complex(Math.Ceiling(newNum.Real), Math.Ceiling(newNum.Imaginary));
            return newNum * nearest;
        }
    }
}
