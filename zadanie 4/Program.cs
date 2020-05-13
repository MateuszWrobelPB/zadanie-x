using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zadanie_4
{
    class Program
    {
        public abstract class Figure
        {
            public string label { get; set; }

            public Figure(string label)
            {
                this.label = label;
            }
            public virtual double GetArea()
            {
                return 0;
            }

            public abstract void Move(double x, double y);

        }

        public class Point : Figure
        {
            private double x, y;
            public Point() : base("DefaultLabel")
            {
            }
            public Point(string label) : base(label)
            {
                base.label = label;
            }

            public Point(double x, double y, string label) : base(label)
            {
                base.label = label;
                this.x = x;
                this.y = y;
            }

            public override void Move(double x, double y)
            {
                this.x += x;
                this.y += y;
            }

            public override string ToString()
            {
                return "Punkt " + base.label + " (" + x + " , " + y + ")";
            }
        }

        class Circle : Figure
        {
            private Point o { get; set; } //origin
            private double r { get; set; } //radius

            public Circle(Point o, double r, string label) : base(label)
            {
                this.o = o;
                this.r = r;
            }

            public Circle(double x, double y, double r, string label) : base(label)
            {
                this.o = new Point(x, y, "");
                this.r = r;
            }
            public override string ToString()
            {
                return o.ToString() + " " + r + " " + base.label;
            }

            public override double GetArea()
            {
                return r * r * Math.PI;
            }

            public override void Move(double x, double y)
            {
                o.Move(x, y);
            }
        }

        class FilledCircle : Circle
        {
            private int color;
            public FilledCircle(double x, double y, double r, int color, string label) : base(x, y, r, label)
            {
                this.color = color;
            }

            public override string ToString()
            {
                return "FilledCircle ...";
            }
        }

        public class Picture
        {
            private List<Figure> figures = new List<Figure>();

            public virtual bool add(Figure figure)
            {
                if (CheckLabel(figure))
                {
                    figures.Add(figure);
                    return true;
                }
                return false;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                foreach (Figure f in figures)
                {
                    sb.Append(f.ToString()).Append("\n");
                }
                return sb.ToString();
            }

            public double GetArea()
            {
                double totalArea = 0;
                foreach (Figure f in figures)
                {
                    totalArea += f.GetArea();
                }
                return totalArea;
            }

            public virtual bool CheckLabel(Figure figure)
            {
                return true;
            }
        }

        public class UniquePicture : Picture
        {
            private List<Figure> figures = new List<Figure>();

            public override bool CheckLabel(Figure figure)
            {
                foreach (Figure f in figures)
                {
                    if (f.label == figure.label) return false;
                }
                return base.CheckLabel(figure);
            }
        }

        public class StandarizedPicture : Picture
        {
            private List<Figure> figures = new List<Figure>();
            public override bool CheckLabel(Figure figure)
            {
                if (Char.IsUpper(figure.label[0]) && figure.label.Any(char.IsUpper) && figure.label.Any(char.IsDigit)) return false;
                return base.CheckLabel(figure);
            }
        }


        public static void Main(string[] args)
        {
            Point p1 = new Point(1, 1, "P1");
            Circle c1 = new Circle(new Point(3.3, 5.5, "P2"), 10.0, "C1");
            Circle c2 = new Circle(new Point(3.3, 5.5, "P2"), 10.0, "C1");
            //FilledCircle fc1 = new FilledCircle(1, 3, 4, 4, "");

            Picture picture = new Picture();
            picture.add(p1);
            picture.add(c1);
            picture.add(c2);
            //picture.add(fc1);

            Console.WriteLine(picture.ToString());

            c1.Move(2, 5);
            Console.WriteLine(picture.ToString());

        }
    }
}