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
            { "sin", new Function(3, HardCodedFunctions.Sin) },
            { "cos", new Function(3, HardCodedFunctions.Cos) },
            { "tan", new Function(2, HardCodedFunctions.Tan) },
            { "rand", new Function(2, HardCodedFunctions.Rand) },
        };

        public Func<string[], Complex> operation;
        public int bidmasIndex;

        public Function(string name, int bidmasIndex, Func<string[], Complex> operation)
        {
            this.bidmasIndex = bidmasIndex;
            this.operation = operation;

            functions.Add(name, this);
        }

        public Function(int bidmasIndex, Func<string[], Complex> operation)
        {
            this.bidmasIndex = bidmasIndex;
            this.operation = operation;
        }
    }
}
