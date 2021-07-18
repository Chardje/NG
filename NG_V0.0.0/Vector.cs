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
