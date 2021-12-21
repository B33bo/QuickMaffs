using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs.Convert
{
    public static class Storage
    {
        public static string Convert(Complex num, string from, string to)
        {
            Complex fromComplex;
            Complex toComplex;

            if (from.EndsWith("bits"))
                fromComplex = Metric.PrefixToMetricValues[from[..^4]] / 8;
            else if (from.EndsWith("bytes"))
                fromComplex = Metric.PrefixToMetricValues[from[..^5]];
            else
            {
                fromComplex = Metric.PrefixToMetricValues[Metric.ExpandSymbol[from[..^1]]];
                if (from[^1] == 'b')
                    fromComplex /= 8;
            }

            if (to.EndsWith("bits"))
                toComplex = Metric.PrefixToMetricValues[to[..^4]] / 8;
            else if (to.EndsWith("bytes"))
                toComplex = Metric.PrefixToMetricValues[to[..^5]];
            else
            {
                toComplex = Metric.PrefixToMetricValues[Metric.ExpandSymbol[to[..^1]]];
                if (to[^1] == 'b')
                    toComplex /= 8;
            }

            return (num * fromComplex / toComplex).ToMathematicalString();
        }
    }
}
