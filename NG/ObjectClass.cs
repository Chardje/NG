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
        public static readonly Color DefaultColor = new Color { R = 255, G = 255, B = 255 };
        internal Vector center;
        internal readonly Color color;
        //internal double light_emittance;
        internal Object(Vector center)  : this(center, DefaultColor)
        {
            // nothing
        }
        internal Object(Vector center, Color color)
        {
            this.center = center;
            this.color = color;
        }

        internal abstract bool ObjectInter(Ray ray, out double t0, out double t1);

        internal virtual List<Object> GenerationObjInCube(Vector p1, Vector p2, double density)
        {
            List<Object> ListObj = new List<Object>();
            double v = (p1.X - p2.X) * (p1.Y - p2.Y) * (p1.Z - p2.Z);
            for (int i = 0; i < (int)(v / Volume() * density); i++)
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
        public abstract double Volume();
        
    }
    internal class Sphere : Object
    {
        int Radius;
        double V;
        

        internal Sphere(Vector center, int Radius) : this(center, DefaultColor, Radius)
        {
        }
        internal Sphere(Vector center, Color color, int Radius) : base(center, color)
        {
            this.Radius = Radius;
            V = CalculateVolume();
        }
        /// <summary>
        /// whether a sphere and a ray intersect
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="t0">lond of ray 1 to point 1</param>
        /// <param name="t1">lond of ray 2 to point 2</param>
        /// <returns></returns>
        internal override bool ObjectInter(Ray ray,out double t0, out double t1)
        {
            Vector s = center - ray.Origin;
            Vector d = ray.Direction;
            return Math.SquareEquation.Solve(
                d*d,    -2*(d*s),   s*s-(Radius*Radius),out t0, out t1);
        }

        internal bool CollidesWith(Sphere b)
        {
            return M.Sqrt((center - b.center).X) + M.Sqrt((center - b.center).Y) + M.Sqrt((center - b.center).Z) <= M.Sqrt(Radius + b.Radius);
        }
        private double CalculateVolume()
        {
            return 4 / 3 * M.PI * Radius * Radius * Radius;
        }
        public override double Volume()
        {
            return V;
        }
    }



    /*class Electron : Sphere
    {
        long mass;
        Electron(Vector C) : base(C, Color.FromArgb(255, 0, 0, 255), 1)
        {
            mass = 1;
        }
    }
    class Proton : Sphere
    {
        long mass;

        Proton(Vector C) : base(C, Color.FromArgb(255, 255, 0, 0), 45)
        {
            mass = 45;
        }
    }
    class Neitron : Sphere
    {
        long mass;
        Neitron(Vector C) : base(C, Color.FromArgb(255, 0, 255, 0), 45)
        {
            mass = 45;
        }
    }
    /*internal class GroupSpherical
    {
        List<Object> objects;
        Sphere sphere;
        internal GroupSpherical(Sphere s)
        {
            sphere = s;
        }
        internal GroupSpherical(Vector v, int r)
        {
            sphere = new Sphere(v,r);
        }
        internal bool ChekingAdd(Sphere s)
        {
            if (sphere.CollidesWith(s))
            {
                if (!objects.Contains(s))
                {
                    objects.Add(s);
                }
                return true;
            }
            else
            {
                objects.Remove(s);
                return false;
            }
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
