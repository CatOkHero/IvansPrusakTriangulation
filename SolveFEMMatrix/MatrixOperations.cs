using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SolveFEMSystem
{
    public static class MatrixOperations
    {
        public static double[,] TransponeMatrix(double [,] input)
        {
            int m = input.GetLength(0);
            int n = input.GetLength(1);
            double[,] output = new double[n, m];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    output[j, i] = input[i, j];
                }
            return output;
        }

        public static double [,] MatrixByMatrix(double[,] matr1, double[,] matr2)
        {

            if(matr1.GetLength(1)!=matr2.GetLength(0))
            {
                
                System.Environment.Exit(0);
                return null;
            }
            else
            {
                int m = matr1.GetLength(0);
                int n = matr2.GetLength(1);
                int l = matr1.GetLength(1);
                double[,] res = new double[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++) 
                    {
                        double sum = 0;
                        for (int k = 0; k < l; k++)
                        {
                            sum += matr1[i, k] * matr2[k, j];
                        }
                        res[i, j] = sum;
                    }
                return res;
            }
        }

        public static double [] MatrixByColumn(double[,] matr, double[]  vect)
        {
            if(matr.GetLength(1)!=vect.Length)
            {
                System.Environment.Exit(0);
                return null;
            }
            else 
            {
                int n = matr.GetLength(0);
                int l = vect.Length;
                double[] res = new double[n];
                for(int i=0; i<n; i++)
                {
                    double sum = 0;
                    for(int k = 0; k<l; k++)
                    {
                        sum+=matr[i,k]*vect[k];
                    }
                    res[i] = sum;
                }
                return res;
            }
        }

        public static double[] SolveByJacobi(double[,] matr, double[] vect,double[] prev)
        {
            const double eps = 0.0001;
            
            int n=vect.Length;
            double[] TempX = new double[n];
            double norm; 
            do
            {
                for (int i = 0; i < n; i++)
                {
                    TempX[i] = vect[i];
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j)
                            TempX[i] -= matr[i,j] * prev[j];
                    }
                    TempX[i] /= matr[i,i];
                }
                norm = Math.Abs(prev[0] - TempX[0]);
                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(prev[i] - TempX[i]) > norm)
                        norm = Math.Abs(prev[i] - TempX[i]);
                    prev[i] = TempX[i];
                }
            } while (norm > eps);
            return prev;
        }

        public static double[] SolveByGauss(double[,] matrix, double[] vect)
        {
           
            int i, j, n;
             n = vect.Length;
            double[,] qqq = new double[n, n];
            int k;
            double[] xx = new double[n];

            for (k = 0; k < n - 2; k++)
            {
                double max = Math.Abs(matrix[k, k]);
                int maxpos = k;
                for (int m = k + 1; m < n; m++) 
                {
                    if (max < Math.Abs(matrix[m, k]))
                    {
                        max = Math.Abs(matrix[m, k]);
                        maxpos = m;
                    }
                }
                for (int l = k; l < n; l++)
                {
                    double temp = matrix[k, l];
                    matrix[k, l] = matrix[maxpos, l];
                    matrix[maxpos, l] = temp;
                }
                double temp1 = vect[k];
                vect[k] = vect[maxpos];
                vect[maxpos] = temp1;
                if(max==0)                
                {
                    throw new Exception("there is no main elements");
                }
                    for (i = k + 1; i < n; i++)
                    {
                        qqq[i, k] = -matrix[i, k] / matrix[k, k];
                        vect[i] += qqq[i, k] * vect[k];
                        for (j = k + 1; j < n; j++)
                        {
                            matrix[i, j] += qqq[i, k] * matrix[k, j];
                        }
                    }
            }
             xx[n - 1] = vect[n - 1] / matrix[n - 1, n - 1];
             for (i = n - 2; i >= 0; i--)
             {

                 xx[i] = vect[i];
                 for (j = i + 1; j < n; j++) xx[i] -= matrix[i, j] * xx[j];
                 xx[i] /= matrix[i, i];
             }
             return xx;
        }
    }
}

