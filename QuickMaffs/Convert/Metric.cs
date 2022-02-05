using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs.Convert
{
    public static class Metric
    {
        public static List<string> Units = new()
        {
            "yocto (y)",
            "zepto (z)",
            "atto (a)",
            "femto (f)",
            "pico (p)",
            "nano (n)",
            "micro (µ)",
            "milli (m)",
            "centi (c)",
            "deci (d)",
            " (none)",
            "deka (da)",
            "hecto (h)",
            "kilo (k)",
            "mega (M)",
            "giga (G)",
            "tera (T)",
            "peta (P)",
            "exa (E)",
            "zetta (Z)",
            "yotta (Y)",

            "kibi",
            "mebi",
            "gibi",
            "tebi",
            "pebi",
            "exbi",
            "zebi",
            "yobi",
        };

        public static Dictionary<string, Complex> PrefixToMetricValues = new()
        {
            { "yocto", 0.000000000000000000000001 },
            { "zepto", 0.000000000000000000001 },
            { "atto", 0.000000000000000001 },
            { "femto", 0.000000000000001 },
            { "pico", 0.000000000001 },
            { "nano", 0.000000001 },
            { "micro", 0.000001 },
            { "milli", 0.001 },
            { "centi", 0.01 },
            { "deci", 0.1 },
            { "", 1d },
            { "deka", 10d },
            { "hecto", 100d },
            { "kilo", 1_000d },
            { "mega", 1_000_000d },
            { "giga", 1_000_000_000d },
            { "tera", 1_000_000_000_000d },
            { "peta", 1_000_000_000_000_000d },
            { "exa", 1_000_000_000_000_000_000d },
            { "zetta", 1_000_000_000_000_000_000_000d },
            { "yotta", 1_000_000_000_000_000_000_000_000d },

            { "kibi", 1_024d },
            { "mebi", 1_048_576d },
            { "gibi", 1_073_741_824d },
            { "tebi", 1_099_511_627_776d },
            { "pebi", 1_125_899_906_842_624 },
            { "exbi", 1.152921504606847E+18 },
            { "zebi", 1.1805916207174113E+21d },
            { "yobi", 1.2089258196146292E+24d },
        };

        public static Dictionary<string, string> ExpandSymbol = new()
        {
            { "y", "yocto" },
            { "z", "zepto" },
            { "a", "atto" },
            { "f", "femto" },
            { "p", "pico" },
            { "n", "nano" },
            { "µ", "micro" },
            { "m", "milli"},
            { "c", "centi"},
            { "d", "deci"},
            { "", ""},
            { "da", "deka"},
            { "h", "hecto"},
            { "k", "kilo"},
            { "M", "mega"},
            { "G", "giga"},
            { "T", "tera"},
            { "P", "peta"},
            { "E", "exa"},
            { "Z", "zetta"},
            { "Y", "yotta"},
        };

        public static string Convert(Complex num, string from, string to)
        {
            if (!PrefixToMetricValues.ContainsKey(from))
                from = ExpandSymbol[from];
            if (!PrefixToMetricValues.ContainsKey(to))
                from = ExpandSymbol[to];

            Complex numAsNormal = num * PrefixToMetricValues[from];
            return (numAsNormal / PrefixToMetricValues[to]).ToMathematicalString();
        }
    }
}
