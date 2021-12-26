using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs.Convert
{
    public static class Time
    {
        private static Dictionary<string, Complex> DaysIn = new()
        {
            {"jan", 31},
            {"feb", 28},
            {"mar", 31},
            {"apr", 30},
            {"may", 31},
            {"jun", 30},
            {"jul", 31},
            {"aug", 31},
            {"sep", 30},
            {"oct", 31},
            {"nov", 30},
            {"dec", 31},

            {"febly", 29},      //feb-leap year
        };

        public static Dictionary<string, string> Units { get; } = new()
        {
            { "Nanosecond (ns)", "ns" },
            { "Microsecond (μs)", "μs" },
            { "Millisecond (ms)", "ms" },
            { "Second (s)", "second" },
            { "Minute (m)", "minute" },
            { "Hour (h)", "hour" },
            { "Day", "day" },
            { "Week", "week" },
            { "Fortnight (2 weeks)", "fortnight" },
            { "Month", "month" },
            { "Year", "year" },
            { "Decade", "decade" },
            { "Century", "century" },
            { "Millennium", "millennium" },
            { "Planck time", "planck" },
        };

        private static Dictionary<string, Complex> ToHours { get; } = new()
        {
            { "ns", 2.77778e-13 },
            { "μs", 2.7778e-10 },
            { "ms", 2.7778e-7 },
            { "second", 0.000277778 },
            { "minute", 0.0166667 },
            { "hour", 1 },
            { "day", 24 },
            { "week", 168 },
            { "fortnight", 336 },
            { "month", 730},
            { "year", 8760 },
            { "decade", 87600 },
            { "century", 876000 },
            { "millennium", 8760000 },
            { "planck", 1.497222222e-47 },
        };

        public static string Convert(Complex from, string fromUnits, string toUnits)
        {
            //Convert from X to kiloJoule
            fromUnits = fromUnits.ToLower();
            Complex hours;

            if (fromUnits.StartsWith("month="))
            {
                string month = fromUnits.Split('=')[1][..3];
                if (fromUnits.Contains("leapyear") && month == "feb")
                    month = "febly";

                hours = from * (DaysIn[month] * 24);
            }
            else
                hours = from * ToHours[fromUnits];

            //Convert from kiloJoule to Y
            return (hours / ToHours[toUnits.ToLower()]).ToMathematicalString();
        }
    }
}
