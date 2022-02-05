using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs.Convert
{
    public static class Roman
    {
        private static readonly string[] Thousands = { "MMMMM", "MMMM", "MMM", "MM", "M" };
        private static readonly string[] Hundreds = { "CM", "DCCC", "DCC", "DC", "D", "CD", "CCC", "CC", "C" };
        private static readonly string[] Tens = { "XC", "LXXX", "LXX", "LX", "L", "XL", "XXX", "XX", "X" };
        private static readonly string[] Units = { "IX", "VIII", "VII", "VI", "V", "IV", "III", "II", "I" };

        public static long FromRoman(string roman)
        {
            roman = roman.ToUpper();
            if (roman == "NULLA")
                return 0;

            int newValue = 0;

            for (int i = 0; i < Thousands.Length; i++)
            {
                if (!roman.StartsWith(Thousands[i]))
                    continue;
                newValue += 1000 * (Thousands.Length - i);
                roman = roman[Thousands[i].Length..];
                break;
            }

            for (int i = 0; i < Hundreds.Length; i++)
            {
                if (!roman.StartsWith(Hundreds[i]))
                    continue;
                newValue += 100 * (Hundreds.Length - i);
                roman = roman[Hundreds[i].Length..];
                break;
            }

            for (int i = 0; i < Tens.Length; i++)
            {
                if (!roman.StartsWith(Tens[i]))
                    continue;
                newValue += 10 * (Tens.Length - i);
                roman = roman[Tens[i].Length..];
                break;
            }

            for (int i = 0; i < Units.Length; i++)
            {
                if (!roman.StartsWith(Units[i]))
                    continue;
                newValue += Units.Length - i;
                roman = roman[Units[i].Length..];
                break;
            }

            return newValue;
        }

        public static string ToRoman(long number)
        {
            long thousands = number / 1000;
            number %= 1000;
            long hundreds = number / 100;
            number %= 100;
            long tens = number / 10;
            long units = number % 10;

            string str = "";
            if (thousands > 0)
                str += Thousands[Thousands.Length - thousands];

            if (hundreds > 0)
                str += Hundreds[Hundreds.Length - hundreds];

            if (tens > 0)
                str += Tens[Tens.Length - tens];

            if (units > 0)
                str +=  Units[Units.Length - units];
            return str;
        }
    }
}
