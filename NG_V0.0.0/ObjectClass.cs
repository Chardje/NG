using System;
using System.Collections.Generic;
using NG_V0._0._0.Math;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using M = System.Math;


namespace NG_V0._0._0
{
    internal abstract class Object
    {
        internal readonly Vector center;
        internal Vector vel;
        internal Vector spin;
        internal long mass;
        internal List<Object> constituents;
        internal readonly Color color;
        internal Object(Vector center)  : this(center, new Color { R = 255, G = 255, B = 255 })
        {
            // nothing
        }
        internal Object(Vector center, Color color)
        {
            this.center = center;
            this.color = color;
        }

        internal abstract bool ObjectInter(Ray ray, out double t0, out double t1);
    }
    internal class Sphere : Object
    {
        int Radius;
        internal Sphere(Vector center, int Radius) : base(center)
        {
            this.Radius = Radius;
        }
        internal Sphere(Vector center, Color color, int Radius) : base(center, color)
        {
            this.Radius = Radius;
        }

        internal override bool ObjectInter(Ray ray,out double t0, out double t1)
        {
            double x0 = center.X;
            double y0 = center.Y;
            double z0 = center.Z;
            double x1 = ray.Origin.X;
            double y1 = ray.Origin.Y;
            double z1 = ray.Origin.Z;
            double x2_x1 = ray.Direction.X;
            double y2_y1 = ray.Direction.Y;
            double z2_z1 = ray.Direction.Z;

            return Math.SquareEquation.Solve(
                M.Pow(x2_x1, 2) + M.Pow(y2_y1, 2) + M.Pow(z2_z1, 2),
                2 * (x2_x1 * (x1 - x0) + y2_y1 * (y1 - y0) + z2_z1 * (z1 - z0)),
                M.Pow(x1 - x0, 2) + M.Pow(y1 - y0, 2) + M.Pow(z1 - z0, 2) - M.Pow(Radius, 2),
                out t0, out t1) ; ;
        }

        bool CollidesWith(Sphere b)
        {
            return M.Sqrt((center - b.center).X) + M.Sqrt((center - b.center).Y) + M.Sqrt((center - b.center).Z) <= M.Sqrt(Radius + b.Radius);
        }
    }
}
