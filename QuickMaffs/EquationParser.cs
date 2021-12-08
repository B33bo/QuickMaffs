using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;

namespace QuickMaffs
{
    public static class EquationParser
    {
        public static Dictionary<string, Complex> variables = new()
        {
            { "π", Math.PI },
            { "e", Math.E },
            { "nan", Complex.NaN },
            { "∞", double.PositiveInfinity },
            { "ε", double.Epsilon },
            { "𝜏", Math.Tau },
            { "Φ", Constants.GoldenRatio },
        };
        public const string digits = "0123456789.,Eim";

        public static Complex Solve(string equation)
        {
            if (equation == string.Empty)
                return 0;

            equation = equation.Trim().Replace(" ", "");
            //Look for parenthesis first

            equation = FindNegativeNumsAndFix(equation);
            equation = FindVariables(equation);
            equation = ExpandBrackets(equation);
            equation = ResolveBrackets(equation);

            int operations = OperationCount(equation);

            if (operations == 0)
            {
                if (ParseComplex.TryParse(equation, out Complex result))
                    return result;
                return Complex.NaN;
            }

            else if (operations == 1)
            {
                if (equation[0] == '-')
                    return Solve("m" + equation[1..]);
                return SolveOneOperationEquation(equation);
            }

            //There are many operations (1+2/3 = 1.66666666666)

            StringBuilder equationBuilder = new StringBuilder(equation);
            for (int i = 0; i < Operator.maxPemdasOrder + 1; i++)
            {
                for (int j = 0; j < equation.Length; j++)
                {
                    if (!Operator.TryParse(equation[j].ToString(), out Operator oper))
                        continue;

                    if (oper.pemdasOrder != i)
                        continue;

                    string insideEquation = GetNearestNumbers(equation, j, out int left);

                    string insideEquationSolution = FindNegativeNumsAndFix(Solve(insideEquation).ToMathematicalString());
                    equationBuilder = equationBuilder.Replace(insideEquation, insideEquationSolution, left, insideEquation.Length);
                    equation = equationBuilder.ToString();
                    j = -1;
                }
            }

            return Solve(equation);
        }

        private static string FindVariables(string Equation)
        {
            List<string> Keys = variables.Keys.ToList();
            List<Complex> Values = variables.Values.ToList();

            for (int i = 0; i < Keys.Count; i++)
            {
                Equation = Equation.Replace(Keys[i], Values[i].ToMathematicalString());
            }

            return Equation;
        }

        private static string FindNegativeNumsAndFix(string Equation)
        {
            StringBuilder equation_StrBuilder = new(Equation);

            for (int i = 0; i < Equation.Length; i++)
            {
                if (Equation[i] == '-')
                {
                    if (i == 0)
                        equation_StrBuilder[0] = 'm';
                    else
                    {
                        if (!digits.Contains(Equation[i-1]) || Equation[i-1] == 'i')
                            equation_StrBuilder[i] = 'm';
                    }
                }
            }

            return equation_StrBuilder.ToString();
        }

        public static string GetNearestNumbers(string Equation, int index, out int left)
        {
            string returnValue = Equation[index].ToString();
            left = -1;

            for (int i = index + 1; i < Equation.Length; i++)
            {
                if (!digits.Contains(Equation[i]))
                    break;

                returnValue += Equation[i];
            }

            for (int i = index - 1; i >= 0; i--)
            {
                if (!digits.Contains(Equation[i]))
                    break;
                returnValue = returnValue.Insert(0, Equation[i].ToString());
                left = i;
            }

            return returnValue;
        }

        private static Complex SolveOneOperationEquation(string equation)
        {
            int operIndex = -1;

            string num1 = "";
            string num2 = "";
            Operator oper = Operator.Invalid;
            for (int i = 0; i < equation.Length; i++)
            {
                if (!Operator.TryParse(equation[i].ToString(), out oper))
                {
                    num1 += equation[i];
                    continue;
                }

                operIndex = i;
                break;
            }

            for (int i = operIndex + 1; i < equation.Length; i++)
            {
                num2 += equation[i];
            }

            _ = ParseComplex.TryParse(num1, out Complex number1);
            _ = ParseComplex.TryParse(num2, out Complex number2);

            return oper.onSolve(number1, number2);
        }

        private static string ResolveBrackets(string equation)
        {
            int bracketNestIndex = 0;
            string currentBracket = "";

            for (int i = 0; i < equation.Length; i++)
            {
                if (bracketNestIndex > 0)
                    currentBracket += equation[i];

                if (equation[i] == '(')
                    bracketNestIndex++;
                else if (equation[i] == ')')
                {
                    bracketNestIndex--;
                    if (bracketNestIndex == 0)
                    {
                        currentBracket = currentBracket[..^1];

                        equation = equation.Replace("(" + currentBracket + ")", Solve(currentBracket).ToMathematicalString());
                    }
                }
            }

            return equation;
        }

        private static int BracketsBalanced(string equation)
        {
            int bracks = 0;

            for (int i = 0; i < equation.Length; i++)
            {
                if (equation[i] == '(')
                    bracks++;
                else if (equation[i] == ')')
                    bracks--;
            }

            return bracks;
        }

        private static int OperationCount(string s)
        {
            int opers = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (Operator.TryParse(s[i].ToString(), out _))
                    opers++;
            }

            return opers;
        }

        private static string ExpandBrackets(string equation)
        {
            for (int i = 0; i < equation.Length; i++)
            {
                if (i <= 0)
                    continue;
                if (equation[i] != '(')
                    continue;
                if (!digits.Contains(equation[i - 1]))
                {
                    if (equation[i - 1] != ')')
                        continue;
                }

                //There is a digit before the bracket.

                equation = equation.Insert(i, $"*");
            }

            return equation;
        }
    }
}