using M;
using M.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace WindowsFormsView
{
    public partial class Form2 : Form
    {
        public Form2(ListView.ListViewItemCollection items)
        {
            InitializeComponent();
            DrawGraph(items);
        }

        private void DrawGraph(ListView.ListViewItemCollection items)
        {
            GraphPane pane = zedGraphControl1.GraphPane;

            pane.YAxis.Scale.MajorStep = 1;
            pane.YAxis.Scale.MinorStep = 1;
            pane.YAxis.Title.Text = "Количество студентов";

            pane.XAxis.Scale.MajorStep = 1;
            pane.XAxis.Scale.MinorStep = 1;
            pane.XAxis.Title.Text = "Специализации";

            pane.CurveList.Clear();

            Random r = new Random();

            Dictionary<string, int> specialtiesDistribution = new Dictionary<string, int>();
            foreach (ListViewItem item in items)
            {
                if (specialtiesDistribution.ContainsKey(item.SubItems[1].Text))
                    specialtiesDistribution[item.SubItems[1].Text] += 1;

                else
                    specialtiesDistribution[item.SubItems[1].Text] = 1;
            }

            foreach (var key in specialtiesDistribution.Keys)
            {
                pane.AddBar(key, null, new double[] { specialtiesDistribution[key] }, Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256)));
            }

            pane.BarSettings.MinBarGap = 1.5f;

            zedGraphControl1.AxisChange();
            Invalidate();
        }
    }
}
