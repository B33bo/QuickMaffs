using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs
{
    public class Equation
    {
        private const string Digits = "01234567890E.";
        private readonly List<string> components = new();

        enum ComponentType
        {
            Number,
            Operator,
            Function,
            NestedEquation,
        }

        public Equation(string input)
        {
            ComponentType type = ComponentType.Number;
            int nestIndex = 0;

            components.Add("");
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                    continue;

                if (input[i] == '(')
                {
                    if (nestIndex == 0)
                        //Make a new nested equation
                        components.Add("");
                    nestIndex++;
                }
                else if (input[i] == ')')
                {
                    nestIndex--;
                    components[^1] += input[i];
                    if (nestIndex == 0)
                        //Finally outside of nested equation
                        components.Add("");
                    continue;
                }

                //Anything inside a nested equation will be ignored
                if (nestIndex > 0)
                {
                    components[^1] += input[i];
                    continue;
                }

                //Used for method seperation
                if (input[i] == ',')
                {
                    components.Add(",");
                    components.Add("");
                    //components[^1] += ',';
                    continue;
                }

                //Gets the type (number/operator/method)
                ComponentType newType = TypeDetector(components[^1], input[i]);

                //if it has changed, add a new component
                if (type != newType)
                {
                    components.Add("");
                    type = newType;
                }

                components[^1] += input[i];
            }

            //remove empty entries
            components.RemoveAll((a) => a == "");
            //Variables
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].StartsWith("(") && components[i].EndsWith(")"))
                    continue;

                if (components[i].Contains(","))
                    continue;

                if (Function.functions.ContainsKey(components[i]))
                    continue;

                if (components[i].Length == 1)
                {
                    if (!Variables.variables.ContainsKey(components[i][0]))
                        continue;

                    components[i] = Variables.variables[components[i][0]].ToMathematicalString();

                    if (i != 0)
                    {
                        if (ParseComplex.TryParse(components[i - 1], out _))
                            components.Insert(i, "*");
                    }
                }
                else
                {
                    string newEquationInnerds = "";
                    for (int j = 0; j < components[i].Length; j++)
                    {
                        if (Variables.variables.ContainsKey(components[i][j]))
                        {
                            if (j >= 1)
                            {
                                if (!Operator.operators.ContainsKey(newEquationInnerds[j - 1]))
                                    if (!newEquationInnerds.EndsWith("*"))
                                        newEquationInnerds += "*";
                            }

                            newEquationInnerds += Variables.variables[components[i][j]].ToMathematicalString();

                            if (j < components[i].Length-1)
                            {
                                if (!Operator.operators.ContainsKey(components[i][j + 1]))
                                    newEquationInnerds += "*";
                            }
                        }
                        else
                            newEquationInnerds += components[i][j];
                    }

                    if (components[i] == newEquationInnerds)
                        continue;
                    components[i] = $"({newEquationInnerds})";
                }
            }

            static ComponentType TypeDetector(string previous, char newCharacter)
            {
                if (newCharacter == '-')
                {
                    //Could be a negative number or a subtraction

                    if (previous.Length == 0)
                        //There is nothing before it
                        return ComponentType.Operator;

                    if (previous[^1] == 'E')
                        //E notation
                        return ComponentType.Number;

                    if (Operator.operators.ContainsKey(previous[0]) || Function.functions.ContainsKey(previous))
                        //The thing before it is an operator/function.
                        return ComponentType.Number;

                    return ComponentType.Operator;
                }

                if (Digits.Contains(newCharacter))
                    //It's a digit
                    return ComponentType.Number;

                //If it's an operator, treat it as one
                return Operator.operators.ContainsKey(newCharacter) ? ComponentType.Operator : ComponentType.Function;
            }
        }

        public string Solve()
        {
            //Solve functions first

            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].StartsWith('(') && components[i].EndsWith(')'))
                {
                    if (components[i].Contains(","))
                        continue;
                    //Solve nested equations here
                    string newEq = new Equation(components[i][1..^1]).Solve();
                    components[i] = newEq;
                }
            }

            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].StartsWith('('))
                {
                    //It's not a method, it's a bit of code inside parenthesis
                    components[i] = new Equation(components[i]).Solve();
                }

                if (!Function.functions.ContainsKey(components[i]))
                    continue;

                string[] functionParamsString;

                if (components[i + 1].StartsWith("(") && components[i + 1].EndsWith(")"))
                    functionParamsString = components[i + 1][1..^1].Split(',');
                else
                    functionParamsString = components[i + 1].Split(',');

                string[] solvedParams = new string[functionParamsString.Length];

                for (int j = 0; j < functionParamsString.Length; j++)
                {
                    //Thanks to the mostly annoying code suggestions bot, I fixed the bug
                    //It helpfully told me to replace the solvedParams[i] to a [j]
                    solvedParams[j] = new Equation(functionParamsString[j]).Solve();
                }

                components[i] = Function.functions[components[i]].operation(solvedParams).ToMathematicalString();
                components.RemoveAt(i + 1);
                i--;
            }

            //Solve the operators, in bidmas order
            for (int j = 0; j < Operator.highestBidmas; j++)
            {
                for (int i = 0; i < components.Count; i++)
                {
                    if (components[i].Length != 1)
                        continue;
                    if (!Operator.operators.TryGetValue(components[i][0], out Operator oper))
                        continue;
                    if (oper.bidmasIndex != j)
                        continue;

                    Complex a = Complex.NaN, b = Complex.NaN;
                    if (i + 1 < components.Count)
                        _ = ParseComplex.TryParse(components[i - 1], out a);
                    if (i - 1 >= 0)
                        _ = ParseComplex.TryParse(components[i + 1], out b);

                    components[i] = oper.operation(a, b).ToMathematicalString();

                    //Removes the numbers before and after it
                    components.RemoveAt(i - 1);
                    components.RemoveAt(i);

                    j = -1;
                    break;
                }
            }

            //return components.Readable();
            return components[0];
        }

        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < components.Count; i++)
            {
                str += components[i];
            }

            return str;
        }
    }
}
