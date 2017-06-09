using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Triangulation
{
    public partial class DisplayTriangulation : Form
    {
        public DisplayTriangulation(TriangulationByMinimumAngle.MinimalAngleAlgorithm.TriangulationStructure dataToDispaly)
        {
            InitializeComponent();
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            int i = 0;
            foreach (TriangulationByMinimumAngle.Triangle tr in dataToDispaly.triangles)
            {
                TriangulationByMinimumAngle.Point a1 = dataToDispaly.points[tr.A];
                TriangulationByMinimumAngle.Point a2 = dataToDispaly.points[tr.B];
                TriangulationByMinimumAngle.Point a3 = dataToDispaly.points[tr.C];
                chart1.Series.Add("Region" + i);
                chart1.Series["Region" + i].ChartType = SeriesChartType.Line;
                chart1.Series["Region" + i].BorderWidth = 3;
                chart1.Series["Region" + i].Color = Color.Black;
                chart1.Series["Region" + i++].Points.DataBindXY(new List<double>() { a1.X, a2.X, a3.X, a1.X }, new List<double> { a1.Y, a2.Y, a3.Y, a1.Y });
            }
            
        }
    }
}
