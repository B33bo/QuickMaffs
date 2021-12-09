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
            { '/', new Operator(2, (a, b) => a / b) },
        };

        public Func<Complex, Complex, Complex> operation;
        public int bidmasIndex;

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
