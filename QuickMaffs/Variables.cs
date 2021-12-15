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
        public static Dictionary<char, Complex> variables = new()
        {
            { 'e', Math.E },
            { 'π', Math.PI },
            { 'ε', double.Epsilon },
            { 'Φ', Constants.GoldenRatio},
            { 'i', Complex.ImaginaryOne},
            { '∞', double.PositiveInfinity },
            { '?', double.NaN},
        };
    }
}
