using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TriangulationByMinimumAngle;
using FormatFEMMatrix;

namespace Triangulation
{
    public partial class DisplayMatrix : Form
    {
        MinimalAngleAlgorithm.TriangulationStructure triangulation;
        List<FEMmatrix.MatrixesOfFEM> matrixes;
        public DisplayMatrix(MinimalAngleAlgorithm.TriangulationStructure triangulation)
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            this.triangulation = triangulation;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            StringBuilder sbuilder = new StringBuilder("");
            try
            {
                double f = Convert.ToDouble(tbxF.Text);
                double d = Convert.ToDouble(tbxD.Text);
                string[] smas = tbxBeta.Text.Split(new string[] { " ", "" }, StringSplitOptions.RemoveEmptyEntries);
                List<double> beta = new List<double>();
                foreach (string s in smas)
                {
                    beta.Add(Convert.ToDouble(s));
                }
                smas = tbxSigma.Text.Split(new string[] { " ", "" }, StringSplitOptions.RemoveEmptyEntries);
                List<double> sigma = new List<double>();
                foreach (string s in smas)
                {
                    sigma.Add(Convert.ToDouble(s));
                }
                smas = tbxUEnv.Text.Split(new string[] { " ", "" }, StringSplitOptions.RemoveEmptyEntries);
                List<double> uEnv = new List<double>();
                foreach (string s in smas)
                {
                    uEnv.Add(Convert.ToDouble(s));
                }
                smas = tbxA.Text.Split(new string[] { " ", "" }, StringSplitOptions.RemoveEmptyEntries);
                List<double> A = new List<double>();
                foreach (string s in smas)
                {
                    A.Add(Convert.ToDouble(s));
                }
                Matrix Amatrix = new Matrix(new double[,] { { A[0], 0 }, { 0, A[1] } });
                if (beta.Count != triangulation.boundaryPoints.Count || sigma.Count != triangulation.boundaryPoints.Count) throw new ApplicationException();
                for (int i = 0; i < beta.Count; i++)
                {
                    if (beta[i] == 0 && sigma[i] == 0) throw new ApplicationException();
                }
                matrixes = FEMmatrix.GetMatrixes(triangulation, Amatrix, beta, sigma, uEnv, d, f);
                for (int i = 0; i < matrixes.Count; i++)
                {
                    Triangle current = triangulation.triangles[i];
                    sbuilder.Append("Triangle " + i + "\n");
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                            sbuilder.Append(String.Format("{0:f4} ", matrixes[i].GrammMatrix[j, k]));
                        sbuilder.Append("\n");
                    }
                    sbuilder.Append("\n");
                    for (int j = 0; j < matrixes[i].CountoursStructs.Count; j++)
                    {
                        var mmm = matrixes[i].CountoursStructs[j].First;
                        var vvv = matrixes[i].CountoursStructs[j].Second;
                        sbuilder.Append("" + mmm.FirstPoint + " " + mmm.SecondPoint + "\n");
                        for (int k = 0; k < 2; k++)
                        {
                            for (int l = 0; l < 2; l++)
                            {
                                sbuilder.Append(String.Format("{0:f4} ", mmm.Matrix[k, l]));
                            }

                            sbuilder.Append(String.Format("     {0:f4}", vvv[k]) + "\n");
                        }
                        sbuilder.Append("\n");
                    }
                    sbuilder.Append("\n");
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                            sbuilder.Append(String.Format("{0:f4} ", matrixes[i].ProperScalarProductMatrix[j, k]));
                        sbuilder.Append("\n");
                    }
                    sbuilder.Append("\n");
                    for (int k = 0; k < 3; k++)
                        sbuilder.Append(String.Format("{0:f4} ", matrixes[i].RightPart[k]) + "\n");
                    sbuilder.Append("\n");
                    sbuilder.Append("___________________________________________________________________________________\n");
                }
                richTextBox1.Text = sbuilder.ToString(); 
            }
            catch (ApplicationException)
            {
                richTextBox1.Text = "";
                MessageBox.Show("Something wrong. Please, check your input data");
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                GeneralMatrixcs genMatr = new GeneralMatrixcs(SolveFEMSystem.FormatGeneralMatrix.GetMatrixAndRightPart(triangulation, matrixes));
                genMatr.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
