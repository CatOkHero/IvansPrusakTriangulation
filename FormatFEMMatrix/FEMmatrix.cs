using System.Collections.Generic;
using TriangulationByMinimumAngle;

namespace FormatFEMMatrix {

	// public delegate double Function(double x1, double x2);
	public static class FEMmatrix {

		#region public functions
		public static List<MatrixesOfFEM> GetMatrixes ( MinimalAngleAlgorithm.TriangulationStructure triangulation, Matrix Amatrix, List<double> beta, List<double> sigma, List<double> uEnvironment, double d, double f ) {
			List<MatrixesOfFEM> res = new List<MatrixesOfFEM>();
			for( int i = 0; i < triangulation.triangles.Count; i++ ) {
				MatrixesOfFEM current = new MatrixesOfFEM();
				current.GrammMatrix = FormatGrammMatrix( triangulation.triangles[i], triangulation.points, Amatrix );
				current.CountoursStructs = GetBoundMatrixAndVector( i, triangulation, beta, sigma, uEnvironment );
				current.ProperScalarProductMatrix = d * GetIntegratedBasisMatrix( triangulation.triangles[i], triangulation.points );
				current.RightPart = GetRightPartVector( triangulation.triangles[i], triangulation.points, f );
				res.Add( current );
			}
			return res;
		}

		#endregion

		#region private functions
		static Matrix FormatGrammMatrix ( Triangle triangle, List<Point> points, Matrix A ) {
			double triangleSquare = 2 * MinimalAngleAlgorithm.getTriangleSquare( triangle, points );
			Matrix res = new Matrix( 3, 3 );
			Point p1 = points[triangle.A];
			Point p2 = points[triangle.B];
			Point p3 = points[triangle.C];
			double bi = p2.Y - p3.Y;
			double ci = p3.X - p2.X;
			double bj = p3.Y - p1.Y;
			double cj = p1.X - p3.X;
			double bm = p1.Y - p2.Y;
			double cm = p2.X - p1.X;
			res[0, 0] = 1 / ( 2.0 * triangleSquare ) * ( A[0, 0] * bi * bi + A[1, 1] * ci * ci );
			res[0, 1] = res[1, 0] = 1 / ( 2.0 * triangleSquare ) * ( A[0, 0] * bi * bj + A[1, 1] * ci * cj );
			res[0, 2] = res[2, 0] = 1 / ( 2.0 * triangleSquare ) * ( A[0, 0] * bi * bm + A[1, 1] * ci * cm );
			res[1, 1] = 1 / ( 2.0 * triangleSquare ) * ( A[0, 0] * bj * bj + A[1, 1] * cj * cj );
			res[1, 2] = res[2, 1] = 1 / ( 2.0 * triangleSquare ) * ( A[0, 0] * bj * bm + A[1, 1] * cj * cm );
			res[2, 2] = 1 / ( 2.0 * triangleSquare ) * ( A[0, 0] * bm * bm + A[1, 1] * cm * cm );
			return res;
		}

		static Matrix GetIntegratedBasisMatrix ( Triangle triangle, List<Point> points ) {
			double triangleSquare = MinimalAngleAlgorithm.getTriangleSquare( triangle, points );
			double[ , ] res = new double[3, 3]
				{ { 2, 1, 1},
				  { 1, 2, 1},
				  { 1, 1, 2},
				};

			return ( 2 * triangleSquare ) / 24.0 * ( new Matrix( res ) );
		}

		static double[ ] GetRightPartVector ( Triangle triangle, List<Point> points, double rightPart ) {
			Matrix matr = GetIntegratedBasisMatrix( triangle, points );
			double[ ] funcValues = new double[ ] { rightPart, rightPart, rightPart };
			return matr * funcValues;
		}

		static Matrix GetBoundIntegralMatrix ( MinimalAngleAlgorithm.TriangulationStructure triangulation, BoundMatrix boundMatr, double sigma, double beta ) {
			Matrix res = new Matrix();
			res = new Matrix( new double[ , ] { { 2, 1 }, { 1, 2 } } );
			double l = Point.GetDistance( triangulation.points[boundMatr.FirstPoint], triangulation.points[boundMatr.SecondPoint] );
			res = ( sigma / beta ) * ( l / 6d ) * res;
			return res;
		}

		static List<BoundMatrix> FindBoundSidesOfTriangle ( int triangle, MinimalAngleAlgorithm.TriangulationStructure trianglulation ) {
			List<BoundMatrix> res = new List<BoundMatrix>();
			for( int i = 0; i < trianglulation.boundaryPoints.Count; i++ ) {
				for( int j = 0; j < trianglulation.boundaryPoints[i].Count; j++ ) {
					if( trianglulation.boundaryPoints[i][j].Contains( trianglulation.triangles[triangle].A ) && trianglulation.boundaryPoints[i][j].Contains( trianglulation.triangles[triangle].B ) ) {
						res.Add( new BoundMatrix() { FirstPoint = trianglulation.boundaryPoints[i][j][0], SecondPoint = trianglulation.boundaryPoints[i][j][1], NumberOfBound = i, Triangle = triangle } );
					} else
					if( trianglulation.boundaryPoints[i][j].Contains( trianglulation.triangles[triangle].A ) && trianglulation.boundaryPoints[i][j].Contains( trianglulation.triangles[triangle].C ) ) {
						res.Add( new BoundMatrix() { FirstPoint = trianglulation.boundaryPoints[i][j][0], SecondPoint = trianglulation.boundaryPoints[i][j][1], NumberOfBound = i, Triangle = triangle } );
					} else
						if( trianglulation.boundaryPoints[i][j].Contains( trianglulation.triangles[triangle].B ) && trianglulation.boundaryPoints[i][j].Contains( trianglulation.triangles[triangle].C ) ) {
						res.Add( new BoundMatrix() { FirstPoint = trianglulation.boundaryPoints[i][j][0], SecondPoint = trianglulation.boundaryPoints[i][j][1], NumberOfBound = i, Triangle = triangle } );
					}
				}
			}
			return res;
		}

		static List<Pair<BoundMatrix, double[ ]>> GetBoundMatrixAndVector ( int triangle, MinimalAngleAlgorithm.TriangulationStructure triangulation, List<double> beta, List<double> sigma, List<double> uEnvironment ) {
			var currentBoundaryMatrix = FindBoundSidesOfTriangle( triangle, triangulation );
			List<Pair<BoundMatrix, double[ ]>> res = new List<Pair<BoundMatrix, double[ ]>>();
			for( int i = 0; i < currentBoundaryMatrix.Count; i++ ) {
				BoundMatrix bmtr = currentBoundaryMatrix[i];
				if( beta[bmtr.NumberOfBound] == 0 ) beta[bmtr.NumberOfBound] = 1e-16;
				//bmtr.Matrix = new Matrix(new double[,] { { 0, 0 }, { 0, 0 } });
				//res.Add(new Pair<BoundMatrix, double[]>(bmtr, new double[] { 0, 0 }));

				Matrix matr = GetBoundIntegralMatrix( triangulation, bmtr, sigma[bmtr.NumberOfBound], beta[bmtr.NumberOfBound] );
				double[ ] array = ( -1 * matr ) * ( new double[ ] { uEnvironment[bmtr.NumberOfBound], uEnvironment[bmtr.NumberOfBound] } );
				bmtr.Matrix = matr;
				res.Add( new Pair<BoundMatrix, double[ ]>( bmtr, array ) );

			}
			return res;
		}


		#endregion

		#region structs
		public struct BoundMatrix {
			public int NumberOfBound;
			public int FirstPoint;
			public int SecondPoint;
			public Matrix Matrix;
			public int Triangle;
		}

		public struct MatrixesOfFEM {
			public Matrix GrammMatrix;
			public Matrix ProperScalarProductMatrix;
			public double[ ] RightPart;
			public List<Pair<BoundMatrix, double[ ]>> CountoursStructs;
		}
		#endregion
	}
}
