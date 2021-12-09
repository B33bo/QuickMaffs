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
        public static Complex Sin(string a, string b, string c)
        {
            _ = ParseComplex.TryParse(a, out Complex newComplex);
            return Complex.Sin(newComplex);
        }

        public static Complex Cos(string a, string b, string c)
        {
            _ = ParseComplex.TryParse(a, out Complex newComplex);
            return Complex.Cos(newComplex);
        }

        public static Complex Tan(string a, string b, string c)
        {
            _ = ParseComplex.TryParse(a, out Complex newComplex);
            return Complex.Tan(newComplex);
        }
    }
}
