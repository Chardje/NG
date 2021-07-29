using System;
using System.Collections.Generic;
using NG.Math;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using M = System.Math;


namespace NG
{
    internal abstract class Object : ICloneable
    {
        internal Vector center;
        internal long mass;
        internal readonly Color color;
        internal double V;
        //internal double light_emittance;
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

        internal virtual List<Object> GenerationObjInCube(List<Object> ListObj, Vector p1, Vector p2, double density)
        {
            double v = (p1.X - p2.X) * (p1.Y - p2.Y) * (p1.Z - p2.Z);
            for (int i = 0; i < (int)(v / this.V * density); i++)
            {
                Object O = (Object)Clone();
                O.center = new Vector(new Random().Next((int)p2.X, (int)p1.X), new Random().Next((int)p2.Y, (int)p1.Y), new Random().Next((int)p2.Z, (int)p1.Z));
                ListObj.Add(O);
            }
            return ListObj;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    internal class Sphere : Object
    {
        int Radius;
        internal Sphere(Vector center, int Radius) : base(center)
        {
            this.Radius = Radius;
            V = 4 / 3 * M.PI * Radius * Radius * Radius;
        }
        internal Sphere(Vector center, Color color, int Radius) : base(center, color)
        {
            this.Radius = Radius;
            V = 4 / 3 * M.PI * Radius * Radius * Radius;

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
                out t0, out t1) ; 
        }

        bool CollidesWith(Sphere b)
        {
            return M.Sqrt((center - b.center).X) + M.Sqrt((center - b.center).Y) + M.Sqrt((center - b.center).Z) <= M.Sqrt(Radius + b.Radius);
        }
    }
    class Electron : Sphere
    {
        Electron(Vector C) : base(C, Color.FromArgb(255, 0, 0, 255), 1)
        {
            mass = 1;
        }
    }
    class Proton : Sphere
    {
        Proton(Vector C) : base(C, Color.FromArgb(255, 255, 0, 0), 45)
        {
            mass = 45;
        }
    }
    class Neitron : Sphere
    {
        Neitron(Vector C) : base(C, Color.FromArgb(255, 0, 255, 0), 45)
        {
            mass = 45;
        }
    }
    /*class Star : Sphere
    {
        Star(Vector C, int R, double LE, int T) : base(C, color, R)
        {
            light_emittance = LE;
            Color color;
            if (T<2000)
            {
                throw new ArgumentException($"T < 2000, T={T}");
            }
            else if (T >= 2000 && T<3500)
            {
                color = Color.FromArgb(255, 255, 160, 0);
            }
            else if (T >= 3500 && T < 5000)
            {
                color = Color.FromArgb(255, 255, 200, 0);
            }
            else if (T >= 5000 && T < 6000)
            {
                color = Color.FromArgb(255, 255, 255, 0);
            }
            else if (T >= 6000 && T < 7500)
            {
                color = Color.FromArgb(255, 255, 255, 255);
            }
            else if (T >= 7500 && T < 10000)
            {
                color = Color.FromArgb(255, 255, 255, 255);
            }
            else if (T >= 10000 && T < 30000)
            {
                color = Color.FromArgb(255, 0, 255, 255);
            }
            else if (T >= 30000 && T < 60000)
            {
                color = Color.FromArgb(255, 0, 204, 255);
            }
            else 
            {
                throw new ArgumentException($"T > 60000, T={T}");
            }
        }
    }*/
}
