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
            double real = complex.Real;
            double imaginary = complex.Imaginary;

            if (imaginary == 0)
                return real.ToString();

            if (imaginary == -1 && real == 0)
                return "-i";

            if (real == 0)
                return imaginary == 1 ? "i" : $"{imaginary}i";

            if (imaginary == 1)
                return $"i + {real}";

            if (imaginary == -1)
                return $"-i + {real}";

            return $"{imaginary}i + {real}";
        }

        public static string Readable(this string[] array, string seperator)
        {
            string str = "";

            for (int i = 0; i < array.Length; i++)
            {
                str += array[i] + seperator;
            }
            return str;
        }

        public static string Readable(this List<string> array, string seperator)
        {
            string str = "";

            for (int i = 0; i < array.Count; i++)
            {
                str += array[i] + seperator;
            }
            return str;
        }

        public static string GetPath(string RelativeLoc)
        {
            List<string> currentDir = System.Reflection.Assembly.GetEntryAssembly().Location.Split(@"\").ToList();

            currentDir.RemoveAt(currentDir.Count - 1);
            string path = currentDir.Readable(@"\");
            return path + RelativeLoc;
        }
    }

    public static class ParseComplex
    {
        public static bool TryParse(string s, out Complex result)
        {
            result = 0;
            if (s == null)
                return false;

            s = s.Replace(" ", "");

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
                    if (splitByi[0] == "-")
                    {
                        result = new(0, -1);
                        return true;
                    }
                    if (double.TryParse(splitByi[0], out res))
                    {
                        result = new(0, res);
                        return true;
                    }
                    return false;
                }

                if (splitByi.Length == 2)
                {
                    if (!double.TryParse(splitByi[0], out double imaginary))
                    {
                        if (splitByi[0] == "-")
                            imaginary = -1;
                        else
                            return false;
                    }
                    if (!double.TryParse(splitByi[1], out double real))
                        return false;

                    result = new(real, imaginary);
                    return true;
                }
            }

            return false;
        }

        public static Complex Parse(string s)
        {
            if (TryParse(s, out Complex result))
                return result;

            throw new FormatException();
        }
    }
}
