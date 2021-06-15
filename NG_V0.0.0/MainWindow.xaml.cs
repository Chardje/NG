using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NG_V0._0._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Random R = new Random();
        public List<Figure> figures = new List<Figure>();

        public MainWindow()
        {
            InitializeComponent();
            #region set timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            timer.Start();
            #endregion

            #region set timer1
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = new TimeSpan(0, 0, 0, 5);
            timer1.Start();
            #endregion

            for (int i = 0; i < 7; i++) 
            {
                figures.Add(new Square(R.Next(800), R.Next(450), R.Next(20,60)));
                NGGrid.Children.Add(figures[figures.Count - 1].polygon);
                figures[figures.Count - 1].polygon.Fill = new SolidColorBrush(Color.FromRgb((byte)R.Next(0, 255), (byte)R.Next(0, 255), (byte)R.Next(255)));
            }
            for (int i = 0; i < 7; i++)
            {
                figures.Add(new Triangle(R.Next(800), R.Next(450), R.Next(20, 60)));
                NGGrid.Children.Add(figures[figures.Count-1].polygon);
                figures[figures.Count - 1].polygon.Fill = new SolidColorBrush(Color.FromRgb((byte)R.Next(0, 255), (byte)R.Next(0, 255), (byte)R.Next(255)));
            }

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < figures.Count; i++)
            {
                figures[i].Move(figures[i].vectormove);
                figures[i].Rotate(figures[i].rotatemove);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i =0; i<figures.Count;i++)
            {
                figures[i].rotatemove = R.Next(-30, 30);
                figures[i].vectormove = new Vector(R.Next(-5, 5), R.Next(-5, 5));
            }
        }

        public class Figure
        {
            public Vector centr;
            public Vector vectormove;
            public int rotate;
            public int rotatemove;
            public Polygon polygon;
            public int side;
            public Figure(int x, int y, int side)
            {
                centr = new Vector(x, y);
                this.side = side;
                vectormove = new Vector(0,0);
            }
            public void Rotate(int angle)
            {
                double x;
                double y;
                rotate += angle;
                List<Vector> list = new List<Vector>();
                double sin = Math.Sin(angle * Math.PI / 180);
                double cos = Math.Cos(angle * Math.PI / 180);
                for (int i = 0; i < polygon.Points.Count; i++)
                {      
                    x = (polygon.Points[i].X - centr.x) * cos - ((polygon.Points[i].Y - centr.y) * sin);
                    y = (polygon.Points[i].X - centr.x) * sin + ((polygon.Points[i].Y - centr.y) * cos);                    
                    polygon.Points[i] = new Point(x + centr.x, y + centr.y);
                }

            }
            public void Move(Vector move)
            {
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new Point(polygon.Points[i].X - centr.x, polygon.Points[i].Y - centr.y);
                }
                centr += move;
                if (centr.x < 0) { centr.x += 800; }
                else if (centr.x > 800) { centr.x = 800 - centr.x; }

                if (centr.y < 0) { centr.y += 430; }
                else if (centr.y > 450) { centr.y = 450 - centr.y; }

                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new Point(polygon.Points[i].X + centr.x, polygon.Points[i].Y + centr.y);
                }
            }
        }
        public class Square : Figure
        {
            public Square(int x, int y, int side) : base(x,y,side)
            {
                polygon = new Polygon();
                this.polygon.Points.Add(new Point(x - side / 2, y - side / 2));
                this.polygon.Points.Add(new Point(x + side / 2, y - side / 2));
                this.polygon.Points.Add(new Point(x + side / 2, y + side / 2));
                this.polygon.Points.Add(new Point(x - side / 2, y + side / 2));
            }
            
            
        }
        public class Triangle : Figure
        {
            public Triangle(int x, int y, int side) : base(x, y,side)
            {
                polygon = new Polygon();
                this.polygon.Points.Add(new Point(x, y - (2 * side / 3)));
                this.polygon.Points.Add(new Point(x + side / 2, y + (side / 3)));
                this.polygon.Points.Add(new Point(x - side / 2, y + (side / 3)));
            }
        }

        public class Vector
        {
            public int x, y;
            public Vector(int x,int y)
            {
                this.x = x;
                this.y = y;
            }
            public static Vector operator + (Vector x, Vector y)
            {
                return new Vector(x.x + y.x, x.y + y.y);
            }
        }

    }
}
