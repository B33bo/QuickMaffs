﻿using System;
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
            { "sin", new Function(HardCodedFunctions.Sin) },
            { "sinh", new Function(HardCodedFunctions.SinH) },
            { "asin", new Function(HardCodedFunctions.Asin) },
            { "cos", new Function(HardCodedFunctions.Cos) },
            { "cosh", new Function(HardCodedFunctions.CosH) },
            { "acos", new Function(HardCodedFunctions.Acos) },
            { "tan", new Function(HardCodedFunctions.Tan) },
            { "tanh", new Function(HardCodedFunctions.TanH) },
            { "atan", new Function(HardCodedFunctions.Atan) },
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
            { "theanswertolifetheuniverseandeverything", new Function((_) => 42)},
            { "round", new Function(HardCodedFunctions.Round)},
            { "floor", new Function(HardCodedFunctions.Floor)},
            { "ceil", new Function(HardCodedFunctions.Ceiling)},
            { "Σ", new Function(HardCodedFunctions.Sigma)},
            { "prime", new Function(HardCodedFunctions.Prime)},
            { "divisors", new Function(HardCodedFunctions.Divisors)},
        };

        public Func<string[], Complex> operation;

        public Function(string name, Func<string[], Complex> operation)
        {
            this.operation = operation;

            functions.Add(name, this);
        }

        public Function(Func<string[], Complex> operation)
        {
            this.operation = operation;
        }
    }
}
