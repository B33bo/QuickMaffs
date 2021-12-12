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
            { "sin", new Function(HardCodedFunctions.Sin) },
            { "sinh", new Function(HardCodedFunctions.SinH) },
            { "asin", new Function(HardCodedFunctions.Asin) },
            { "cos", new Function(HardCodedFunctions.Cos) },
            { "acos", new Function(HardCodedFunctions.Acos) },
            { "cosh", new Function(HardCodedFunctions.CosH) },
            { "tan", new Function(HardCodedFunctions.Tan) },
            { "atan", new Function(HardCodedFunctions.Atan) },
            { "tanh", new Function(HardCodedFunctions.TanH) },
            { "rand", new Function(HardCodedFunctions.Rand) },
            { "log", new Function(HardCodedFunctions.Log) },
            { "ln", new Function(HardCodedFunctions.LN) },
            { "Γ", new Function(HardCodedFunctions.Gamma) },
            { "total", new Function(HardCodedFunctions.Total) },
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
