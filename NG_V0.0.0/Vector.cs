using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NG_V0._0._0
{
    public class Vector
    {
        public double x;
        public double y;
        public double z;
        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static Vector operator + (Vector v1, Vector v2)
        {
            return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }
    }

    class Ray
    {
        public Vector Origin { get; }
        public Vector Direction { get; }
        public Vector NextPoint { get { return Origin + Direction; } }

        public Ray(Vector origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public static Ray ByTwoPoint(Vector origin, Vector nextpoint)
        {
            return new Ray(origin, nextpoint - origin);
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
            for (int h = 0; h < height; h++) 
            {
                for (int w = 0; w < width; w++) 
                {
                    Ray ray = new Ray(position, new Vector(direction.x + w - width / 2, direction.y + h - height / 2, direction.z));
                    {
                        array[4 * (h * width + w) + 0] = 0;
                        array[4 * (h * width + w) + 1] = 0;
                        array[4 * (h * width + w) + 2] = 0;
                        array[4 * (h * width + w) + 3] = 255;
                    }
                    double t = double.PositiveInfinity;
                    for (int i = 0; i < window.objects.Count; i++) 
                    {
                        if (window.objects[i].ObjectInter(ray,out double t0, out double t1) && (t1>0||t0>0) && t > t0)
                        {    
                            t = t0;                            
                            double k = 1d / (t0 + 1d);
                            array[4 * (h * width + w) + 0] = (byte)(((int)(255 * k))%2 * 255);
                            array[4 * (h * width + w) + 1] = (byte)(((int)(255 * k)) % 2 * 191); ;
                            array[4 * (h * width + w) + 2] = (byte)(((int)(255 * k)) % 2 * 0); ;
                            array[4 * (h * width + w) + 3] = 255;
                        }
                    }
                }
            }
            writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), array, 4 * width, 0);
        }
    }
}
