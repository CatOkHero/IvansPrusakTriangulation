using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangulationByMinimumAngle
{
    public class Point:IComparable<Point>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool IsBoundaryPoint { get; set; }

        public Point()
        {

        }

        public Point(double x, double y, bool isBoundaryPoint)
        {
            X = x;
            Y = y;
            IsBoundaryPoint = isBoundaryPoint;
        }

        public Point(double x, double y):this(x,y,false)
        {

        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", X, Y);
        }
        public static bool IsPointOnTheSegment(Point begin, Point end, Point point)
        {
            bool IsInternalX = (point.X <= Max(begin.X, end.X) && point.X >= Min(begin.X, end.X));
            bool IsInternalY = (point.Y <= Max(begin.Y, end.Y) && point.Y >= Min(begin.Y, end.Y));
            if (point.Equals(begin) || point.Equals(end)) return true;
            else if (begin.X == end.X) return (point.X == begin.X && IsInternalY);
            else if (begin.Y == end.Y) return (point.Y == begin.Y && IsInternalX);
            else
                return (((-begin.Y + point.Y) / (-begin.Y + end.Y) == (-begin.X + point.X) / (-begin.X + end.X))
                    && IsInternalX);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Point pn = obj as Point;
            if (pn == null)
                return false;
            if (pn.X == this.X && pn.Y == this.Y) return true;
            else
                return false;
        }

        static double Max(double a, double b)
        {
            return (a > b) ? a : b;
        }

        static double Min(double a, double b)
        {
            return (a < b) ? a : b;
        }

        public List<double> ToList()
        {
            return new List<double>() { X, Y };
        }

        public int CompareTo(Point other)
        {
            if (this.Y < other.Y) return -1;
            else if (this.Y > other.Y) return 1;
            else return 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public static Point operator- (Point p1, Point p2)
        {
            return new Point(p2.X - p1.X, p2.Y - p1.Y);
        }

        public static double GetDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static double GetScalarProduct(Point p1, Point p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y;
        }

        public static Point GetMiddlePoint(Point p1, Point p2)
        {
            return new Point((p1.X + p2.X) / 2.0, (p1.Y + p2.Y) / 2.0);
        }

        public static Point GetPercentPoint(Point p1, Point p2,double percent)
        {
            return new Point(p1.X + percent / 100.0 * (p2.X - p1.X), p1.Y + percent / 100.0 * (p2.Y - p1.Y));
        }
    }
}
