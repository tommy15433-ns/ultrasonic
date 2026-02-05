using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace _2022_Test.NSTEK.Plot
{
    public class FocalLawPlot
    {
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_prob;

        public FocalLawPlot(Panel parent)
        {
            chart_prob = new System.Windows.Forms.DataVisualization.Charting.Chart();

            parent.Controls.Add(chart_prob);
            chart_prob.Dock = DockStyle.Fill;
            chart_prob.ChartAreas.Add("focal laws");
            chart_prob.Legends.Add(new Legend());
        }

        public void clear()
        {
            chart_prob.Series.Clear();
        }
        public void add_prob_conf(string name, double[] delay, int x_offset = 0)
        {

            chart_prob.Series.Add(name);
            chart_prob.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart_prob.Series[name].BorderWidth = 3;
            chart_prob.Series[name].IsVisibleInLegend = true;
            //chart_prob.Series[name].LegendText = name;

            Guid id = Guid.NewGuid();
            Random r = new Random(id.GetHashCode());

            chart_prob.Series[name].Color = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), 0);

            for (int i = 0; i < delay.Length; i++)
            {
                chart_prob.Series[name].Points.AddXY(i + x_offset, delay[i]);
            }
            chart_prob.ChartAreas[0].RecalculateAxesScale();
            
        }

    }
}
