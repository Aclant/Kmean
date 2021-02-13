using System;
using K_means_Library;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Point[] DataPoints = new Point[20];
            DataPoints[0] = new Point ( 0.0, 0.0 );
            DataPoints[1] = new Point ( -73.0, 160.0 );
            DataPoints[2] = new Point(59.0, 25.0);
            DataPoints[3] = new Point ( 61.0, 36.0 );
            DataPoints[4] = new Point ( 35.0,75.0 );
            DataPoints[5] = new Point ( -74.0, 80.0 );
            DataPoints[6] = new Point ( 86.0, -230.0 );
            DataPoints[7] = new Point (200.0, 220.0 );
            DataPoints[8] = new Point ( 146.0, 3.0 );
            DataPoints[9] = new Point (88.0, -13.0 );
            DataPoints[10] = new Point ( -46.0, 45.0 );
            DataPoints[11] = new Point ( 34.0, 15.0 );
            DataPoints[12] = new Point ( 68.0, -57.0 );
            DataPoints[13] = new Point ( 57.0, 45.0 );
            DataPoints[14] = new Point ( -68.0, 79.0 );
            DataPoints[15] = new Point (-98.0, -34.0 );
            DataPoints[16] = new Point ( 66.0, 68.0 );
            DataPoints[17] = new Point ( -8.0, 2.0 );
            DataPoints[18] = new Point ( 65.0, 3.0 );
            DataPoints[19] = new Point ( -2.0, -89.0 );
            
            Cluster cluster = new Cluster(new Point());
            var r = cluster.DoCluster(DataPoints, 0, 30);

            Console.Read();
        }
    }
}
