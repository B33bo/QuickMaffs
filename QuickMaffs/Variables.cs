using System;
using MathNet.Numerics;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs
{
    public static class Variables
    {
        public static Dictionary<char, string> variables = new()
        {
            { 'e', Math.E.ToString() },
            { 'π', Math.PI.ToString() },
            { 'ε', "0.000000001"},
            { 'Φ', Constants.GoldenRatio.ToString() },
            { 'φ', Constants.GoldenRatio.ToString() },
            { 'i', "i" },
            { '∞', double.PositiveInfinity.ToString() },
            { '?', double.NaN.ToString() },
            { '\'', "\"" },
        };

        public static Complex Get(char key)
        {
            return ParseComplex.Parse(variables[key]);
        }

        public static double GetDouble(char key)
        {
            return double.Parse(variables[key]);
        }

        public static double Epsilon
        {
            get => double.Parse(variables['ε']);
            set => variables['ε'] = value.ToString();
        }
    }
}
