using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs.Convert
{
    public static class Angle
    {
        public static Dictionary<string, string> Units { get; } = new()
        {
            { "degree (°)", "degree" },
            { "radian (c)", "radian" },
            { "gradian", "gradian" },
            { "milliradian", "milliradian" },
            { "minute arc", "minutearc" },
            { "second arc", "secondarc" },
        };

        private static Dictionary<string, Complex> ToDegrees { get; } = new()
        {
            { "degree", 1 },
            { "radian", 180 / Math.PI },
            { "gradian", 0.9 },
            { "milliradian", 180 / (1000 * Math.PI) },
            { "minutearc", 1d / 60d },
            { "secondarc", 1d / 3600d},
        };

        public static string Convert(Complex from, string fromUnits, string toUnits)
        {
            //Convert from X to kiloJoule
            Complex degrees = from * ToDegrees[fromUnits.ToLower().Replace(" ", "")];

            //Convert from kiloJoule to Y
            return (degrees / ToDegrees[toUnits.ToLower().Replace(" ", "")]).ToMathematicalString();
        }
    }
}
