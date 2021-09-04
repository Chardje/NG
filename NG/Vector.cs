using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NG
{
    public class Vector
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static Vector operator + (Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static Vector operator *(double s, Vector v)
        {
            return new Vector(s * v.X, s * v.Y, s * v.Z);
        }        
        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y, -v.Z);
        }

        /// <summary>
        /// Length of the vector
        /// </summary>
        public double Length => System.Math.Sqrt(this * this);

        /// <summary>
        /// Normalized vector, collinear to the current
        /// </summary>
        public Vector Norm
        {
            get
            {
                double l = Length;
                return new Vector(X / l, Y / l, Z / l);
            }
        }

        /// <summary>
        /// Scalar Product
        /// </summary>
        /// <param name="v1">1st vector</param>
        /// <param name="v2">2nd vector</param>
        /// <returns>Scalar Product</returns>
        public static double operator * (Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
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

        public Camera(Vector P, Vector D)
        {
            position = P;
            direction = D;
        }
        public void RenderTo(MainWindow window)
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
                    Ray ray = new Ray(position, new Vector(direction.X + w - width / 2, direction.Y + h - height / 2, direction.Z));
                    { 
                        int baseindex = 4 * (h * width + w);
                        array[baseindex + 0] = 0;
                        array[baseindex + 1] = 0;
                        array[baseindex + 2] = 0;
                        array[baseindex + 3] = 255;
                    }
                    double t = double.PositiveInfinity;
                    Object best = null;
                    for (int i = 0; i < window.objects.Count; i++) 
                    {
                        Object obj = window.objects[i];
                        Vector z = obj.center - position;
                        //if (z * direction > 0.9 * direction.Length * z.Length)
                        {
                            if (obj.ObjectInter(ray, out double t0, out double _) && t0 > 0.1 && t > t0)
                            {
                                best = obj;
                                t = t0;
                            }
                        }
                    }
                    if (best != null)
                    {
                        Vector sphereNorm = (t * ray.Direction - best.center).Norm;
                        double product = System.Math.Clamp(-(sphereNorm * ray.Direction.Norm), 0, 1);
                        Debug.Assert(product >= 0 && product <= 1, $"product = {product}");
                        double k = product;
                        int baseindex = 4 * (h * width + w);
                        array[baseindex + 0] = (byte)(best.color.B * k);
                        array[baseindex + 1] = (byte)(best.color.G * k);
                        array[baseindex + 2] = (byte)(best.color.R * k);
                    }
                }
            }
            writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), array, 4 * width, 0);
        }
    }
}
