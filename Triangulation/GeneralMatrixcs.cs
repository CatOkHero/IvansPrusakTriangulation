using System;
using System.Windows.Forms;
using FormatFEMMatrix;
using System.IO;

namespace Triangulation
{
    public partial class GeneralMatrixcs : Form
    {
        public GeneralMatrixcs(Pair<double[,],double[]> matrixAndVector)
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 0;
            dataGridView1.ColumnCount = matrixAndVector.Second.Length+2;
            for(int i=0; i<matrixAndVector.Second.Length; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                for (int j=0; j<matrixAndVector.Second.Length; j++)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = String.Format("{0:f4}", matrixAndVector.First[i, j]) });
                }
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = " " });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = String.Format("{0:f4}", matrixAndVector.Second[i]) });
                dataGridView1.Rows.Add(row);
            }
            dataGridView1.Rows.Add();

            double[] res = SolveFEMSystem.MatrixOperations.SolveByGauss(matrixAndVector.First, matrixAndVector.Second);
            DataGridViewRow row1 = new DataGridViewRow();
            foreach (double d in res)
            {
                row1.Cells.Add(new DataGridViewTextBoxCell() { Value = String.Format("{0:f4}", d) });
            }
            row1.Cells.Add(new DataGridViewTextBoxCell() { Value = " " });
            row1.Cells.Add(new DataGridViewTextBoxCell() { Value = " " });
            dataGridView1.Rows.Add(row1);
            using (StreamWriter swriter = new StreamWriter("Solution.txt"))
            {
                foreach(double item in res)
                {
                    string s = Convert.ToString(item);
                    s = s.Replace(',', '.');
                    swriter.WriteLine(s);
                }
            }
        }

    }
}
