using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
