using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using K_means_Library;
using Newtonsoft;
using Newtonsoft.Json;

namespace K_means_Windows_Form
{
    public partial class Form1 : Form
    {
        public class PointDouble
        {
            public double X;
            public double Y;

            public PointDouble(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        public Form1()
        {

            InitializeComponent();

            //List<Point> points = new List<Point>();

            //for (int i = 0; i < 5; i++)
            //{
            //  //  points.Add(new Point(GetRandomNumber(0, 200), GetRandomNumber(0, 200)));
            //}

            //foreach (Point f in points)
            //{
            //    chart1.Series["Series1"].Points.AddXY(f.X, f.Y);
            //}

            Application.DoEvents();

        }

        double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string numberOfIterationst = textBox3.Text;
            string numberOfClustersts = textBox2.Text;
            string points = textBox1.Text;
            if(string.IsNullOrWhiteSpace(numberOfIterationst) || string.IsNullOrWhiteSpace(numberOfClustersts) || string.IsNullOrWhiteSpace(points))
            {
                MessageBox.Show("One of the three fields is empty.Please complete them");
                throw new Exception("One of the three fields is empty.Please complete them");
            }
            int numberOfIteration;
            int numberOfCluster;
            bool isParsable = Int32.TryParse(numberOfIterationst, out numberOfIteration);
            if(!isParsable)
            {
                MessageBox.Show("numberOfIterationst is not parsable into an int");
                throw new Exception("numberOfIterationst is not parsable into an int");
            }
            bool isParsable2 = Int32.TryParse(numberOfClustersts, out numberOfCluster);
            if (!isParsable2)
            {
                MessageBox.Show("numberOfClustersts is not parsable into an int");
                throw new Exception("numberOfClustersts is not parsable into  a int");
            }
            try
            {
                K_means_Library.Point[] deser = JsonConvert.DeserializeObject<K_means_Library.Point[]>(points);
                Cluster cluster = new Cluster(new K_means_Library.Point());
                var res = cluster.DoCluster(deser, numberOfCluster, numberOfIteration);

                for(int i = 0; i < numberOfCluster; i++)
                {
                    for (int j = 0; j < res.NormalizedPoints.Length; j++)
                    {
                        if (res.Clustering[j] == i)
                        {
                            if (!chart1.Series.Any(x => x.Name == $"Series{i + 1}"))
                            {
                                chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series($"Series{i + 1}"));
                                chart1.Series[$"Series{i + 1}"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                            }

                            chart1.Series[$"Series{i + 1}"].Points.AddXY(((K_means_Library.Point)res.NormalizedPoints[j]).x, ((K_means_Library.Point)res.NormalizedPoints[j]).y);
                        }
                    }
                    
                }
                for (int i = 0; i < res.Centroids.Length; i++)
                {
                    chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series($"Centroid{i + 1}"));
                    chart1.Series[$"Centroid{i + 1}"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                    chart1.Series[$"Centroid{i + 1}"].Points.AddXY(((K_means_Library.Point)res.Centroids[i]).x, ((K_means_Library.Point)res.Centroids[i]).y);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
