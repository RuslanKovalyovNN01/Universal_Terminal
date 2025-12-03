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

        private async void StartButton_Click(object sender, EventArgs e)
        {
            int count = 10000;      // всего точек
            int batch = 100;        // количество точек, добавляемых за один раз
            double[] data = new double[batch];
            int currentIndex = 0;

            // очищаем график
            chart1.Series[0].Points.Clear();

            for (int i = 0; i < count; i++)
            {
                double y = Math.Sin(i * 0.01); // пример данных
                data[currentIndex++] = y;

                if (currentIndex == batch || i == count - 1)
                {
                    int start = i - currentIndex + 1;

                    // добавляем пакет точек
                    for (int j = 0; j < currentIndex; j++)
                    {
                        chart1.Series[0].Points.AddXY(start + j, data[j]);
                    }

                    chart1.Invalidate(); // обновляем график

                    currentIndex = 0;

                    // небольшая пауза, чтобы UI успевал обновляться
                    await Task.Delay(1);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ReadFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
        }
    }
}
