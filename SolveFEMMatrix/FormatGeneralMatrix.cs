using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriangulationByMinimumAngle;
using FormatFEMMatrix;

namespace SolveFEMSystem
{
    public static class FormatGeneralMatrix
    {
        public static Pair<double[,],double[]> GetMatrixAndRightPart(MinimalAngleAlgorithm.TriangulationStructure triangulation,List<FEMmatrix.MatrixesOfFEM> femMatrixes)
        { 
            double[,] generalMatrix = new double[triangulation.points.Count, triangulation.points.Count];
            double[] rightPart = new double[triangulation.points.Count];
            for(int i=0; i<femMatrixes.Count; i++)
            {
                Triangle tr = triangulation.triangles[i];
                Matrix matr = femMatrixes[i].GrammMatrix;
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        generalMatrix[tr[j], tr[k]] += matr[j, k];
                matr = femMatrixes[i].ProperScalarProductMatrix;
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        generalMatrix[tr[j], tr[k]] += matr[j, k];
                var boundsMatr = femMatrixes[i].CountoursStructs;
                for(int j=0; j<boundsMatr.Count; j++)
                {
                    int[] indexes = new int[] { boundsMatr[j].First.FirstPoint, boundsMatr[j].First.SecondPoint };
                    for (int k = 0; k < 2; k++)
                    {
                        for (int l = 0; l < 2; l++)
                            generalMatrix[indexes[k], indexes[l]] += boundsMatr[j].First.Matrix[k, l];
                        rightPart[indexes[k]] += -boundsMatr[j].Second[k];
                    }
                }
                for(int j=0; j<3; j++)
                {
                    rightPart[tr[j]] += femMatrixes[i].RightPart[j];
                }
            }
            return new Pair<double[,], double[]>(generalMatrix,rightPart);
        }
    }
}
