using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs
{
    public class InvalidEquation : Exception
    {
        public InvalidEquation()
        {
        }

        public InvalidEquation(string message)
            : base(message)
        {
        }

        public InvalidEquation(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
