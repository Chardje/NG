using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Diagnostics;

using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows;

namespace NG_V0._0._0
{
    public abstract class Object
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
        
    }
    public class Sphere : Object
    {
        int Radius;
        public Sphere(Vector center,int Radius):base(center)
        {
            this.Radius = Radius;
        }
        bool CollidesWith(Sphere b)
        {
            return Math.Sqrt((center - b.center).x) + Math.Sqrt((center - b.center).y) + Math.Sqrt((center - b.center).z) <= Math.Sqrt(Radius + b.Radius);
        }
    }
    
    public class Vector
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
            int width = (int)window.BackgroundImage.Width;
            int height = (int)window.BackgroundImage.Height;
            WriteableBitmap writeableBitmap;
            if (window.BackgroundImage.Source is WriteableBitmap wb)
            {
                writeableBitmap = wb;
            }
            else
            {
                writeableBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
                window.BackgroundImage.Source = writeableBitmap;
            }
            byte[] array = new byte[width * height * 4];
            for (int i = 0; i < array.Length / 4; i += 1)
            {
                array[4 * i + 0] = (byte)window.R.Next(0, 256);
                array[4 * i + 1] = (byte)window.R.Next(0, 256);
                array[4 * i + 2] = (byte)window.R.Next(0, 256);
                array[4 * i + 3] = 255;
            }
            writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), array, 4 * width, 0);
        }
    }
}
