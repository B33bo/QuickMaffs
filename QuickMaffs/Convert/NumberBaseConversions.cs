using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs.Convert
{
    public static class NumberBaseConversions
    {
        public static List<char> Characters = new() {
        '0','1','2','3','4','5','6','7','8','9',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            '_','-'};

        public static string Convert(string number, string from_str, string to_str)
        {
            int from, to;

            from = from_str.ToLower() == "roman" ? -1 : int.Parse(from_str);
            to = to_str.ToLower() == "roman" ? -1 : int.Parse(to_str);

            long base10 = ToBase10(number, from);

            if (to == 1)
                return ToBase1(base10);

            if (to <= 1 && to_str != "roman")
                return FromBase10(base10, 10);

            return FromBase10(base10, to);
        }

        private static string ToBase1(long n)
        {
            string s = "";
            for (int i = 0; i < n; i++)
            {
                s += "#";
            }
            return s;
        }

        private static long ToBase10(string number, int from)
        {
            if (from == 10)
                return long.Parse(number);
            if (from == -1)
                return Roman.FromRoman(number);
            if (from == 1)
                return number.Length;

            long returnValue = 0;

            for (int i = 0; i < number.Length; i++)
            {
                int currentDigitIndex = number.Length - i - 1;
                int currentDigitValue = Characters.IndexOf(number[i]);

                returnValue += (long)(currentDigitValue * Math.Pow(from, currentDigitIndex));
            }

            return returnValue;
        }

        private static string FromBase10(long number, int to)
        {
            if (to == 10)
                return number.ToString();
            if (to == -1)
                return Roman.ToRoman(number);

            string returnValue = "";
            long Quotient = number;

            while (true)
            {
                returnValue = Characters[(int)(Quotient % to)] + returnValue;
                Quotient /= to;

                if (Quotient == 0)
                    return returnValue;
            }
        }
    }
}
