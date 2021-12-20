using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs
{
    public class Function
    {
        public static Dictionary<string, Function> functions = new()
        {
            #region trig
            { "sin", new Function(HardCodedFunctions.Sin) },
            { "sinh", new Function(HardCodedFunctions.SinH) },
            { "asin", new Function(HardCodedFunctions.Asin) },
            { "arcsin", new Function(HardCodedFunctions.Asin) },
            { "cos", new Function(HardCodedFunctions.Cos) },
            { "cosh", new Function(HardCodedFunctions.CosH) },
            { "acos", new Function(HardCodedFunctions.Acos) },
            { "arccos", new Function(HardCodedFunctions.Acos) },
            { "tan", new Function(HardCodedFunctions.Tan) },
            { "tanh", new Function(HardCodedFunctions.TanH) },
            { "atan", new Function(HardCodedFunctions.Atan) },
            { "arctan", new Function(HardCodedFunctions.Atan) },
            { "csc", new Function(HardCodedFunctions.Csc) },
            { "csch", new Function(HardCodedFunctions.Csch) },
            { "acsc", new Function(HardCodedFunctions.ACsc) },
            { "arccsc", new Function(HardCodedFunctions.ACsc) },
            { "sec", new Function(HardCodedFunctions.Sec) },
            { "sech", new Function(HardCodedFunctions.Sech) },
            { "asec", new Function(HardCodedFunctions.ASec) },
            { "arcsec", new Function(HardCodedFunctions.ASec) },
            { "cot", new Function(HardCodedFunctions.ACot) },
            { "coth", new Function(HardCodedFunctions.Coth) },
            { "acot", new Function(HardCodedFunctions.ACot) },
            { "arccot", new Function(HardCodedFunctions.ACot) },
            #endregion
            { "rand", new Function(HardCodedFunctions.Rand) },
            { "randint", new Function(HardCodedFunctions.RandInt) },
            { "log", new Function(HardCodedFunctions.Log) },
            { "ln", new Function(HardCodedFunctions.LN) },
            { "Γ", new Function(HardCodedFunctions.Gamma) },
            { "total", new Function(HardCodedFunctions.Total) },
            { "min", new Function(HardCodedFunctions.Min) },
            { "max", new Function(HardCodedFunctions.Max) },
            { "mean", new Function(HardCodedFunctions.Mean) },
            { "len", new Function(HardCodedFunctions.Len) },
            { "real", new Function(HardCodedFunctions.Real) },
            { "imaginary", new Function(HardCodedFunctions.Imaginary) },
            { "hcf", new Function(HardCodedFunctions.HCF) },
            { "gcd", new Function(HardCodedFunctions.HCF) },
            { "lcm", new Function(HardCodedFunctions.LCM) },
            { "mod", new Function(HardCodedFunctions.Mod) },
            { "sign", new Function(HardCodedFunctions.Sign) },
            { "magnitude", new Function(HardCodedFunctions.Magnitude) },
            { "phase", new Function(HardCodedFunctions.Phase) },
            { "nPr", new Function(HardCodedFunctions.NpR) },
            { "nCr", new Function(HardCodedFunctions.NcR) },
            { "abs", new Function(HardCodedFunctions.Abs) },
            { "set", new Function(HardCodedFunctions.Set)},
            { "convert", new Function(HardCodedFunctions.Conversion)},
            { "theanswertolifetheuniverseandeverything", new Function((_) => "42")}, //https://hitchhikers.fandom.com/wiki/42
            { "round", new Function(HardCodedFunctions.Round)},
            { "floor", new Function(HardCodedFunctions.Floor)},
            { "ceil", new Function(HardCodedFunctions.Ceiling)},
            { "Σ", new Function(HardCodedFunctions.Sigma)},
            { "sigma", new Function(HardCodedFunctions.Sigma)},
            { "product", new Function(HardCodedFunctions.Product)},
            { "prime", new Function(HardCodedFunctions.Prime)},
            { "divisors", new Function(HardCodedFunctions.Divisors)},
            { "none", new Function(HardCodedFunctions.None)}
        };

        public Func<string[], string> operation;

        public Function(string name, Func<string[], string> operation)
        {
            this.operation = operation;

            functions.Add(name, this);
        }

        public Function(Func<string[], string> operation)
        {
            this.operation = operation;
        }
    }
}
