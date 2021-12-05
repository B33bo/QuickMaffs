using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs
{
    public static class StaticMethods
    {
        public static string ToMathematicalString(this Complex complex)
        {
            if (complex.Imaginary == 0)
                return complex.Real.ToString();

            if (complex.Real == 0)
                return complex.Imaginary.ToString() + "i";

            if (complex.Imaginary == 1)
                return $"i + {complex.Real}";

            return $"{complex.Imaginary}i + {complex.Real}";
        }

        public static string Readable(this string[] array)
        {
            string str = "";

            for (int i = 0; i < array.Length; i++)
            {
                str += array[i];
            }
            return str;
        }
    }

    public static class ParseComplex
    {
        //FORMAT: 3i1 = 3i + 1
        public static bool TryParse(string s, out Complex result)
        {
            s = s.Replace("m", "-");
            s = s.Replace(",", "");

            result = 0;
            if (double.TryParse(s, out double res))
            {
                result = res;
                return true;
            }

            if (s.Contains("i"))
            {
                string[] splitByi = s.Split('i', StringSplitOptions.RemoveEmptyEntries);

                if (splitByi.Length == 0)
                {
                    result = Complex.ImaginaryOne;
                    return true;
                }

                if (splitByi.Length == 1)
                {
                    if (double.TryParse(splitByi[0], out res))
                    {
                        result = new(res, 0);
                        return true;
                    }
                    return false;
                }

                if (splitByi.Length == 2)
                {
                    if (!double.TryParse(splitByi[0], out double imaginary))
                        return false;
                    if (!double.TryParse(splitByi[1], out double real))
                        return false;

                    result = new(real, imaginary);
                    return true;
                }
            }

            return false;
        }
    }
}
