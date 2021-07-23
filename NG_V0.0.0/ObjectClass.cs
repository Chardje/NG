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
        internal Vector center;
        internal Vector vel;
        internal Vector spin;
        internal long mass;
        internal List<Object> constituents;
        internal Brush color;
        internal Object(Vector center)
        {
            this.center = center;
        }

        internal abstract bool ObjectInter(Ray ray, out double t0, out double t1);
    }
    internal class Sphere : Object
    {
        int Radius;
        internal Sphere(Vector center,int Radius):base(center)
        {
            this.Radius = Radius;
        }

        internal override bool ObjectInter(Ray ray,out double t0, out double t1)
        {
            double x0 = center.x;
            double y0 = center.y;
            double z0 = center.z;
            double x1 = ray.Origin.x;
            double y1 = ray.Origin.y;
            double z1 = ray.Origin.z;
            double x2_x1 = ray.Direction.x;
            double y2_y1 = ray.Direction.y;
            double z2_z1 = ray.Direction.z;

            return Math.SquareEquation.Solve(
                M.Pow(x2_x1, 2) + M.Pow(y2_y1, 2) + M.Pow(z2_z1, 2),
                2 * (x2_x1 * (x1 - x0) + y2_y1 * (y1 - y0) + z2_z1 * (z1 - z0)),
                M.Pow(x1 - x0, 2) + M.Pow(y1 - y0, 2) + M.Pow(z1 - z0, 2) - M.Pow(Radius, 2),
                out t0, out t1) ; ;
        }

        bool CollidesWith(Sphere b)
        {
            return M.Sqrt((center - b.center).x) + M.Sqrt((center - b.center).y) + M.Sqrt((center - b.center).z) <= M.Sqrt(Radius + b.Radius);
        }
    }
    class Electron : Sphere
    {
        Electron(Vector C):base(C,1)
        {
            mass = 1;
            color = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));

        }
    }
    class Proton : Sphere
    {
        Proton(Vector C) : base(C, 45)
        {
            mass = 45;
            color = new SolidColorBrush(Color.FromArgb(255,255,0,0));
        }
    }
    class Neitron : Sphere
    {
        Neitron(Vector C) : base(C, 45)
        {
            mass = 45;
            color = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));

        }
    }
}
