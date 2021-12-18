using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs.Convert
{
    public static class Distance
    {
        public static Dictionary<string, string> Units { get; } = new()
        {
            { "nanometers (nm)", "nm"},
            { "micrometers (µm)", "µm" },
            { "millimeters (mm)", "mm" },
            { "centimeters (cm)", "cm" },
            { "meters (m)", "m" },
            { "kilometers (km)", "km" },
            { "mile (mi)", "mile" },
            { "yard", "yard" },
            { "foot", "foot" },
            { "inch", "inch" },
            { "nautical mile", "nauticalmile" },
            { "lightyear (ly)", "lightyear" },
            { "astronomical unit (au)", "au" },
            { "planck length", "planck" },
        };

        private static Dictionary<string, Complex> ToDegrees { get; } = new()
        {
            { "nm", 1e-9 },
            { "µm", 1e-6 },
            { "mm", 0.001 },
            { "cm", 0.01 },
            { "m", 1 },
            { "km", 1000},
            { "mile", 1609.34 },
            { "yard", 0.9144 },
            { "foot", 0.3048 },
            { "inch", 0.0254 },
            { "nauticalmile", 1852 },
            { "lightyear", 9.461e+15 },
            { "au", 1.496e+11 },
            { "planck", 1.6e-35 },
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
