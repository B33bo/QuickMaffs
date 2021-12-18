using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs
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
            int from = int.Parse(from_str);
            int to = int.Parse(to_str);

            long base10 = ToBase10(number, from);
            return FromBase10(base10, to);
        }

        private static long ToBase10(string number, int from)
        {
            if (from == 10)
                return long.Parse(number);

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
