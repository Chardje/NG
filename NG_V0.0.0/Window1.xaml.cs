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


namespace NG_V0._0._0
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Random R = new Random();
        Camera camera = new Camera(new Vector(0,0,0), new Vector(0, 0, 500));
        internal List<Object> objects = new List<Object>();
        
        public Window1()
        {
            InitializeComponent();
            #region set timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
            #endregion
            objects.Add(new Sphere(new Vector(0, 0, 200), 100));
            objects.Add(new Sphere(new Vector(600, 0, 1000), 50));
            objects.Add(new Sphere(new Vector(0, 150, 1600), 20));
            objects.Add(new Sphere(new Vector(150, 150, 1600), 20));
            objects.Add(new Sphere(new Vector(-150, -150, 750), 20));
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            camera.RenderTo(this);
        }
    }
}
