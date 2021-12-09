using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs
{
    public class Equation
    {
        private const string Digits = "01234567890E.";
        private List<string> components = new();

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
                    if (nestIndex == 0 && type != ComponentType.Function)
                        components.Add("");
                    nestIndex++;
                }
                else if (input[i] == ')')
                {
                    nestIndex--;
                    components[^1] += input[i];
                    if (nestIndex == 0)
                    {
                        components.Add("");
                    }
                    continue;
                }

                if (nestIndex > 0)
                {
                    components[^1] += input[i];
                    continue;
                }

                ComponentType newType = TypeDetector(components[^1], input[i]);

                if (type != newType)
                {
                    components.Add("");
                    type = newType;
                }

                components[^1] += input[i];
            }

            components.RemoveAll((a) => a == "");

            static ComponentType TypeDetector(string previous, char newCharacter)
            {
                if (newCharacter == '-')
                {
                    if (previous.Length == 0)
                        return ComponentType.Operator;

                    if (Operator.operators.ContainsKey(previous[0]) || Function.functions.ContainsKey(previous))
                        return ComponentType.Number;

                    return ComponentType.Operator;
                }

                if (Digits.Contains(newCharacter))
                    return ComponentType.Number;

                return Operator.operators.ContainsKey(newCharacter) ? ComponentType.Operator : ComponentType.Function;
            }

            Console.WriteLine(components.Readable());
        }

    }
}
