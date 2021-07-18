using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace NG_V0._0._0
{
    internal abstract class Object
    {
        internal Vector center;
        internal Vector vel;
        internal Vector spin;
        internal long mass;
        internal List<Object> constituents;
        internal Object(Vector center)
        {
            this.center = center;
        }

        internal abstract bool ObjectInter(Ray ray);
    }
    internal class Sphere : Object
    {
        int Radius;
        internal Sphere(Vector center,int Radius):base(center)
        {
            this.Radius = Radius;
        }

        internal override bool ObjectInter(Ray ray)
        {
            int x0 = (int)center.x;
            int y0 = (int)center.x;
            int z0 = (int)center.x;

            for (int i=0;i<500;i++)
            {
            }
            return false;
        }

        bool CollidesWith(Sphere b)
        {
            return Math.Sqrt((center - b.center).x) + Math.Sqrt((center - b.center).y) + Math.Sqrt((center - b.center).z) <= Math.Sqrt(Radius + b.Radius);
        }
    }
}
