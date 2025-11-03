using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Terminal_v0._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                int count = 10000;
                int batch = 50;
                double[] data = new double[batch];
                int currentIndex = 0;
                chart1.Invoke((Action)(() => chart1.Series[0].Points.Clear()));
                for(int i = 0; i<count; i++)
                {
                    double y = Math.Sin(i * 0.01);
                    data[currentIndex++] = y;
                    if(currentIndex == batch || i == count -1)
                    {
                        int start = i - currentIndex + 1;
                        var points = data.Take(currentIndex).Select((v, idx) => new DataPoint(start + idx, v)).ToArray();
                        chart1.Invoke((Action)(() =>
                        {
                            foreach (var p in points)
                            {
                                chart1.Series[0].Points.Add(p);
                            }
                            chart1.Invalidate();
                        }));
                        currentIndex = 0;
                    }
                    Thread.Sleep(1);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
