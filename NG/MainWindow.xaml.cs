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

            units = Distribution(new Sphere(new Vector(0, 0, 0), new Color { R = 63, G = 255, B = 127, }, 100).GenerationObjInCube(new Vector(450, 200, 3500), new Vector(-450, -200, 500), 0.3));
        }

        private void RenewDebugMenu(float fps)
        {
            FPS.Content = "FPS: " + fps;
            NofObj.Content = "NofObj: " + units.Count;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            renderingStopWatch.Restart();
            camera.RenderTo(this);
            renderingStopWatch.Stop();
            RenewDebugMenu((float)(1 / renderingStopWatch.Elapsed.TotalSeconds));
            totalRenderingTime += renderingStopWatch.Elapsed;
            totalCount += 1;
            Debug.WriteLine($"Rendering: {renderingStopWatch.Elapsed} {totalCount} {totalRenderingTime / totalCount}");
        }
        private Dictionary<Vector, Unit> Distribution(List<Object> obj)
        {
            int r = 150;
            Dictionary<Vector, Unit> D = new Dictionary<Vector, Unit>();
            for (int x = -r; x < r; x++)
                for (int y = -r; y < r; y++)
                    for (int z = -r; z < r; z++)
                    {
                        D.Add(new Vector(x, y, z), new Unit(new Vector(x, y, z), 150));
                        foreach (Object objItem in obj)
                        {
                            D[new Vector(x, y, z)].Add(objItem);
                        }
                    }
            return D;
        }
    }
}
