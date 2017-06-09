using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriangulationByMinimumAngle;

namespace FormatFEMMatrix
{
    public class Matrix
    {
        double[,] matr;
        public int FirstDimension
        {
            get
            {
                return matr.GetLength(0);
            }
        }
        public int SecondDimension
        {
            get
            {
                return matr.GetLength(1);
            }
        }

        public Matrix(int m,int n)
        {
            matr = new double[m, n];
        }

        public Matrix()
        {

        }
        public Matrix(double[,] matrix)
        {
            matr = matrix;
        }

        public Matrix(double[] vector)
        {
            matr = new double[vector.Length, 1];
            for(int i=0; i<vector.Length; i++)
            {
                matr[i, 0] = vector[i];
            } 
        }

        public static Matrix operator *(Matrix matr1, Matrix matr2)
        {
            Matrix res = new Matrix(matr1.FirstDimension, matr2.SecondDimension);
            for (int i = 0; i < matr1.FirstDimension; i++)
            {

                for (int j = 0; j < matr2.SecondDimension; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < matr1.SecondDimension; k++)
                    {
                        sum += matr1[i, k] * matr2[k, j];
                    }
                    res[i, j] = sum;
                }
            }
            return res;
        }

        public static double[] operator *(Matrix matr, double[] vector)
        {
            double[] res = new double[matr.FirstDimension];
            for (int i = 0; i < matr.FirstDimension; i++)
            {
                double sum = 0;
                for(int j=0; j<vector.Length; j++)
                {
                    sum += matr[i, j] * vector[j];
                }
                res[i] = sum;
            }
            return res;
        }
        public static Matrix operator *(double scalar, Matrix matr)
        {
            Matrix res = new Matrix(matr.FirstDimension, matr.SecondDimension);
            for (int i = 0; i < matr.FirstDimension; i++)
            {
                for (int j = 0; j < matr.SecondDimension; j++)
                    res[i, j] = scalar * matr[i, j];
            }
            return res;
        }


        public static Matrix operator +(Matrix matr1, Matrix matr2)
        {
            Matrix res = new Matrix(matr1.FirstDimension, matr2.SecondDimension);
            for (int i = 0; i < matr1.FirstDimension; i++)
            {

                for (int j = 0; j < matr2.SecondDimension; j++)
                {
                    res[i, j] = matr1[i, j] + matr2[i, j];
                }
            }
            return res;
        }

        public static Matrix operator -(Matrix matr1, Matrix matr2)
        {
            Matrix res = new Matrix(matr1.FirstDimension, matr2.SecondDimension);
            for (int i = 0; i < matr1.FirstDimension; i++)
            {

                for (int j = 0; j < matr2.SecondDimension; j++)
                {
                    res[i, j] = matr1[i, j] - matr2[i, j];
                }
            }
            return res;
        }

        

        public double this[int i,int j]
        {
            get { return matr[i, j]; }
            set { matr[i, j] = value; }
        }

    }
}
