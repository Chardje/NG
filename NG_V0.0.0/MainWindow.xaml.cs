using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly SolidColorBrush brushForUncollided = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        private readonly SolidColorBrush brushForCollided = new SolidColorBrush(Color.FromRgb(255, 0, 0));
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

            for (int i = 0; i < 6; i++) 
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
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < figures.Count; i++)
            {
                figures[i].Move(figures[i].vectormove);
                figures[i].Rotate(figures[i].rotatemove);
                figures[i].Shape.Fill = brushForUncollided;

            }
            for (int i = 0; i < figures.Count; i++)
            {
                for (int j = i + 1; j < figures.Count ; j++)
                {
                    if (Collide(figures[i], figures[j]))
                    {
                        figures[i].Shape.Fill = brushForCollided;
                        figures[j].Shape.Fill = brushForCollided;
                        //figures[i].vectormove.x *= -1;
                        //figures[i].vectormove.y *= -1;
                    }
                }
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
            public int _side;

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
            public virtual Polygon GivePoligon()
            {
                return null;
            }
        }

        public class EllipseF : Figure
        {
            private readonly Ellipse _figura;
            int rotateAngle;

            public EllipseF(int x, int y, int diameter) : base(x, y)
            {
                _side = diameter;
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
            public override Polygon GivePoligon()
            {
                return _figura;
            }
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
            public static Vector operator - (Vector x, Vector y)
            {
                return new Vector(x.x - y.x, x.y - y.y);
            }
            /*public static Vector operator +(Vector x, Vector y)
            {
                return new Vector(x.x + y.x, x.y + y.y);
            }*/
        }

        public static bool Collide(PoligonF x1, PoligonF x2)
        {
            List<Figure> x = new List<Figure>() { x1, x2 };
            bool XBigest = x1.centr.x > x2.centr.x;
            bool YBigest = x1.centr.y > x2.centr.y;
            bool colx = false, coly = false;

            for (int i = 0; i < x[XBigest ? 0 : 1].GivePoligon().Points.Count; i++)
            {
                for (int ii = 0; ii < x[!XBigest ? 0 : 1].GivePoligon().Points.Count; ii++)
                {
                    colx = colx || x[XBigest ? 0 : 1].GivePoligon().Points[i].X - x[!XBigest ? 0 : 1].GivePoligon().Points[ii].X <= 0;
                }
            }
            for (int i = 0; i < x[YBigest ? 0 : 1].GivePoligon().Points.Count; i++)
            {
                for (int ii = 0; ii < x[!YBigest ? 0 : 1].GivePoligon().Points.Count; ii++)
                {
                    coly = coly || x[YBigest ? 0 : 1].GivePoligon().Points[i].Y - x[!YBigest ? 0 : 1].GivePoligon().Points[ii].Y <= 0;
                }
            }
            return colx && coly;
        }

        public static bool Collide(EllipseF x1, PoligonF x2)
        {
            List<Figure> x = new List<Figure>() { x1, x2 };
            bool XBigest = x1.centr.x > x2.centr.x;
            bool YBigest = x1.centr.y > x2.centr.y;
            bool colx = false, coly = false;

            if (x[XBigest ? 0 : 1] is PoligonF)
            {
                for (int i = 0; i < x[XBigest ? 0 : 1].GivePoligon().Points.Count; i++)
                {
                    colx = x[XBigest ? 0 : 1].GivePoligon().Points[i].X - x[!XBigest ? 0 : 1].centr.x - x[!XBigest ? 0 : 1]._side/2 <= 0;
                }
            }
            else
            {
                for (int i = 0; i < x[!XBigest ? 0 : 1].GivePoligon().Points.Count; i++)
                {
                    colx = x[XBigest ? 0 : 1].centr.x - x[XBigest ? 0 : 1]._side/2 - x[!XBigest ? 0 : 1].GivePoligon().Points[i].X <= 0;
                }
            }

            if (x[YBigest ? 0 : 1] is PoligonF)
            {
                for (int i = 0; i < x[YBigest ? 0 : 1].GivePoligon().Points.Count; i++)
                {
                    coly = x[YBigest ? 0 : 1].GivePoligon().Points[i].Y - x[!YBigest ? 0 : 1].centr.y - x[!YBigest ? 0 : 1]._side/2 <= 0;
                }
            }
            else
            {
                for (int i = 0; i < x[!YBigest ? 0 : 1].GivePoligon().Points.Count; i++)
                {
                    coly = x[YBigest ? 0 : 1].centr.y - x[YBigest ? 0 : 1]._side/2 - x[!YBigest ? 0 : 1].GivePoligon().Points[i].Y < 0;
                }
            }
            return colx && coly;
        }

        public static bool Collide(EllipseF x1, EllipseF x2)
        {
            List<Figure> x = new List<Figure>() { x1, x2 };
            bool XBigest = x1.centr.x > x2.centr.x;
            bool YBigest = x1.centr.y > x2.centr.y;
            //bool colx = false, coly = false;

            //colx = x[XBigest ? 0 : 1].centr.x - x[XBigest ? 0 : 1]._side/2 - x[!XBigest ? 0 : 1].centr.x - x[!XBigest ? 0 : 1]._side/2 <= 0;
            //coly = x[YBigest ? 0 : 1].centr.y - x[YBigest ? 0 : 1]._side/2 - x[!YBigest ? 0 : 1].centr.y - x[!YBigest ? 0 : 1]._side/2 <= 0;
            //return colx && coly;
            return Math.Sqrt((x1.centr - x2.centr).x)+ Math.Sqrt((x1.centr - x2.centr).x) < Math.Sqrt(x1._side + x2._side);


        }

        public static bool Collide(Figure x1, Figure x2)
        {
            if (x1 is EllipseF)
            {
                EllipseF e1 = (EllipseF)x1;
                if (x2 is EllipseF)
                {
                    EllipseF e2 = (EllipseF)x2;
                    return Collide(e1, e2);
                }
                else if (x2 is PoligonF)
                {
                    PoligonF p2 = (PoligonF)x2;
                    return Collide(e1, p2);
                }
                else
                {
                    Debug.Fail($"x2 is {x2}, x2.GetType={x2?.GetType()}");
                    return false;
                }
            }
            else if (x1 is PoligonF)
            {
                PoligonF p1 = (PoligonF)x1;
                if (x2 is EllipseF)
                {
                    EllipseF e2 = (EllipseF)x2;
                    //return Collide(p1, e2); // StackOverflow
                    return Collide(e2, p1);
                }
                else if (x2 is PoligonF)
                {
                    PoligonF p2 = (PoligonF)x2;
                    return Collide(p1, p2);
                }
                else
                {
                    Debug.Fail($"x2 is {x2}, x2.GetType={x2?.GetType()}");
                    return false;
                }
            }
            else
            {
                Debug.Fail($"x1 is {x1}, x1.GetType={x1?.GetType()}");
                return false;
            }
        }
    }
}
