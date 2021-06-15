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
        public Square squareF = new Square(250, 250, 30);
        public Triangle triangleF = new Triangle(50, 50, 35);

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
            
            NGGrid.Children.Add(squareF.polygon);
            NGGrid.Children.Add(triangleF.polygon);
            squareF.polygon.Fill = new SolidColorBrush(Color.FromRgb((byte)R.Next(0, 255), (byte)R.Next(0, 255), (byte)R.Next(255)));
            triangleF.polygon.Fill = new SolidColorBrush(Color.FromRgb((byte)R.Next(0, 255), (byte)R.Next(0, 255), (byte)R.Next(255)));

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            squareF.Move(squareF.vectormove);
            squareF.Rotate(squareF.rotatemove);
            triangleF.Move(triangleF.vectormove);
            triangleF.Rotate(triangleF.rotatemove);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            squareF.rotatemove = 5/*R.Next(-30, 30)*/;
            //squareF.vectormove = new Vector(R.Next(-10, 10), R.Next(-10, 10));
            triangleF.rotatemove = 5 /*R.Next(-30, 30)*/;
            //triangleF.vectormove = new Vector(R.Next(-10, 10), R.Next(-10, 10));
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
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    x = polygon.Points[i].X - centr.x;
                    y = polygon.Points[i].Y - centr.y;
                    x = (x * Math.Cos(angle * Math.PI / 180)) - (y * Math.Sin(angle * Math.PI / 180));
                    y = (x * Math.Sin(angle * Math.PI / 180)) + (y * Math.Cos(angle * Math.PI / 180));                    
                    polygon.Points[i] = new Point(x + centr.x, y + centr.y);
                }

            }
            public void Move(Vector move)
            {
                centr += move;
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new Point(polygon.Points[i].X + move.x, polygon.Points[i].Y + move.y);
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
