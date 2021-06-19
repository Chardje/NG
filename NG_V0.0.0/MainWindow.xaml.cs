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
        const byte tttos = 1;//teleport to the other side 
        const byte miror = 2; 


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
            timer1.Interval = new TimeSpan(0, 0, 0, 30);
            //timer1.Start();
            #endregion

            for (int i = 0; i < 7; i++) 
            {
                figures.Add(new Square(R.Next(800), R.Next(450), R.Next(20, 60)));
                figures.Add(new Triangle(R.Next(800), R.Next(450), R.Next(20, 60)));
                figures.Add(new EllipseF(R.Next(800), R.Next(450), R.Next(20, 60)));

            }
            for (int i = 0; i < figures.Count; i++)
            {
                NGGrid.Children.Add(figures[i].Shape);
                figures[i].Shape.Fill = new SolidColorBrush(Color.FromRgb((byte)R.Next(0, 255), (byte)R.Next(0, 255), (byte)R.Next(255)));
            }

            timer1_Tick(null, null);
            //el.RenderTransform = new RotateTransform(10);
            
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
            for (int i = 0; i<figures.Count;i++)
            {
                figures[i].rotatemove = R.Next(-30, 30);
                figures[i].vectormove = new Vector(R.Next(-5, 5), R.Next(-5, 5));
            }
        }

        public abstract class Figure
        {
            public Vector centr;
            public Vector vectormove;
            public int rotatemove;
            private byte border;
            public abstract Shape Shape { get; }

            public Figure(int x, int y)
            {
                centr = new Vector(x, y);
                vectormove = new Vector(0,0);
                border = miror;
            }
            public abstract void Rotate(int angle);
            public abstract void Move(Vector move);
            public void ReactBorder()
            {
                switch (border)
                {
                    case 0:

                        break;
                    case 1:

                        if (centr.x < 0) { centr.x += 800; }
                        else if (centr.x > 800) { centr.x -= 800; }

                        if (centr.y < 0) { centr.y += 450; }
                        else if (centr.y > 450) { centr.y -= 450; }

                        break;

                    case 2:

                        if (centr.x < 0) { centr.x = 0; vectormove.x *= -1; }
                        else if (centr.x > 800) { centr.x = 800; vectormove.x *= -1; }

                        if (centr.y < 0) { centr.y = 0; vectormove.y *= -1; }
                        else if (centr.y > 450) { centr.y = 450; vectormove.y *= -1; }

                        break;
                }


                if (centr.x < 0) { centr.x += 800; }
                else if (centr.x > 800) { centr.x -= 800; }

                if (centr.y < 0) { centr.y += 450; }
                else if (centr.y > 450) { centr.y -= 450; }
            }
        }

        public class EllipseF : Figure
        {
            private readonly Ellipse _figura;
            int rotateAngle;

            public EllipseF(int x, int y, int diameter) : base(x, y)
            {
                _figura = new Ellipse();
                _figura.Width = diameter;
                _figura.Height = diameter;
                _figura.HorizontalAlignment = HorizontalAlignment.Left;
                _figura.VerticalAlignment = VerticalAlignment.Top;
                _figura.Margin = new Thickness(x - _figura.Width / 2, y - _figura.Height / 2,0,0);
            }                        

            public override Shape Shape => _figura;

            public override void Move(Vector move)
            {
                centr += move;
                ReactBorder();
                _figura.Margin = new Thickness(centr.x - _figura.Width/2, centr.y - _figura.Height/2, 0, 0);
            }

            public override void Rotate(int angle)
            {
                rotateAngle += angle;
                _figura.RenderTransform = new RotateTransform(10);


            }
        }

        public class PoligonF : Figure
        {
            private readonly Polygon _figura;

            public PoligonF(int x, int y, Polygon figura) : base(x, y)
            {
                _figura = figura;
            }

            public override Shape Shape => _figura;

            public override void Rotate(int angle) 
            {
                double x;
                double y;
                double sin = Math.Sin(angle * Math.PI / 180);
                double cos = Math.Cos(angle * Math.PI / 180);
                for (int i = 0; i < _figura.Points.Count; i++)
                {
                    x = (_figura.Points[i].X - centr.x) * cos - ((_figura.Points[i].Y - centr.y) * sin);
                    y = (_figura.Points[i].X - centr.x) * sin + ((_figura.Points[i].Y - centr.y) * cos);
                    _figura.Points[i] = new Point(x + centr.x, y + centr.y);
                }

            }

            public override void Move(Vector move)
            {
                for (int i = 0; i < _figura.Points.Count; i++)
                {
                    _figura.Points[i] = new Point(_figura.Points[i].X - centr.x, _figura.Points[i].Y - centr.y);
                }

                centr += move;
                ReactBorder();

                for (int i = 0; i < _figura.Points.Count; i++)
                {
                    _figura.Points[i] = new Point(_figura.Points[i].X + centr.x, _figura.Points[i].Y + centr.y);
                }
            }
        }
        public class Square : PoligonF
        {
            public Square(int x, int y, int side) : base(x, y, NewSquare(x, y, side)) {}

            static Polygon NewSquare(int x, int y, int side)
            {
                Polygon figura = new Polygon();
                figura.Points.Add(new Point(x - side / 2, y - side / 2));
                figura.Points.Add(new Point(x + side / 2, y - side / 2));
                figura.Points.Add(new Point(x + side / 2, y + side / 2));
                figura.Points.Add(new Point(x - side / 2, y + side / 2));
                return figura;
            }
        }

        public class Triangle : PoligonF
        {
            public Triangle(int x, int y, int side) : base(x, y, NewTriangle(x, y, side)) {}

            static Polygon NewTriangle(int x, int y, int side)
            {
                Polygon figura = new Polygon();
                figura.Points.Add(new Point(x, y - (2 * side / 3)));
                figura.Points.Add(new Point(x + side / 2, y + (side / 3)));
                figura.Points.Add(new Point(x - side / 2, y + (side / 3)));
                return figura;
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
