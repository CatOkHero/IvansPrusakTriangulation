using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Triangulation {
	public partial class Form1 : Form {
		public Form1 () {
			InitializeComponent();
			comboBox1.SelectedIndex = 0;
		}

		private void button1_Click ( object sender, EventArgs e ) {
			try {
				double maxArea = Convert.ToDouble( tbxMaxArea.Text );
				TriangulationByMinimumAngle.MinimalAngleAlgorithm.TriangulationStructure outInfo = new TriangulationByMinimumAngle.MinimalAngleAlgorithm.TriangulationStructure();
				
				//задаємо вершини точнок фігури
				if( comboBox1.SelectedIndex == 0 )
					outInfo = TriangulationByMinimumAngle.MinimalAngleAlgorithm.Triangulate( 
						new List<TriangulationByMinimumAngle.Point>() {
							new TriangulationByMinimumAngle.Point( 0, 0 ),
							new TriangulationByMinimumAngle.Point( 2, 0 ),
							new TriangulationByMinimumAngle.Point( 2, 3 ),
							new TriangulationByMinimumAngle.Point( 0, 3 ) }, maxArea );
				else if( comboBox1.SelectedIndex == 1 )
					outInfo = TriangulationByMinimumAngle.MinimalAngleAlgorithm.Triangulate(
						new List<TriangulationByMinimumAngle.Point>() {
							new TriangulationByMinimumAngle.Point( 0, 0 ),
							new TriangulationByMinimumAngle.Point( 2, 0 ),
							new TriangulationByMinimumAngle.Point( 2, 3 ),
							new TriangulationByMinimumAngle.Point( 0, 3 ) }, maxArea );
				else if( comboBox1.SelectedIndex == 2 )
					outInfo = TriangulationByMinimumAngle.MinimalAngleAlgorithm.Triangulate( 
						new List<TriangulationByMinimumAngle.Point>() {
							new TriangulationByMinimumAngle.Point( 0, 1 ),
							new TriangulationByMinimumAngle.Point( 1, 0 ),
							new TriangulationByMinimumAngle.Point( 2, 0 ),
							new TriangulationByMinimumAngle.Point( 0, 2 ) }, maxArea );
				else if( comboBox1.SelectedIndex == 3 )
					outInfo = TriangulationByMinimumAngle.MinimalAngleAlgorithm.Triangulate(
						new List<TriangulationByMinimumAngle.Point>() {
							new TriangulationByMinimumAngle.Point( -3, 0 ),
							new TriangulationByMinimumAngle.Point( 3, 0 ),
							new TriangulationByMinimumAngle.Point( 0, 3 ) }, maxArea );
				else if( comboBox1.SelectedIndex == 4 )
					outInfo = TriangulationByMinimumAngle.MinimalAngleAlgorithm.Triangulate(
						new List<TriangulationByMinimumAngle.Point>() {
							new TriangulationByMinimumAngle.Point( -2, 0 ),
							new TriangulationByMinimumAngle.Point( 2, 0 ),
							new TriangulationByMinimumAngle.Point( 2, 2 ),
							new TriangulationByMinimumAngle.Point( -2, 2 ) }, maxArea );
				else if( comboBox1.SelectedIndex == 5 )
					outInfo = TriangulationByMinimumAngle.MinimalAngleAlgorithm.Triangulate(
						new List<TriangulationByMinimumAngle.Point>() {
							new TriangulationByMinimumAngle.Point( -3, 0 ),
							new TriangulationByMinimumAngle.Point( -1, 0 ),
							new TriangulationByMinimumAngle.Point( 0, 1 ),
							new TriangulationByMinimumAngle.Point( 1, 0 ),
							new TriangulationByMinimumAngle.Point( 3, 0 ),
							new TriangulationByMinimumAngle.Point( 0, 3 ) }, maxArea );
				else
					outInfo = TriangulationByMinimumAngle.MinimalAngleAlgorithm.Triangulate( 
						new List<TriangulationByMinimumAngle.Point>() {
							new TriangulationByMinimumAngle.Point( -3, 0 ),
							new TriangulationByMinimumAngle.Point( -1, -1 ),
							new TriangulationByMinimumAngle.Point( -2, -3 ),
							new TriangulationByMinimumAngle.Point( 0, -2 ),
							new TriangulationByMinimumAngle.Point( 2, -3 ),
							new TriangulationByMinimumAngle.Point( 1, -1 ),
							new TriangulationByMinimumAngle.Point( 3, 0 ),
							new TriangulationByMinimumAngle.Point( 1, 0 ),
							new TriangulationByMinimumAngle.Point( 0, 3 ),
							new TriangulationByMinimumAngle.Point( -1, 0 ) }, maxArea );

				DisplayTriangulation form = new DisplayTriangulation( outInfo );
				form.Show();
				dataGridView1.Columns.Clear();
				dataGridView2.Columns.Clear();
				dataGridView3.Columns.Clear();
				dataGridView1.ColumnCount = 2;
				dataGridView2.ColumnCount = 2;
				List<int> dimensions = new List<int>();
				for( int i = 0; i < outInfo.boundaryPoints.Count; i++ ) {
					dimensions.Add( outInfo.boundaryPoints[i].Count );
				}
				int maxDim = dimensions.Max();
				dataGridView3.ColumnCount = maxDim + 1;
				for( int i = 0; i < outInfo.points.Count; i++ ) {
					dataGridView1.Rows.Add( i, outInfo.points[i] );
				}
				for( int i = 0; i < outInfo.triangles.Count; i++ ) {
					dataGridView2.Rows.Add( i, outInfo.triangles[i] );
				}
				for( int i = 0; i < outInfo.boundaryPoints.Count; i++ ) {
					object[ ] objects = new object[maxDim + 1];
					objects[0] = i;
					for( int j = 0; j < outInfo.boundaryPoints[i].Count; j++ ) {
						objects[j + 1] = string.Format( "[{0}, {1}]", outInfo.boundaryPoints[i][j][0], outInfo.boundaryPoints[i][j][1] );
					}
					for( int j = outInfo.boundaryPoints[i].Count; j < maxDim; j++ ) {
						objects[j + 1] = "";
					}
					dataGridView3.Rows.Add( objects );
				}
				DisplayMatrix matr = new DisplayMatrix( outInfo );
				matr.Show();
				using( StreamWriter swriter = new StreamWriter( "Points.txt" ) ) {
					foreach( var item in outInfo.points ) {
						string s1 = Convert.ToString( item.X );
						string s2 = Convert.ToString( item.Y );
						s1 = s1.Replace( ',', '.' );
						s2 = s2.Replace( ',', '.' );
						swriter.WriteLine( s1 + " " + s2 );
					}
				}
				using( StreamWriter swriter = new StreamWriter( "Triangles.txt" ) ) {
					foreach( var item in outInfo.triangles ) {
						string s1 = Convert.ToString( item.A + 1 );
						string s2 = Convert.ToString( item.B + 1 );
						string s3 = Convert.ToString( item.C + 1 );
						swriter.WriteLine( s1 + " " + s2 + " " + s3 );
					}
				}
			} catch( FormatException ) {
				MessageBox.Show( "Wrong input of Max Area" );
			} catch( IndexOutOfRangeException ) {
				MessageBox.Show( "Something wrong with triangulation" );
			} catch( Exception ex ) {
				MessageBox.Show( ex.Message );
			}


		}

		private void comboBox1_SelectedIndexChanged ( object sender, EventArgs e ) {

		}

		private void label1_Click ( object sender, EventArgs e ) {

		}
	}
}