using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMaffs
{
    public class Graph
    {
        public Equation Equation { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; }

        public Vector2 Min
        {
            get => new(Origin.X - Scale.X, Origin.Y - Scale.X);
        }

        public Vector2 Max
        {
            get => new(Origin.X + Scale.Y, Origin.Y + Scale.Y);
        }

        public Graph(Equation equation)
        {
            Equation = equation;
            Origin = new(0, 0);
            Scale = new(100, 100);
        }

        public Graph(Equation equation, Vector2 origin, Vector2 scale)
        {
            Equation = equation;
            Origin = origin;
            Scale = scale;
        }

        public override string ToString() =>
            Equation.ToString();

        public Bitmap Plot()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return null;

            if (Equation.IsBoolean)
                return null;

            return LinePlot();
        }

        private Bitmap LinePlot()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return null;

            Bitmap bmp = new((int)Scale.X, (int)Scale.Y);

            Variables.variables.Add('x', 0);
            for (float i = 0; i < bmp.Width; i ++)
            {
                Variables.variables['x'] = i;
                int val = (int)Equation.SolveComplex().Real;

                if (val >= bmp.Height)
                    val = bmp.Height - 1;

                bmp.SetPixel((int)i, val, Color.Black);
            }

            return bmp;
        }
    }
}
