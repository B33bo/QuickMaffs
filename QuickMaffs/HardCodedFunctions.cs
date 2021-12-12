using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;

namespace QuickMaffs
{
    class HardCodedFunctions
    {
        static Random rnd;
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

        public static Complex Gamma(string[] parameters)
        {
            _ = ParseComplex.TryParse(parameters[0], out Complex a);

            return SpecialFunctions.Gamma(a.Real);
        }

        public static Complex Factorial(Complex a, Complex b)
        {
            return SpecialFunctions.Gamma(a.Real+1);
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
            Complex min = 0;

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
    }
}
