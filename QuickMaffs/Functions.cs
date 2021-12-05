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
        public static Function[] functions =
        {
            new Function(1, (a, b) => Complex.Sin(a), "sin"),
            new Function(1, (a, b) => Complex.Cos(a), "cos"),
            new Function(1, (a, b) => Complex.Tan(a), "tan"),
        };

        public string name;
        public int arguments;
        public Func<Complex, Complex, Complex> onSolve;

        public Function(int args, Func<Complex, Complex, Complex> solve, string name)
        {
            arguments = args;
            this.name = name;
            onSolve = solve;
        }
    }
}
