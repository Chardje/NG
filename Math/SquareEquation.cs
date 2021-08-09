using System;
using M = System.Math;
using System.Diagnostics;


namespace NG.Math
{
    
    public static class SquareEquation
    {

        public static bool Solve(double a, double b, double c, out double x0, out double x1)
        {
            double D = b * b - (4 * a * c);
            if (D < 0)
            {
                x0 = double.NaN;
                x1 = double.NaN;
                return false;
            }
            else if (D >= 0)
            {
                double T = M.Sqrt(D);
                double a2 = (1 / a) / 2;
                x0 = (-b - T) * a2;
                x1 = (-b + T) * a2;
            }
            else
            {
                throw new ArgumentException($"a={a} b={b} c={c} D={D}");
            }
            return true;
        }
    }
}
