using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.Random;

namespace QuickMaffs
{
    public class Operator
    {
        public static Dictionary<char, Operator> operators = new()
        {
            { '+', new Operator(3, (a, b) => a + b) },
            { '-', new Operator(3, (a, b) => a - b) },
            { '*', new Operator(2, (a, b) => a * b) },
            { '×', new Operator(2, (a, b) => a * b) },
            { '/', new Operator(2, (a, b) => a / b) },
            { '÷', new Operator(2, (a, b) => a * b) },
            { '^', new Operator(1, Complex.Pow) },
            { '√', new Operator(1, (a, b) => Complex.Pow(b, 1 / a)) },
            { '!', new Operator(1, HardCodedFunctions.Factorial) },
            { '=', new Operator(4, (a, b) => (a == b) ? 1 : -1) },
            { '≠', new Operator(4, (a, b) => (a != b) ? 1 : -1) },
            { '>', new Operator(4, HardCodedFunctions.GreaterThan) },
            { '<', new Operator(4, HardCodedFunctions.LessThan) },
        };

        public Func<Complex, Complex, Complex> operation;
        public int bidmasIndex;
        public const int highestBidmas = 5;

        public Operator(string name, int bidmasIndex, Func<Complex, Complex, Complex> operation)
        {
            this.bidmasIndex = bidmasIndex;
            this.operation = operation;

            operators.Add(name[0], this);
        }

        public Operator(int bidmasIndex, Func<Complex, Complex, Complex> operation)
        {
            this.bidmasIndex = bidmasIndex;
            this.operation = operation;
        }
    }
}
