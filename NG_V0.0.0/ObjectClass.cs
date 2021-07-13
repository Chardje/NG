using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Diagnostics;

using System.Windows.Shapes;
using System.Windows.Threading;
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
    class Camera
    {
        public Vector position;
        public Vector direction;
        public int depth;
        
        public Camera(Vector P, Vector D)
        {
            position = P;
            direction = D;
        }
        public void RenderTo(Window1 window)
        {
            
            for (int h=0;h<window.NGGrid.Height;h++)
            {
                for (int w = 0; w < window.NGGrid.Width; w++)
                {
                    window.rectangles[h][w].Fill = new SolidColorBrush(Color.FromRgb((byte)window.R.Next(0, 255), (byte)window.R.Next(0, 255), (byte)window.R.Next(255)));
                }
            }
        }
    }
}
