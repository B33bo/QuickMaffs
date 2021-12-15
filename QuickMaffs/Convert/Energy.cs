using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs.Convert
{
    public static class Energy
    {
        public static Dictionary<string, string> Units { get; } = new()
        {
            { "KiloJoules (KJ)", "kj" },
            { "Joules (K)", "j" },
            { "Calorie (cal)", "calorie" },
            { "KiloCalories", "kcal" },
            { "Watt Hour", "watthour" },
            { "KiloWatt Hour", "kilowatthour" },
            { "Electron Volts (ev)", "ev" },
            { "British Thermal", "britthermal" },
            { "US Thermal", "usthermal" },
            { "foot-pound", "footpound" },
        };

        private static Dictionary<string, Complex> ToKiloJoule { get; } = new()
        {
            { "kj", 1 },
            { "j", 0.001 },
            { "calorie", 0.004184 },
            { "kcal", 4.184 },
            { "watthour", 3.6 },
            { "kilowatthour", 3600 },
            { "ev", 1.6022e-22 },
            { "britthermal", 1.05506 },
            { "usthermal", 105480.81464345 },
            { "footpound", 0.0013558232780517241473 },
        };

        public static Complex Convert(Complex from, string fromUnits, string toUnits)
        {
            //Convert from X to kiloJoule
            Complex kiloJoule = from * ToKiloJoule[fromUnits.ToLower().Replace(" ", "")];

            //Convert from kiloJoule to Y
            return kiloJoule / ToKiloJoule[toUnits.ToLower().Replace(" ", "")];
        }
    }
}
