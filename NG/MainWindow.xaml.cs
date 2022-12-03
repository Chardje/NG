using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;


namespace NG
{
    internal static class Extensions
    {
        internal static int SuperCount(this Dictionary<Vector, Unit> units)
        {
            int result = 0;
            foreach (Unit unit in units.Values)
            {
                result += unit.Count();
            }
            return result;
        }
    }
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Random R = new Random();
        Camera camera = new Camera(new Vector(0, 0, 0), new Vector(0, 0, 500));
        internal Dictionary<Vector, Unit> units = new Dictionary<Vector, Unit>();
        private readonly Stopwatch renderingStopWatch = new Stopwatch();

        private TimeSpan totalRenderingTime = new TimeSpan();
        private int totalCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            #region set timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            timer.Start();
            #endregion
#if true
            units = Distribution(new Sphere(new Vector(0, 0, 0), new Color { R = 63, G = 255, B = 127, }, 100).GenerationObjInCube(new Vector(500, 500, 3500), new Vector(-500, -500, -2000), 0.5));
#else
            List<Object> objects = new List<Object>();
            objects = new Sphere(new Vector(0, 0, 0), new Color { R = 63, G = 255, B = 127, }, 100).GenerationObjInCube(new Vector(500, 500, 3500), new Vector(-500, -500, -2000), 0.3);
            /*
            objects.Add(new Sphere(new Vector(0, 0, 1000), new Color { R = 63, G = 127, B = 255, }, 100));
            objects.Add(new Sphere(new Vector(600, 0, 1000), new Color { R = 127, G = 63, B = 255, }, 50));
            objects.Add(new Sphere(new Vector(0, 150, 1600), new Color { R = 255, G = 127, B = 63, }, 50));
            objects.Add(new Sphere(new Vector(0, -50, 500), new Color { R = 63, G = 255, B = 127, }, 20));
            objects.Add(new Sphere(new Vector(-150, -150, 750), new Color { R = 127, G = 255, B = 63, }, 20));
            objects.Add(new Sphere(new Vector(150, -150, 1600), new Color { R = 255, G = 63, B = 127, }, 20));

            objects.Add(new Sphere(new Vector(-400, -60, 900), new Color { R = 255, G = 0, B = 0, }, 100));
            objects.Add(new Sphere(new Vector(-400,  60, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            */
            /*objects.Add(new Sphere(new Vector(400, 100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(200, 100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(0, 100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(-200, 100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(-400, 100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));

            objects.Add(new Sphere(new Vector(400, 0, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(200, 0, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(0, 0, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(-200, 0, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(-400, 0, 1000), new Color { R = 0, G = 255, B = 0, }, 160));

            objects.Add(new Sphere(new Vector(400, -100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(200, -100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(0, -100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(-200, -100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
            objects.Add(new Sphere(new Vector(-400, -100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));*/
            units = Distribution(objects);
            
#endif
        }

        private void RenewDebugMenu(float fps,int numofObj,int rendObj)
        {
            FPS.Content = "FPS: " + fps;
            NumofObj.Content = "NumofObj: " + numofObj;
            RendObj.Content = "RendObj: " + rendObj;
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            renderingStopWatch.Restart();
            int rendObj = camera.RenderTo(this);
            renderingStopWatch.Stop();
            RenewDebugMenu((float)(1 / renderingStopWatch.Elapsed.TotalSeconds), units.SuperCount(), rendObj);
            totalRenderingTime += renderingStopWatch.Elapsed;
            totalCount += 1;
            Debug.WriteLine($"Rendering: {renderingStopWatch.Elapsed} {totalCount} {totalRenderingTime / totalCount}");
        }
        private Dictionary<Vector, Unit> Distribution(List<Object> obj)
        {
            int rx = 2000;
            int ry = 2000;
            int rz = 3000;
            int size = 500;
            Dictionary<Vector, Unit> D = new Dictionary<Vector, Unit>();
            for (int x = -rx; x < rx; x += size)
            {
                for (int y = -ry; y < ry; y += size)
                {
                    for (int z = -rz; z < rz; z += size)
                    {
                        Vector cord = new Vector(x, y, z);
                        Unit unit = new Unit(cord, size);
                        foreach (Object objItem in obj)
                        {
                            //try add item to unit
                            unit.Add(objItem);
                        }
                        if (unit.Count() > 0) D.Add(cord, unit);

                    }
                }
            }
            return D;
        }
    }
}
