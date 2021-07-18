using System;
using M = System.Math;

namespace NG_V0._0._0.Math
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
                x0 = (-b - M.Sqrt(D)) / (2 * a);
                x1 = (-b + M.Sqrt(D)) / (2 * a);
            }
            else
            {
                throw new ArgumentException($"a={a} b={b} c={c} D={D}");
            }
            return true;
        }
    }
}
