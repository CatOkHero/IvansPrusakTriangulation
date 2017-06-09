//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Triangulation
//{
//    public static class Triangulation
//    {
//        public struct TriangulationStructure
//        {
//            public List<Point> points;
//            public List<Triangle> triangles;
//            public List<List<List<int>>> boundaryPoints;
//        }
//        //public delegate double BoundFunction(double x);

//        //public static TriangulationStructure Triangulate(double beginPoint, double endPoint, BoundFunction lowerBound, BoundFunction upperBound, double maxArea, int numOfBounds, params List<Point>[] boundPoints)
//        public static TriangulationStructure Triangulate(double beginPoint, double endPoint, double maxArea, int numOfBounds, params List<Point>[] boundPoints)

//        {
//            double maxY = 0;
//            for (int i = 0; i < numOfBounds; i++)
//            {
//                if (boundPoints[i][0].Y > maxY) maxY = boundPoints[i][0].Y;
//            }
//            string s = "";
//            for(int i=0; i<numOfBounds; i++)
//            {
//                s += String.Format("{0},{1} ", boundPoints[i][0].X, boundPoints[i][0].Y);
//            }
//            s = s.Remove(s.Length - 1, 1);
//            Area area = new Area();
//            area = Area.convertStrIntoArea(s);
//            int square = (int)new List<double>() { maxY, endPoint - beginPoint }.Max();
//            int numOfPartition = (int)(1 / 4.0 / Math.Sqrt(maxArea));
//            if (numOfPartition % 2 == 1) numOfPartition++;
//           // int numOfPartition = (int)(square / 2.0 / Math.Sqrt(maxArea));
//           // int n = 0;
//           // while (numOfPartition >= Math.Pow(2, n)) n++;
//           //// if (numOfPartition - Math.Pow(2, n-1) <= 0.1*(Math.Pow(2,n)-Math.Pow(2,n-1))) n--;

//           // numOfPartition = (int)Math.Pow(2,n);

//            numOfPartition *= square;
//            if (numOfPartition == 0) numOfPartition = square;
//            if (maxArea > 0.25) numOfPartition = square;
//            while(maxArea<(square*square/(double)(numOfPartition*numOfPartition*4)))
//            {
//                numOfPartition *= 2;
//            }
//           // if (numOfPartition % 2 == 1) numOfPartition *= 2;
//            TriangulationStructure outInfo = TriangulateSquare(beginPoint, square + beginPoint, numOfPartition);
//            List<Point> boundaryPoints = new List<Point>();
//            for (int i = 0; i < outInfo.points.Count; i++)
//            {
//                double y = outInfo.points[i].Y;
//                double x = outInfo.points[i].X;
//               // double y1 = lowerBound(x), y2 = upperBound(x);
               
//                // if ((!(y <= y2 && y >= y1) || !(x <= endPoint && x >= beginPoint)))
//                if(!ptInPol(area,x,y))
//                {
                   
//                    removeSomeTriangles(outInfo.triangles, i);
//                    outInfo.points.RemoveAt(i);
//                    ChangeTriangles(outInfo.triangles, i);
//                    i--;
//                }
//            }

//            outInfo.boundaryPoints = new List<List<List<int>>>();

//            bool direct = true;
//            for (int i = 0; i < numOfBounds; i++)
//            {
//                outInfo.boundaryPoints.Add(new List<List<int>>());
//                if (boundPoints[i][0].X == endPoint && direct == true) direct = false;
//                int prevPoint = 0;
//                if (direct)
//                {
//                    for (int j = 0; j < outInfo.points.Count; j++)
//                    {
//                        if (Point.IsPointOnTheSegment(boundPoints[i][0], boundPoints[i][1], outInfo.points[j]))
//                        {
//                            outInfo.points[j].IsBoundaryPoint = true;
//                            prevPoint = j;
//                            break;
//                        }
//                    }
//                }
//                else
//                {

//                    for (int j = outInfo.points.Count - 1; j >= 0; j--)
//                    {
//                        if (Point.IsPointOnTheSegment(boundPoints[i][0], boundPoints[i][1], outInfo.points[j]))
//                        {
//                            outInfo.points[j].IsBoundaryPoint = true;
//                            prevPoint = j;
//                            break;
//                        }
//                    }
//                }


//                if (direct)
//                {
//                    for (int j = prevPoint + 1; j < outInfo.points.Count; j++)
//                    {
//                        if (Point.IsPointOnTheSegment(boundPoints[i][0], boundPoints[i][1], outInfo.points[j]))
//                        {
//                            outInfo.points[j].IsBoundaryPoint = true;
//                            outInfo.boundaryPoints[i].Add(new List<int>() { prevPoint, j });
//                            prevPoint = j;
//                        }
//                    }
//                }
//                else
//                {
//                    for (int j = prevPoint - 1; j >= 0; j--)
//                    {

//                        //  if((boundaryPoints[i][1].X-boundaryPoints[i][0].X)*())
//                        if (Point.IsPointOnTheSegment(boundPoints[i][0], boundPoints[i][1], outInfo.points[j]))
//                        {
//                            outInfo.points[j].IsBoundaryPoint = true;
//                            outInfo.boundaryPoints[i].Add(new List<int>() { prevPoint, j });
//                            prevPoint = j;
//                        }
//                    }
//                }
//            }
//            for(int i=0; i<outInfo.points.Count; i++)
//            {
//                if(!IsPointEngaged(outInfo.triangles,i))
//                {
//                    outInfo.points.RemoveAt(i);
//                    ChangeTriangles(outInfo.triangles, i);
//                    i--;
//                }
//            }
           
//            return outInfo;
//        }

//        public static TriangulationStructure TriangulateSquare(double beginPoint, double endPoint, int numOfPartision)
//        {
//            TriangulationStructure outInfo = new TriangulationStructure();
//            outInfo.points = new List<Point>();
//            outInfo.triangles = new List<Triangle>();
//            double h = (endPoint - beginPoint) / numOfPartision;
//            bool isBound = false;
//            for (int i = 0; i < numOfPartision + 1; i++)
//            {

//                double x = beginPoint + i * h;
//                if (x == beginPoint || x == endPoint) isBound = true;
//                else isBound = false;
//                for (int j = 0; j < numOfPartision + 1; j++)
//                {
//                    double y = j * h;
//                    if (isBound == false && (y == 0 || y == endPoint - beginPoint)) isBound = true;
//                    outInfo.points.Add(new Point(x, y, isBound));
//                }
//                x += h / 2.0;
//                if (x > endPoint) break;
//                for (int j = 0; j < numOfPartision; j++)
//                {
//                    double y = j * h;
//                    y += h / 2;
//                    outInfo.points.Add(new Point(x, y));
//                }
//            }

//            for (int i = 0; i < numOfPartision; i++)
//            {

//                double x1 = beginPoint + i * h;
//                double x2 = beginPoint + (i + 1) * h;
//                for (int j = 0; j < numOfPartision; j++)
//                {
//                    double y1 = j * h;
//                    double y2 = (j + 1) * h;
//                    Point a11 = new Point(x1, y1), a21 = new Point(x1, y2), a12 = new Point(x2, y1), a22 = new Point(x2, y2), amidle = new Point((x1 + x2) / 2.0, (y1 + y2) / 2.0);
//                    int p11 = findPoint(outInfo.points, a11);
//                    int p12 = findPoint(outInfo.points, a12);
//                    int p21 = findPoint(outInfo.points, a21);
//                    int p22 = findPoint(outInfo.points, a22);
//                    int pmidle = findPoint(outInfo.points, amidle);
//                    outInfo.triangles.Add(new Triangle(p11, p12, pmidle));
//                    outInfo.triangles.Add(new Triangle(p11, pmidle, p21));
//                    outInfo.triangles.Add(new Triangle(pmidle, p12, p22));
//                    outInfo.triangles.Add(new Triangle(p21, pmidle, p22));
//                }
//            }
//            return outInfo;

//        }

//        public static TriangulationStructure TriangulateSquarePrimitive(double beginPoint, double endPoint, int numOfPartision)
//        {
//            TriangulationStructure outInfo = new TriangulationStructure();
//            outInfo.points = new List<Point>();
//            outInfo.triangles = new List<Triangle>();
//            double h = (endPoint - beginPoint) / numOfPartision;
//            bool isBound = false;
//            for (int i = 0; i < numOfPartision + 1; i++)
//            {

//                double x = beginPoint + i * h;
//                if (x == beginPoint || x == endPoint) isBound = true;
//                else isBound = false;
//                for (int j = 0; j < numOfPartision + 1; j++)
//                {
//                    double y = j * h;
//                    if (isBound == false && (y == 0 || y == endPoint - beginPoint)) isBound = true;
//                    outInfo.points.Add(new Point(x, y, isBound));
//                }
//            }
//            for (int i = 0; i < numOfPartision; i++)
//            {

//                double x1 = beginPoint + i * h;
//                double x2 = beginPoint + (i + 1) * h;
//                for (int j = 0; j < numOfPartision; j++)
//                {
//                    double y1 = j * h;
//                    double y2 = (j + 1) * h;
//                    Point a11 = new Point(x1, y1), a21 = new Point(x1, y2), a12 = new Point(x2, y1), a22 = new Point(x2, y2);
//                    int p11 = findPoint(outInfo.points, a11);
//                    int p12 = findPoint(outInfo.points, a12);
//                    int p21 = findPoint(outInfo.points, a21);
//                    int p22 = findPoint(outInfo.points, a22);
//                    outInfo.triangles.Add(new Triangle(p11, p12, p21));
//                    outInfo.triangles.Add(new Triangle(p21, p12, p22));

//                }
//            }
//            return outInfo;

//        }
//        static int findPoint(List<Point> points, Point point)
//        {
//            int res = -1;
//            for (int i = 0; i < points.Count; i++)
//            {
//                if (point.Equals(points[i])) { res = i; return res; }
//            }
//            return res;
//        }

//        static void removeSomeTriangles(List<Triangle> triangles, int point)
//        {
//            for (int i = 0; i < triangles.Count; i++)
//            {
//                if (triangles[i].DoesContain(point)) { triangles.RemoveAt(i--); }
//            }
//        }

//        static bool IsPointEngaged(List<Triangle> triangles, int point)
//        {
//            foreach(Triangle tr in triangles)
//            {
//                if (tr.A == point || tr.B == point || tr.C == point)
//                    return true;
//            }
//            return false;
//        }

//        static void ChangeTriangles(List<Triangle> triangles, int point)
//        {
//            foreach (Triangle tr in triangles)
//            {
//                if (tr.A > point) tr.A--;
//                if (tr.B > point) tr.B--;
//                if (tr.C > point) tr.C--;
//            }
//        }




//        private static bool ptInPol(Area polygon, double X, double Y)
//        {
//            //початково точка не лежить в області
//            bool res = false;
//            for (int i = 0; i < polygon.Count; i++)
//            {
//                if (polygon.ElementAt(i).X == X && polygon.ElementAt(i).Y == Y) return true;
//            }
//            //перевірка відрізка з точки многокутника і попередньої точки
//            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; i++)
//            {
//                //випадок коли відрізкок лежить на одному рівні по y з точкою яку перевіряємо
//                if ((polygon.ElementAt(i).Y == Y) && (Y == polygon.ElementAt(j).Y))
//                {
//                    //точка ледить справа від відрізка - перетинає сторону
//                    if ((polygon.ElementAt(i).X < X) && (polygon.ElementAt(j).X < X))
//                    {
//                        res = !res;
//                        i++;
//                        j = i;
//                    }
//                    //точка ледить на відрізку -  знаходиься на многокутнику подальших перевірок не треба
//                    if (((polygon.ElementAt(i).X <= X) && (polygon.ElementAt(j).X >= X) && (polygon.ElementAt(i).Y <= Y) && (polygon.ElementAt(j).Y >= Y)) || ((polygon.ElementAt(j).X <= X) && (polygon.ElementAt(i).X >= X) && (polygon.ElementAt(j).Y <= Y) && (polygon.ElementAt(i).Y >= Y)))
//                        return true;
//                }
//                else
//                //точка лежить між початком і кінцем відрізку по у
//                if (((polygon.ElementAt(i).Y <= Y) && (Y < polygon.ElementAt(j).Y)) || ((polygon.ElementAt(j).Y <= Y) && (Y < polygon.ElementAt(i).Y)))
//                {
//                    //проекія тточки на відрізок
//                    double xOnLine = (polygon.ElementAt(j).X - polygon.ElementAt(i).X) * (Y - polygon.ElementAt(i).Y)
//                    / (polygon.ElementAt(j).Y - polygon.ElementAt(i).Y) + polygon.ElementAt(i).X;
//                    //точка співпадає зі своєю проекцією на сторону - лежить в прямокутнику подальших перевірок не треба
//                    if (X == xOnLine) return true;
//                    //точка лежить справа від відрізка є перетин сторони
//                    if (X > xOnLine)
//                    {
//                        res = !res;
//                    }
//                }
//                j = i;
//            }
//            return res;
//        }
        
//    }
//}