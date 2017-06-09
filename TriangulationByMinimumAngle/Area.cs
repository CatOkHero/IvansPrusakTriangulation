using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TriangulationByMinimumAngle
{
    
    
    



    public class Area
    {
        public static event Action pointInPolygon;
        List<Point1> polygon;
        public List<Point1> Polygon { get { return polygon; } private set { polygon = value; } }

        public static Area readAreaFromFile(string fileName)
        {
            try
            {
                StreamReader sr = new StreamReader(fileName);
                string ar = sr.ReadToEnd();
                sr.Close();
                return convertStrIntoArea(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace + ex.Message);
                Console.ReadLine();
            }
            return null;
        }

        public static Area convertStrIntoArea(string s)
        {
            List<Point1> pol = new List<Point1>();
            string[] allPoints = (s).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < allPoints.Length; i++)
            {
                string[] point = allPoints[i].Split(',');
                try
                {
                    pol.Add(new Point1(Convert.ToDouble(point[0]), Convert.ToDouble(point[1])));
                }
                catch (Exception e)
                {
                    string problemPlace = "";
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < point.Length; j++)
                    {
                        sb.Append(point[j]);
                    }
                    problemPlace = sb.ToString();
                    throw new InvalidAreaInputException(problemPlace, e);
                }
            }
            return new Area()
            {
                Polygon = pol
            };
        }
        public Point1 ElementAt(int i)
        {
            return Polygon.ElementAt(i);
        }
        public int Count { get { return Polygon.Count; } }
        public static void isPointinPolygon(Area polygon, Point1 p)
        {
            if (ptInPol(polygon, p)) pointInPolygon();
        }
        /*
        public static bool ptInPol(Area polygon, Point p)
        {
            double eps = 0.001;
            if (ptInPol(polygon, p.X,p.Y)) return true;
            if (ptInPol(polygon, p.X, p.Y+eps)) return true;
            if (ptInPol(polygon, p.X, p.Y-eps)) return true;
            if (ptInPol(polygon, p.X+eps, p.Y)) return true;
            if (ptInPol(polygon, p.X-eps, p.Y)) return true;
            if (ptInPol(polygon, p.X - eps, p.Y-eps)) return true;
            if (ptInPol(polygon, p.X + eps, p.Y - eps)) return true;
            if (ptInPol(polygon, p.X - eps, p.Y + eps)) return true;
            return (ptInPol(polygon, p.X + eps, p.Y + eps));


 
        }

        private static bool ptInPol(Area polygon, double X,double Y)
        {
            int nPol = polygon.Count;
            int j = nPol - 1;
            bool c = false;
            
            for (int i = 0; i < nPol; i++)
            {
                if (((polygon.ElementAt(i).Y <= Y) && (Y < polygon.ElementAt(j).Y))
                    || ((polygon.ElementAt(j).Y <= Y) && (Y < polygon.ElementAt(i).Y)))
                {
                    if(X > (polygon.ElementAt(j).X - polygon.ElementAt(i).X) * (Y - polygon.ElementAt(i).Y)
                    / (polygon.ElementAt(j).Y - polygon.ElementAt(i).Y) + polygon.ElementAt(i).X)
                    
                {
                    c = !c;
                }}
            
                j = i;
            }
            return c;
        }*/
        public static bool ptInPol(Area polygon, Point1 p)
        {
            return (ptInPol(polygon, p.X, p.Y));
        }
        private static bool ptInPol(Area polygon, double X, double Y)
        {
            //початково точка не лежить в області
            bool res = false;
            //перевірка відрізка з точки многокутника і попередньої точки
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; i++)
            {
                //випадок коли відрізкок лежить на одному рівні по y з точкою яку перевіряємо
                if ((polygon.ElementAt(i).Y == Y) && (Y == polygon.ElementAt(j).Y))
                {
                    //точка ледить справа від відрізка - перетинає сторону
                    if ((polygon.ElementAt(i).X < X) && (polygon.ElementAt(j).X < X))
                    {
                        res = !res;
                        i++;
                        j = i;
                    }
                    //точка ледить на відрізку -  знаходиься на многокутнику подальших перевірок не треба
                    if (((polygon.ElementAt(i).X <= X) && (polygon.ElementAt(j).X >= X)) || ((polygon.ElementAt(j).X <= X) && (polygon.ElementAt(i).X >= X)))
                        return true;
                }
                else
                //точка лежить між початком і кінцем відрізку по у
                if (((polygon.ElementAt(i).Y <= Y) && (Y < polygon.ElementAt(j).Y)) || ((polygon.ElementAt(j).Y <= Y) && (Y < polygon.ElementAt(i).Y)))
                {
                    //проекія тточки на відрізок
                    double xOnLine = (polygon.ElementAt(j).X - polygon.ElementAt(i).X) * (Y - polygon.ElementAt(i).Y)
                    / (polygon.ElementAt(j).Y - polygon.ElementAt(i).Y) + polygon.ElementAt(i).X;
                    //точка співпадає зі своєю проекцією на сторону - лежить в прямокутнику подальших перевірок не треба
                    if (X == xOnLine) return true;
                    //точка лежить справа від відрізка є перетин сторони
                    if (X > xOnLine) res = !res;
                }
                j = i;
            }
            return res;
        }
    }


    public class Point1 : IComparable<Point1>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Point1() : this(0, 0) { }
        public Point1(double x, double y)
        {
            X = x;
            Y = y;
        }
        public int CompareTo(Point1 other)
        {
            return this.X.CompareTo(other.X);

        }
    }

     public class InvalidAreaInputException : Exception
    {
        public string ProblemPlace { get; private set; }
        protected InvalidAreaInputException() : base() { }

        public InvalidAreaInputException(string problemPlace): base()
        {
            ProblemPlace = problemPlace;
        }

        public InvalidAreaInputException(string problemPlace, Exception innerException)
            : base("incorect input", innerException)
        {
            ProblemPlace = problemPlace;
        }

      


    }
}
