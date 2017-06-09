using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangulationByMinimumAngle
{
    public static class MinimalAngleAlgorithm
    {

        #region public structs
        public struct TriangulationStructure
        {
            public List<Point> points;
            public List<Triangle> triangles;
            public List<List<List<int>>> boundaryPoints;
        }
        #endregion

        #region public methods

        public static TriangulationStructure TriangulateRect(double xx, double yy, int numOfPartision)
        {
            TriangulationStructure outInfo = new TriangulationStructure();
            outInfo.points = new List<Point>();
            outInfo.triangles = new List<Triangle>();
            double h1 = (xx) / numOfPartision;
            double h2 = yy / numOfPartision;
            bool isBound = false;
            for (int i = 0; i < numOfPartision + 1; i++)
            {

                double x = i * h1;
                if (x == 0 || x == xx) isBound = true;
                else isBound = false;
                for (int j = 0; j < numOfPartision + 1; j++)
                {
                    double y = j * h2;
                    if (isBound == false && (y == 0 || y == yy)) isBound = true;
                    outInfo.points.Add(new Point(x, y, isBound));
                }
                x += h1 / 2.0;
                if (x > xx) break;
                for (int j = 0; j < numOfPartision; j++)
                {
                    double y = j * h2;
                    y += h2 / 2;
                    outInfo.points.Add(new Point(x, y));
                }
            }

            for (int i = 0; i < numOfPartision; i++)
            {

                double x1 = 0 + i * h1;
                double x2 = 0 + (i + 1) * h1;
                for (int j = 0; j < numOfPartision; j++)
                {
                    double y1 = j * h2;
                    double y2 = (j + 1) * h2;
                    Point a11 = new Point(x1, y1), a21 = new Point(x1, y2), a12 = new Point(x2, y1), a22 = new Point(x2, y2), amidle = new Point((x1 + x2) / 2.0, (y1 + y2) / 2.0);
                    int p11 = findPoint(outInfo.points, a11);
                    int p12 = findPoint(outInfo.points, a12);
                    int p21 = findPoint(outInfo.points, a21);
                    int p22 = findPoint(outInfo.points, a22);
                    int pmidle = findPoint(outInfo.points, amidle);
                    outInfo.triangles.Add(new Triangle(p11, p12, pmidle));
                    outInfo.triangles.Add(new Triangle(p11, pmidle, p21));
                    outInfo.triangles.Add(new Triangle(pmidle, p12, p22));
                    outInfo.triangles.Add(new Triangle(p21, pmidle, p22));
                }
            }
            outInfo.boundaryPoints = new List<List<List<int>>>();
            outInfo.boundaryPoints.Add(new List<List<int>>());
            foreach (var tr in outInfo.triangles)
            {

                if (outInfo.points[tr.A].Y == 0 && outInfo.points[tr.B].Y == 0)
                    outInfo.boundaryPoints[0].Add(new List<int>() { tr.A, tr.B });

                if (outInfo.points[tr.B].Y == 0 && outInfo.points[tr.C].Y == 0)
                    outInfo.boundaryPoints[0].Add(new List<int>() { tr.B, tr.C });

                if (outInfo.points[tr.C].Y == 0 && outInfo.points[tr.A].Y == 0)
                    outInfo.boundaryPoints[0].Add(new List<int>() { tr.C, tr.A });

            }
            outInfo.boundaryPoints.Add(new List<List<int>>());
            foreach (var tr in outInfo.triangles)
            {

                if (outInfo.points[tr.A].X == xx && outInfo.points[tr.B].X == xx)
                    outInfo.boundaryPoints[1].Add(new List<int>() { tr.A, tr.B });

                if (outInfo.points[tr.B].X == xx && outInfo.points[tr.C].X == xx)
                    outInfo.boundaryPoints[1].Add(new List<int>() { tr.B, tr.C });

                if (outInfo.points[tr.C].X == xx && outInfo.points[tr.A].X == xx)
                    outInfo.boundaryPoints[1].Add(new List<int>() { tr.C, tr.A });

            }
            outInfo.boundaryPoints.Add(new List<List<int>>());
            foreach (var tr in outInfo.triangles)
            {

                if (outInfo.points[tr.A].Y == yy && outInfo.points[tr.B].Y == yy)
                    outInfo.boundaryPoints[2].Add(new List<int>() { tr.A, tr.B });

                if (outInfo.points[tr.B].Y == yy && outInfo.points[tr.C].Y == yy)
                    outInfo.boundaryPoints[2].Add(new List<int>() { tr.B, tr.C });

                if (outInfo.points[tr.C].Y == yy && outInfo.points[tr.A].Y == yy)
                    outInfo.boundaryPoints[2].Add(new List<int>() { tr.C, tr.A });

               
            }
            outInfo.boundaryPoints[2].Reverse();
            outInfo.boundaryPoints.Add(new List<List<int>>());
            foreach (var tr in outInfo.triangles)
            {

                if (outInfo.points[tr.A].X == 0 && outInfo.points[tr.B].X == 0)
                    outInfo.boundaryPoints[3].Add(new List<int>() { tr.A, tr.B });

                if (outInfo.points[tr.B].X == 0 && outInfo.points[tr.C].X == 0)
                    outInfo.boundaryPoints[3].Add(new List<int>() { tr.B, tr.C });

                if (outInfo.points[tr.C].X == 0 && outInfo.points[tr.A].X == 0)
                    outInfo.boundaryPoints[3].Add(new List<int>() { tr.C, tr.A });

               
            }
            outInfo.boundaryPoints[3].Reverse();
            return outInfo;
        }
        public static TriangulationStructure Triangulate(List<Point> points, double MaxArea)
        {
            TriangulationStructure res = new TriangulationStructure();
            String s = "";
            foreach (Point p in points)
            {
                s += String.Format("{0},{1} ", p.X, p.Y);
            }
            s = s.Remove(s.Length - 1, 1);
            Area area = new Area();
            List<Point> tempPoints = new List<Point>();
            tempPoints.AddRange(points);
            area = Area.convertStrIntoArea(s);
            res.points = new List<Point>();
            res.triangles = new List<Triangle>();
            res.boundaryPoints = new List<List<List<int>>>();
            InitialTriangulation(tempPoints, ref res, area, points);
            FinalTriangulation(ref res, MaxArea);
            SetBoundaryPoints(ref res, points);
            return res;
        }
        public static double getTriangleSquare(Triangle triangle, List<Point> points)
        {
            Point p1 = points[triangle.A], p2 = points[triangle.B], p3 = points[triangle.C];
            return Math.Abs(0.5 * ((p2.X * p3.Y - p2.Y * p3.X) - (p1.X * p3.Y - p1.Y * p3.X) + (p1.X * p2.Y - p1.Y * p2.X)));

        }
        #endregion

        #region private methods
        static void InitialTriangulation(List<Point> points, ref TriangulationStructure returnInfo, Area area, List<Point> region)
        {

            // Triangulation.Triangulation.TriangulationStructure returnInfo = new Triangulation.Triangulation.TriangulationStructure();

            List<Point> subPoints = points;
            double minimumAngle = 0;
            double length1 = 0, length2 = 0;
            Point p1 = points.Last();
            Point p2 = points[0];
            Point p3 = points[1];
            length1 = Point.GetDistance(p1, p2);
            length2 = Point.GetDistance(p2, p3);
            double scalarProduct = Point.GetScalarProduct(p1 - p2, p3 - p2);
            minimumAngle = Math.Acos(scalarProduct / length1 / length2);
            Point middle = Point.GetMiddlePoint(Point.GetPercentPoint(p2, p1, 1), Point.GetPercentPoint(p2, p3, 1));
            if (!ptInPol(area, middle.X, middle.Y))
            {
                minimumAngle = 2 * Math.PI - minimumAngle;
            }
            int minIndex = 0;

            for (int i = 1; i < points.Count - 1; i++)
            {
                p1 = points[i - 1];
                p2 = points[i];
                p3 = points[i + 1];
                length1 = Point.GetDistance(p1, p2);
                length2 = Point.GetDistance(p2, p3);
                scalarProduct = Point.GetScalarProduct(p1 - p2, p3 - p2);
                double angle = Math.Acos(scalarProduct / length1 / length2);
                middle = Point.GetMiddlePoint(Point.GetPercentPoint(p2, p1, 1), Point.GetPercentPoint(p2, p3, 1));
                if (!ptInPol(area, middle.X, middle.Y))
                {
                    angle = 2 * Math.PI - angle;
                }
                if (angle < minimumAngle) { minimumAngle = angle; minIndex = i; }
            }
            p1 = points[points.Count() - 2];
            p2 = points.Last();
            p3 = points[0];
            length1 = Point.GetDistance(p1, p2);
            length2 = Point.GetDistance(p2, p3);
            scalarProduct = Point.GetScalarProduct(p1 - p2, p3 - p2);
            double angleL = Math.Acos(scalarProduct / length1 / length2);
            middle = Point.GetMiddlePoint(Point.GetPercentPoint(p2, p1, 1), Point.GetPercentPoint(p2, p3, 1));
            if (!ptInPol(area, middle.X, middle.Y))
            {
                angleL = 2 * Math.PI - angleL;
            }
            if (angleL < minimumAngle)
            {
                minimumAngle = angleL;
                minIndex = points.Count - 1;
            }
            int k, l, m;
            k = l = m = 0;
            if (minIndex == 0)
            {
                k = subPoints.Count - 1;
                l = 0;
                m = 1;
            }
            else if (minIndex == points.Count - 1)
            {
                k = points.Count - 2;
                l = points.Count - 1;
                m = 0;
            }
            else
            {
                k = minIndex - 1;
                l = minIndex;
                m = minIndex + 1;
            }
            //  returnInfo.points.AddRange(new List<Point>() { points[k], points[l], points[m] });
            if (findPoint(returnInfo.points, points[k]) == -1) returnInfo.points.Add(points[k]);
            if (findPoint(returnInfo.points, points[l]) == -1) returnInfo.points.Add(points[l]);
            if (findPoint(returnInfo.points, points[m]) == -1) returnInfo.points.Add(points[m]);
            //int kk = -1, ll = -1, mm = -1;
            //for(int i=0; i<region. Count; i++)
            //{
            //    if (points[k] == region[i]) kk = i;
            //    if (points[l] == region[i]) ll = i;
            //    if (points[m] == region[i]) mm = i;
            //}
            //int firstBoundTriangle = -1, secondBoundTriangle = -1;
            //List<int> bounds = new List<int>();
            returnInfo.triangles.Add(new Triangle(findPoint(returnInfo.points, points[k]), findPoint(returnInfo.points, points[l]), findPoint(returnInfo.points, points[m])));
            if (points.Count == 3) return;
            subPoints.Remove(points[l]);
            InitialTriangulation(subPoints, ref returnInfo, area, region);
        }
        private static bool ptInPol(Area polygon, double X, double Y)
        {
            //початково точка не лежить в області
            bool res = false;
            for (int i = 0; i < polygon.Count; i++)
            {
                if (polygon.ElementAt(i).X == X && polygon.ElementAt(i).Y == Y) return true;
            }
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
                    if (((polygon.ElementAt(i).X <= X) && (polygon.ElementAt(j).X >= X) && (polygon.ElementAt(i).Y <= Y) && (polygon.ElementAt(j).Y >= Y)) || ((polygon.ElementAt(j).X <= X) && (polygon.ElementAt(i).X >= X) && (polygon.ElementAt(j).Y <= Y) && (polygon.ElementAt(i).Y >= Y)))
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
                    if (X > xOnLine)
                    {
                        res = !res;
                    }
                }
                j = i;
            }
            return res;
        }
        static int findPoint(List<Point> points, Point point)
        {
            int res = -1;
            for (int i = 0; i < points.Count; i++)
            {
                if (point.Equals(points[i])) { res = i; return res; }
            }
            return res;
        }

        //static int FindTriangle(List<Triangle> points, Tr\ point)
        //{
        //    int res = -1;
        //    for (int i = 0; i < points.Count; i++)
        //    {
        //        if (point.Equals(points[i])) { res = i; return res; }
        //    }
        //    return res;
        //}
        static void removeSomeTriangles(List<Triangle> triangles, int point)
        {
            for (int i = 0; i < triangles.Count; i++)
            {
                if (triangles[i].DoesContain(point)) { triangles.RemoveAt(i--); }
            }
        }
        static bool IsPointEngaged(List<Triangle> triangles, int point)
        {
            foreach (Triangle tr in triangles)
            {
                if (tr.A == point || tr.B == point || tr.C == point)
                    return true;
            }
            return false;
        }
        static void ChangeTriangles(List<Triangle> triangles, int point)
        {
            foreach (Triangle tr in triangles)
            {
                if (tr.A > point) tr.A--;
                if (tr.B > point) tr.B--;
                if (tr.C > point) tr.C--;
            }
        }

        static void FinalTriangulation(ref TriangulationStructure returnData, double MaxArea)
        {
            for (int i = 0; i < returnData.triangles.Count; i++)
            {
                double currentSquare = getTriangleSquare(returnData.triangles[i], returnData.points);
                if (currentSquare > MaxArea)
                {


                    Point p1 = returnData.points[returnData.triangles[i].A];
                    Point p2 = returnData.points[returnData.triangles[i].B];
                    Point p3 = returnData.points[returnData.triangles[i].C];
                    returnData.triangles.Remove(returnData.triangles[i]);
                    double lengthAB = Point.GetDistance(p1, p2), lengthAC = Point.GetDistance(p1, p3), lengthBC = Point.GetDistance(p2, p3);
                    int max = Max(lengthAB, lengthAC, lengthBC);
                    if (max == 0)
                    {
                        Point middle = Point.GetMiddlePoint(p1, p2);
                        if (findPoint(returnData.points, middle) == -1)
                            returnData.points.Add(middle);
                        returnData.triangles.Add(new Triangle(findPoint(returnData.points, p1), findPoint(returnData.points, middle), findPoint(returnData.points, p3)));
                        returnData.triangles.Add(new Triangle(findPoint(returnData.points, p3), findPoint(returnData.points, middle), findPoint(returnData.points, p2)));
                    }
                    else if (max == 1)
                    {
                        Point middle = Point.GetMiddlePoint(p1, p3);
                        if (findPoint(returnData.points, middle) == -1)
                            returnData.points.Add(middle);
                        returnData.triangles.Add(new Triangle(findPoint(returnData.points, p1), findPoint(returnData.points, p2), findPoint(returnData.points, middle)));
                        returnData.triangles.Add(new Triangle(findPoint(returnData.points, middle), findPoint(returnData.points, p2), findPoint(returnData.points, p3)));
                    }
                    else
                    {
                        Point middle = Point.GetMiddlePoint(p2, p3);
                        if (findPoint(returnData.points, middle) == -1)
                            returnData.points.Add(middle);
                        returnData.triangles.Add(new Triangle(findPoint(returnData.points, p1), findPoint(returnData.points, p2), findPoint(returnData.points, middle)));
                        returnData.triangles.Add(new Triangle(findPoint(returnData.points, p1), findPoint(returnData.points, middle), findPoint(returnData.points, p3)));
                    }
                    i--;
                }
            }

            returnData.boundaryPoints = new List<List<List<int>>>();
        }
        static int Max(params double[] x)
        {
            int res = 0;
            for (int i = 1; i < x.Length; i++)
            {
                if (x[i] > x[res]) res = i;
            }
            return res;
        }
        static void SetBoundaryPoints(ref TriangulationStructure returnInfo, List<Point> area)
        {
            returnInfo.boundaryPoints = new List<List<List<int>>>();
            for (int i = 0; i < area.Count - 1; i++)
            {
                returnInfo.boundaryPoints.Add(new List<List<int>>());

                foreach (Triangle tr in returnInfo.triangles)
                {
                    Point p1 = returnInfo.points[tr.A], p2 = returnInfo.points[tr.B], p3 = returnInfo.points[tr.C];
                    if (Point.IsPointOnTheSegment(area[i], area[i + 1], p1) && Point.IsPointOnTheSegment(area[i], area[i + 1], p2))
                    {
                        returnInfo.boundaryPoints.Last().Add(new List<int>() { tr.A, tr.B });
                    }
                    else if (Point.IsPointOnTheSegment(area[i], area[i + 1], p1) && Point.IsPointOnTheSegment(area[i], area[i + 1], p3))
                    {
                        returnInfo.boundaryPoints.Last().Add(new List<int>() { tr.A, tr.C });
                    }
                    else if (Point.IsPointOnTheSegment(area[i], area[i + 1], p2) && Point.IsPointOnTheSegment(area[i], area[i + 1], p3))
                    {
                        returnInfo.boundaryPoints.Last().Add(new List<int>() { tr.B, tr.C });
                    }
                }


            }
            returnInfo.boundaryPoints.Add(new List<List<int>>());
            foreach (Triangle tr in returnInfo.triangles)
            {
                Point p1 = returnInfo.points[tr.A], p2 = returnInfo.points[tr.B], p3 = returnInfo.points[tr.C];
                if (Point.IsPointOnTheSegment(area.Last(), area[0], p1) && Point.IsPointOnTheSegment(area.Last(), area[0], p2))
                {
                    returnInfo.boundaryPoints.Last().Add(new List<int>() { tr.A, tr.B });
                }
                else if (Point.IsPointOnTheSegment(area.Last(), area[0], p1) && Point.IsPointOnTheSegment(area.Last(), area[0], p3))
                {
                    returnInfo.boundaryPoints.Last().Add(new List<int>() { tr.A, tr.C });
                }
                else if (Point.IsPointOnTheSegment(area.Last(), area[0], p2) && Point.IsPointOnTheSegment(area.Last(), area[0], p3))
                {
                    returnInfo.boundaryPoints.Last().Add(new List<int>() { tr.B, tr.C });
                }
            }

            for (int i = 0; i < area.Count - 1; i++)
            {
                //  List<List<int>> curentBound = returnInfo.boundaryPoints[i];
                int begin = findPoint(returnInfo.points, area[i]);
                int end = findPoint(returnInfo.points, area[i + 1]);
                for (int k = 0; k < returnInfo.boundaryPoints[i].Count; k++)
                {
                    int minIndex = k;
                    bool direct = true;
                    for (int j = k; j < returnInfo.boundaryPoints[i].Count; j++)
                    {
                        if (returnInfo.boundaryPoints[i][j][0] == begin)
                        {
                            minIndex = j;
                            direct = true;
                            begin = returnInfo.boundaryPoints[i][j][1];
                            break;
                        }
                        else if (returnInfo.boundaryPoints[i][j][1] == begin)
                        {
                            minIndex = j;
                            direct = false;
                            begin = returnInfo.boundaryPoints[i][j][0];
                            break;
                        }
                    }
                    if (direct)
                    {
                        List<int> temp = returnInfo.boundaryPoints[i][k];
                        returnInfo.boundaryPoints[i][k] = returnInfo.boundaryPoints[i][minIndex];
                        returnInfo.boundaryPoints[i][minIndex] = temp;
                    }
                    else
                    {
                        returnInfo.boundaryPoints[i][minIndex].Reverse();
                        List<int> temp = returnInfo.boundaryPoints[i][k];
                        returnInfo.boundaryPoints[i][k] = returnInfo.boundaryPoints[i][minIndex];
                        returnInfo.boundaryPoints[i][minIndex] = temp;
                    }
                }
            }
            // List<List<int>> curentBoundL = returnInfo.boundaryPoints.Last();
            int beginL = findPoint(returnInfo.points, area.Last());
            int endL = findPoint(returnInfo.points, area[0]);
            for (int k = 0; k < returnInfo.boundaryPoints.Last().Count; k++)
            {
                int minIndexL = k;
                bool directL = true;
                for (int j = k; j < returnInfo.boundaryPoints.Last().Count; j++)
                {
                    if (returnInfo.boundaryPoints.Last()[j][0] == beginL)
                    {
                        minIndexL = j;
                        directL = true;
                        beginL = returnInfo.boundaryPoints.Last()[j][1];
                        break;
                    }
                    else if (returnInfo.boundaryPoints.Last()[j][1] == beginL)
                    {
                        minIndexL = j;
                        directL = false;
                        beginL = returnInfo.boundaryPoints.Last()[j][0];
                        break;
                    }
                }
                if (directL)
                {
                    List<int> temp = returnInfo.boundaryPoints.Last()[k];
                    returnInfo.boundaryPoints.Last()[k] = returnInfo.boundaryPoints.Last()[minIndexL];
                    returnInfo.boundaryPoints.Last()[minIndexL] = temp;
                }
                else
                {
                    returnInfo.boundaryPoints.Last()[minIndexL].Reverse();
                    List<int> temp = returnInfo.boundaryPoints.Last()[k];
                    returnInfo.boundaryPoints.Last()[k] = returnInfo.boundaryPoints.Last()[minIndexL];
                    returnInfo.boundaryPoints.Last()[minIndexL] = temp;
                }
            }


        }
        #endregion
    }
}
