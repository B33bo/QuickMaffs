using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs
{
    internal static class StaticMethods
    {

        internal static string Readable(this string[] array, string seperator)
        {
            string str = "";

            for (int i = 0; i < array.Length; i++)
            {
                str += array[i] + seperator;
            }
            return str;
        }

        internal static string Readable(this List<string> array, string seperator)
        {
            string str = "";

            for (int i = 0; i < array.Count; i++)
            {
                str += array[i] + seperator;
            }
            return str;
        }

        internal static string GetPath(string RelativeLoc)
        {
            List<string> currentDir = System.Reflection.Assembly.GetEntryAssembly().Location.Split(@"\").ToList();

            currentDir.RemoveAt(currentDir.Count - 1);
            string path = currentDir.Readable(@"\");
            return path + RelativeLoc;
        }
    }

    public static class ComplexHelper
    {
        public static string ToMathematicalString(this Complex complex)
        {
            complex = complex.Approximate();
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

            if (s == "i")
            {
                result = new(0, 1);
                return true;
            }

            if (s == "-i")
            {
                result = new(0, -1);
                return true;
            }

            if (s[^1] == 'i')
            {
                result = new(0, double.Parse(s[..^1]));
                return true;
            }

            bool isPositive = s.Contains("+");
            if (!isPositive && !s.Contains("-"))
            {
                if (s.EndsWith("i"))
                {
                    result = new(0, double.Parse(s[..^1]));
                    return true;
                }
                return false;
            }

            string[] numbers = s.Contains("+") ? s.Split('+') : s.Split('-');

            if (numbers.Length != 2)
                return false;
            if (numbers[0] == "")
                return false;
            if (numbers[1] == "")
                return false;

            if (numbers[0].Contains("i"))
                result = new(double.Parse(numbers[1]), Parse(numbers[0]).Imaginary);
            else
                result = new(double.Parse(numbers[0]), Parse(numbers[1]).Imaginary);

            return true;
        }

        public static Complex Parse(string s)
        {
            if (TryParse(s, out Complex result))
                return result;

            throw new FormatException();
        }

        public static Complex Approximate(this Complex c)
        {
            double epsilon = Variables.Epsilon;
            int awayFromZero = (int)Math.Ceiling(Math.Log10((1/epsilon) + 1));

            double real = c.Real;
            double imaginary = c.Imaginary;

                real = Math.Round(real, awayFromZero, MidpointRounding.AwayFromZero);

                imaginary = Math.Round(imaginary, awayFromZero, MidpointRounding.AwayFromZero);

            return new(real, imaginary);
        }

        public static double Approximate(this double d)
        {
            return new Complex(d, 0).Approximate().Real;
        }
    }
}
