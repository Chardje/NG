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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Random R = new Random();
        Camera camera = new Camera(new Vector(0,0,0), new Vector(0, 0, 500));
        internal List<Object> objects = new List<Object>();
        private readonly Stopwatch renderingStopWatch = new Stopwatch();

        private TimeSpan totalRenderingTime = new TimeSpan();
        private int totalCount = 0;
        
        public Window1()
        {
            InitializeComponent();
            #region set timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            timer.Start();
            #endregion
            /*objects.Add(new Sphere(new Vector(0, 0, 1000), new Color { R = 63, G = 127, B = 255, }, 100));
            objects.Add(new Sphere(new Vector(600, 0, 1000), new Color { R = 127, G = 63, B = 255, }, 50));
            objects.Add(new Sphere(new Vector(0, 150, 1600), new Color { R = 255, G = 127, B = 63, }, 50));
            objects.Add(new Sphere(new Vector(0, -50, 500), new Color { R = 63, G = 255, B = 127, }, 20));
            objects.Add(new Sphere(new Vector(-150, -150, 750), new Color { R = 127, G = 255, B = 63, }, 20));
            objects.Add(new Sphere(new Vector(150, -150, 1600), new Color { R = 255, G = 63, B = 127, }, 20));

            objects.Add(new Sphere(new Vector(-400, -60, 900), new Color { R = 255, G = 0, B = 0, }, 100));
            objects.Add(new Sphere(new Vector(-400,  60, 1000), new Color { R = 0, G = 255, B = 0, }, 160));*/
            //objects = new Sphere(new Vector(0, 0, 0), new Color { R = 63, G = 255, B = 127, }, 100).GenerationObjInCube(objects, new Vector(400, 200, 1500), new Vector(-400, -200, 500), 0.3);

            objects.Add(new Sphere(new Vector(400, 100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));
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
            objects.Add(new Sphere(new Vector(-400, -100, 1000), new Color { R = 0, G = 255, B = 0, }, 160));

        }

        private void RenewDebugMenu(ushort fps)
        {
            FPS.Content = "FPS: " + fps;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            renderingStopWatch.Restart();
            camera.RenderTo(this);
            renderingStopWatch.Stop();
          //RenewDebugMenu(Convert.ToUInt16(1 / renderingStopWatch.Elapsed.TotalSeconds));
            totalRenderingTime += renderingStopWatch.Elapsed;
            totalCount += 1;
            Debug.WriteLine($"Rendering: {renderingStopWatch.Elapsed} {totalCount} {totalRenderingTime/totalCount}");
        }
    }
}
