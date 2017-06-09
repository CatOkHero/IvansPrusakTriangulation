using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varification
{
    public static class ExactSolution
    {

        public static double GetSolutionValue(double x, double aCoef, double d, double f, double a)
        {
            if (d == 0)
            {
                return -f / aCoef * x * x / 2.0 + f / aCoef * a / 2.0 * x;
            }
            else if (d < 0)
            {
                double temp = Math.Sqrt(-d / aCoef * x);
                double c1 = -f / d;
                double c2 = f / d * (Math.Cos(Math.Sqrt(-d / aCoef * a)) - 1) / Math.Sin(Math.Sqrt(-d / aCoef * a));
                return c1 * Math.Cos(temp) + c2 * Math.Sin(temp) + f / d;
            }
            else
            {
                double temp = Math.Sqrt(d / aCoef * x);
                double c1 = -f / d / 2.0;
                double c2 = f / 2.0 / d * (1 - Math.Exp(Math.Sqrt(d / aCoef * a))) / (1 - Math.Exp(-Math.Sqrt(d / aCoef * a)));
                return c1 * Math.Exp(temp) + c2 * Math.Exp(-temp) + f / d;
            }
        }



    }
}
