using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs.Convert
{
    public static class Temperature
    {
        public static Dictionary<string, string> Units { get; } = new()
        {
            { "Celsius (C)", "c"},
            { "Centigrade (C)" , "c"},
            { "Fahrenheit (F)" , "f"},
            { "Kelvin (K)" , "k"},
            { "Rankine (R)", "r"},
        };

        private static Complex ToCelsius(Complex value, char from)
        {
            return from switch
            {
                'c' => value,                       //Celsius
                'f' => (value - 32) * (5d / 9d),    //Fahrenheit
                'k' => value - 273.15,              //Kelvin
                'r' => (value - 491.67) * 5d / 9d,  //Rankine
                _ => value,
            };
        }

        private static Complex FromCelsius(Complex value, char to)
        {
            return to switch
            {
                'c' => value,                       //Celsius
                'f' => value * (9d / 5d) + 32,      //Fahrenheit
                'k' => value + 273.15,              //Kelvin
                'r' => (value * 9d / 5d) + 491.67,  //Rankine
                _ => value,
            };
        }

        public static string Convert(Complex from, string fromUnits, string toUnits)
        {
            //Convert from X to celsius
            Complex Celsius = ToCelsius(from, fromUnits.ToLower()[0]);

            //Convert from celsius to Y
            return FromCelsius(Celsius, toUnits.ToLower()[0]).ToMathematicalString();
        }
    }
}
