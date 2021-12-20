using System.Collections.Generic;
using System.Numerics;

//The equation class works by figuring out where each operator is and where each function is

namespace QuickMaffs
{
    public class Equation
    {
        private const string Digits = "01234567890.";
        private readonly List<string> components = new();

        //Is the equation a boolean (true/false return value)
        public bool IsBoolean
        {
            get
            {
                //If it had an equals sign inside of brackets, it wouldn't be inside components. This is good
                if (components.Contains("="))
                    return true;
                if (components.Contains(">"))
                    return true;
                if (components.Contains("<"))
                    return true;
                if (components.Contains("≠"))
                    return true;
                return false;
            }
        }

        //Used for each component to identify which type it is. Only used in initialisation
        enum ComponentType
        {
            Number,
            Operator,
            Function,
            NestedEquation,
            Factorial,
        }

        //For example: 2,(5+5) would return true but 2,*,5,+,5 would return valuse
        private static bool AreBracketsExpandable(List<string> input)
        {
            if (input.Count < 1)
                return false;

            string current = input[^1];

            if (current.Length == 0)
            {
                if (input.Count == 1)
                    return false;

                current = input[^2];
            }

            if (Operator.operators.ContainsKey(current[0]))
                return false;
            if (Function.functions.ContainsKey(current))
                return false;

            //The value is a number
            return true;
        }

        private static bool CheckBrackets(string s)
        {
            int nestIndex = 0;
            bool isSpeechmarks = false;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '"')
                    isSpeechmarks = !isSpeechmarks;

                if (isSpeechmarks)
                    continue;

                if (s[i] == '(')
                    nestIndex++;
                else if (s[i] == ')')
                    nestIndex--;

                if (nestIndex < 0)
                    return false;
            }
            return nestIndex == 0;
        }

        public Equation(string input)
        {
            if (!CheckBrackets(input))
                throw new InvalidEquation("Brackets don't match");

            ComponentType type = ComponentType.Number;
            int nestIndex = 0;
            bool isSpeechMarks = false;

            //In this method, adding an empty string means that the script will append to the last index

            components.Add("");
            for (int i = 0; i < input.Length; i++)
            {
                if (isSpeechMarks)
                {
                    if (input[i] == '"')
                        isSpeechMarks = !isSpeechMarks;
                    //The value is inside of speech marks, so ignore it
                    components[^1] += input[i];
                    continue;
                }

                if (input[i] == ' ')
                    continue;

                if (input[i] == '(')
                {
                    if (nestIndex == 0)
                    {
                        //Checks if there is a number or a bracket before the current index
                        bool isExpandable = AreBracketsExpandable(components);

                        if (isExpandable)
                            components.Add("*");
                        //Make a new nested equation
                        components.Add("");
                    }
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

                //If it finds a speech mark, treat it as a string and don't do anything funky to it
                if (input[i] == '"')
                {
                    isSpeechMarks = !isSpeechMarks;

                    if (isSpeechMarks)
                    {
                        if (nestIndex > 0)
                            components[^1] += input[i];
                        else
                            components.Add("\"");
                        continue;
                    }
                }

                //Anything inside a nested equation will be simply appended and nothing else
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
                    continue;
                }

                //Gets the type (number/operator/method)
                ComponentType newType = TypeDetector(components[^1], input[i]);

                //if it has changed, add a new component
                if (type != newType || type == ComponentType.Operator)
                {
                    components.Add("");
                    type = newType;
                }

                components[^1] += input[i];
            }

            //remove empty entries
            components.RemoveAll((a) => a == "");

            isSpeechMarks = false;

            static ComponentType TypeDetector(string previous, char newCharacter)
            {
                if (newCharacter == '-')
                {
                    //Could be a negative number or a subtraction

                    if (previous.Length == 0)
                        //There is nothing before it
                        return ComponentType.Number;

                    if (Operator.operators.TryGetValue(previous[0], out Operator oper))
                    {
                        if (oper.direction == OperatorDirection.left)
                            return ComponentType.Operator;
                        return ComponentType.Number;
                    }

                    if (Function.functions.ContainsKey(previous))
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

        private List<string> ResolveVariables()
        {
            //This script turns things like "2e" into "2*e" into "2*2.71..."
            //We don't want to directly modify components because you might save an equation, change a variable, and resolve it

            List<string> newComponents = new();

            for (int i = 0; i < components.Count; i++)
            {
                //Because List is a class and if I set something to a class, it works like a pointer
                newComponents.Add(components[i]);
            }

            bool isSpeechMarks = false;
            for (int i = 0; i < newComponents.Count; i++)
            {
                //it's inside of brackets, that will be solved seperatly
                //This also ensures that the rest of the code is operating on the same scope as intended
                if (newComponents[i].StartsWith("(") && newComponents[i].EndsWith(")"))
                    continue;

                //Since it's operating on components, anything with a " in it is a string
                if (!newComponents[i].Contains("\""))
                {
                    if (newComponents[i].Contains(","))
                        //It's got a comma inside the string, that's cool
                        continue;

                    if (Function.functions.ContainsKey(newComponents[i]))
                        continue;
                }

                //It's only one character long, don't bother iterating
                if (newComponents[i].Length == 1)
                {
                    //If the current string is a ", continue
                    if (newComponents[i][0] == '"')
                    {
                        isSpeechMarks = !isSpeechMarks;
                        continue;
                    }

                    if (!Variables.variables.ContainsKey(newComponents[i][0]))
                        //If it's not a variable, that's cool too
                        continue;

                    //It is a variable. Aha!
                    newComponents[i] = Variables.variables[newComponents[i][0]].ToMathematicalString();

                    //Ensures that it's not OOB
                    if (i != 0)
                    {
                        //If the previous component is a complex number
                        if (ParseComplex.TryParse(newComponents[i - 1], out _))
                            //Put a times symbol before the current index but after the number
                            newComponents.Insert(i, "*");
                    }
                }

                //The equation is multiple characters long!
                else
                {
                    string newEquationInnerds = ""; //Replacing the current line with a new one
                    for (int j = 0; j < newComponents[i].Length; j++)
                    {
                        //It's a speech mark. ignore everything until it hits another one
                        if (newComponents[i][j] == '"')
                        {
                            isSpeechMarks = !isSpeechMarks;
                            newEquationInnerds += newComponents[i][j];
                            continue;
                        }

                        if (isSpeechMarks)
                        {
                            //Speech marks are treated as strings and should not be replaced with variables
                            newEquationInnerds += newComponents[i][j];
                            continue;
                        }

                        //It's actually a variable that isn't in speech marks.
                        //Don't worry about if it's inside some brackets, that's been handled

                        if (Variables.variables.ContainsKey(newComponents[i][j]))
                        {
                            //The current index isn't 0, fixed OOB exceptions
                            if (j > 0)
                            {
                                if (!Operator.operators.ContainsKey(newEquationInnerds[j - 1]))
                                    //If the character before me is not an operator
                                    if (!newEquationInnerds.EndsWith("*"))
                                        //And it doesn't end with a *
                                        newEquationInnerds += "*"; //Add a times symbol

                                //remember that we haven't yet added the variable. that comes in later
                            }

                            //add the variable. The ordering is required.
                            newEquationInnerds += Variables.variables[newComponents[i][j]].ToMathematicalString();

                            if (j < newComponents[i].Length - 1)
                            {
                                if (!Operator.operators.ContainsKey(newComponents[i][j + 1]))
                                    //The next character isn't an operator, it probably wants a times symbol
                                    newEquationInnerds += "*";
                            }
                        }

                        //It's not even a variable. pathetic
                        else
                            newEquationInnerds += newComponents[i][j];
                    }

                    //if no change occured, ignore it.
                    if (newComponents[i] == newEquationInnerds)
                        continue;

                    //Adds a bracket around it because why not
                    newComponents[i] = $"({newEquationInnerds})";
                }
            }

            //Done :)
            return newComponents;
        }

        public Equation(List<string> components)
        {
            this.components = components;
        }

        public string Solve()
        {
            //Solve variables first
            List<string> componentsSolvedVars = ResolveVariables();

            if (componentsSolvedVars.Count == 0)
                //Empty = 0
                return "0";

            if (componentsSolvedVars[0] == "-")
                //If it starts with a -, add a zero
                componentsSolvedVars.Insert(0, "0");

            //Solve parenthesis first
            for (int i = 0; i < componentsSolvedVars.Count; i++)
            {
                if (componentsSolvedVars[i].StartsWith('(') && componentsSolvedVars[i].EndsWith(')'))
                {
                    if (componentsSolvedVars[i].Contains(","))
                        continue;

                    //Solve nested equations here
                    string newEq = new Equation(componentsSolvedVars[i][1..^1]).Solve();
                    componentsSolvedVars[i] = newEq;
                }
            }

            for (int i = 0; i < componentsSolvedVars.Count; i++)
            {
                if (componentsSolvedVars[i].StartsWith('('))
                {
                    //It's not a method, it's a bit of code inside parenthesis
                    componentsSolvedVars[i] = new Equation(componentsSolvedVars[i]).Solve();
                }

                if (!Function.functions.ContainsKey(componentsSolvedVars[i]))
                    continue;

                //Used for functions::
                string[] functionParamsString = GetParamsOf(componentsSolvedVars[i + 1]);

                //Each param is it's own equation
                for (int j = 0; j < functionParamsString.Length; j++)
                {
                    string newStr = new Equation(functionParamsString[j]).Solve();
                    if (newStr.StartsWith("\"") && newStr.EndsWith("\""))
                        newStr = newStr[1..^1];

                    functionParamsString[j] = newStr;
                }

                //Done, replace the index with the answer and remove the parameters
                componentsSolvedVars[i] = Function.functions[componentsSolvedVars[i]].operation(functionParamsString);
                componentsSolvedVars.RemoveAt(i + 1);
                //i--;
            }

            //Solve the operators, in bidmas order
            for (int bidmasIndex = 0; bidmasIndex < Operator.highestBidmas; bidmasIndex++)
            {
                //Start with the lowest bidmas order (because lower the value the higher the order)
                //^ = 1, * = 2, + = 3, ...

                for (int i = 0; i < componentsSolvedVars.Count; i++)
                {
                    //The reason we need this check as well as the one after it, is because -1 technically starts with an operator
                    if (componentsSolvedVars[i].Length != 1)
                        continue;

                    if (!Operator.operators.TryGetValue(componentsSolvedVars[i][0], out Operator oper))
                        continue;

                    if (oper.bidmasIndex != bidmasIndex)
                        //It's the wrong order
                        continue;

                    Complex a = Complex.NaN, b = Complex.NaN;
                    //Starts at NaN, ends with the number (if it exists - factorials only have 1)

                    if (i - 1 >= 0)
                        _ = ParseComplex.TryParse(componentsSolvedVars[i - 1], out a);
                    if (componentsSolvedVars.Count > i + 1)
                        _ = ParseComplex.TryParse(componentsSolvedVars[i + 1], out b);

                    componentsSolvedVars[i] = oper.operation(a, b).ToMathematicalString();

                    //Removes the numbers before and after it
                    //If it's a factorial, only remove the one before it
                    switch (oper.direction)
                    {
                        case OperatorDirection.none:
                            break;
                        case OperatorDirection.left:
                            componentsSolvedVars.RemoveAt(i - 1);
                            break;
                        case OperatorDirection.right:
                            componentsSolvedVars.RemoveAt(i + 1);
                            break;
                        case OperatorDirection.both:
                        default:
                            componentsSolvedVars.RemoveAt(i - 1);
                            componentsSolvedVars.RemoveAt(i);
                            break;
                    }

                    //Recalculate equation
                    bidmasIndex = -1;
                    break;
                }
            }

            return componentsSolvedVars[0];
        }

        private static string[] GetParamsOf(string str)
        {
            //Example: (5,3) returns 5, 3
            //It can't just do str.split(',') because of nested methods
            //yup -_-

            if (!str.StartsWith("("))
                str = "(" + str;
            if (!str.EndsWith(")"))
                str += ")";

            int bracketIndentLevel = 0;
            List<string> Params = new();
            Params.Add("");

            for (int i = 1; i < str.Length - 1; i++)
            {
                if (str[i] == '(')
                    bracketIndentLevel++;
                else if (str[i] == ')')
                    bracketIndentLevel--;

                char currentChar = str[i];
                if (bracketIndentLevel == 0 && currentChar == ',')
                {
                    Params.Add("");
                    continue;
                }

                Params[^1] += currentChar;
            }
            return Params.ToArray();
        }

        public Complex SolveComplex() =>
            ParseComplex.Parse(Solve());

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
