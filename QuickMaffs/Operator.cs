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
            { '÷', new Operator(2, (a, b) => a / b) },
            { '^', new Operator(1, Complex.Pow) },
            { '√', new Operator(1, (a, b) => Complex.Pow(b, 1 / a)) },
            { '!', new Operator(1, OperatorDirection.left, HardCodedFunctions.Factorial) },
            { '=', new Operator(4, (a, b) => (a == b) ? 1 : -1) },
            { '≠', new Operator(4, (a, b) => (a != b) ? 1 : -1) },
            { '>', new Operator(4, HardCodedFunctions.GreaterThan) },
            { '≥', new Operator(4, HardCodedFunctions.GreaterEqual) },
            { '<', new Operator(4, HardCodedFunctions.LessThan) },
            { '≤', new Operator(4, HardCodedFunctions.LessEqual) },
            { '≈', new Operator(4, HardCodedFunctions.Approx) },
            { '≉', new Operator(4, HardCodedFunctions.NotApprox) },
            { '±', new Operator(3, (a, b) => a + b) },
            { '%', new Operator(0, OperatorDirection.left, (a, b) => a / 100) },
            { 'E', new Operator(0, (a, b) => a * (Complex.Pow(10, b))) },
            { '°', new Operator(0, OperatorDirection.left, (a, b) => ParseComplex.Parse(Convert.Angle.Convert(a, "degree", "radian"))) },
            { '@', new Operator(0, OperatorDirection.none, (a, b) => 0)},
        };

        public Func<Complex, Complex, Complex> operation;
        public int bidmasIndex;
        public const int highestBidmas = 5;
        public OperatorDirection direction = OperatorDirection.both;

        public Operator(string name, int bidmasIndex, Func<Complex, Complex, Complex> operation)
        {
            this.bidmasIndex = bidmasIndex;
            this.operation = operation;
            direction = OperatorDirection.both;

            operators.Add(name[0], this);
        }

        public Operator(int bidmasIndex, Func<Complex, Complex, Complex> operation)
        {
            this.bidmasIndex = bidmasIndex;
            this.operation = operation;
            direction = OperatorDirection.both;
        }

        public Operator(string name, OperatorDirection direction, int bidmasIndex, Func<Complex, Complex, Complex> operation)
        {
            this.bidmasIndex = bidmasIndex;
            this.operation = operation;
            this.direction = direction;

            operators.Add(name[0], this);
        }

        public Operator(int bidmasIndex, OperatorDirection direction, Func<Complex, Complex, Complex> operation)
        {
            this.bidmasIndex = bidmasIndex;
            this.operation = operation;
            this.direction = direction;
        }
    }
    public enum OperatorDirection
    {
        none = 0,
        left = 1,
        right = 2,
        both = 3,
    }
}
