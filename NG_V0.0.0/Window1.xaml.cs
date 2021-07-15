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
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Threading;


namespace NG_V0._0._0
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public List <List<Rectangle>> rectangles = new List<List<Rectangle>>();
        public Random R = new Random();
        Camera camera = new Camera(new Vector(0,0,0), new Vector(0, 0,0));
        public Window1()
        {
            InitializeComponent();
            #region set timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 5, 0);
            timer.Start();
            #endregion

            for (int h = 0; h < NGGrid.Height; h++) 
            {
                List<Rectangle> r = new List<Rectangle>();
                for (int w = 0; w < NGGrid.Width; w++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = 1;
                    rectangle.Height = 1;
                    rectangle.HorizontalAlignment = HorizontalAlignment.Left;
                    rectangle.VerticalAlignment = VerticalAlignment.Top;
                    rectangle.Margin = new Thickness(w,h,0,0);
                    r.Add(rectangle);

                }
                rectangles.Add(r);
            }
            for (int h = 0; h < NGGrid.Height; h++)
            {
                for (int w = 0; w < NGGrid.Width; w++)
                {
                    NGGrid.Children.Add(rectangles[h][w]);
                }
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            camera.RenderTo(this);
        }
    }
}
