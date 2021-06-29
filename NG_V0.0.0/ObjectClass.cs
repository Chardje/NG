using System;
using System.Collections.Generic;
using System.Text;

namespace NG_V0._0._0
{
    class Object
    {
        Vector center;
        Vector vel;
        Vector spin;
        long mass;
        int Radius;
        List<Object> constituents;
        internal Object()
        {

        }
        bool CollidesWith(Object b)
        {
            return Math.Sqrt((center - b.center).x) + Math.Sqrt((center - b.center).y) + Math.Sqrt((center - b.center).z) <= Math.Sqrt(Radius + b.Radius);
        }
    }
    class Atom : Object
    {
        Atom():base()
        {

        }
    }

    class Vector
    {
        public long x;
        public long y;
        public long z;
        public Vector(long x, long y, long z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

        }
        public static Vector operator + (Vector x, Vector y)
        {
            return new Vector(x.x + y.x, x.y + y.y, x.z + y.z);
        }
        public static Vector operator -(Vector x, Vector y)
        {
            return new Vector(x.x - y.x, x.y - y.y, x.z - y.z);
        }
    }
}
