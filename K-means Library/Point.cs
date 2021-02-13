using System;
using System.Collections.Generic;
using System.Text;

namespace K_means_Library
{
    public class Point : IPoint
    {
        public double x { get; set; }
        public double y { get; set; }

        public Point()
        {
            this.x = 0.0;
            this.y = 0.0;
        }
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public IPoint[] Normalize(IPoint[] DataPoints)
        {
            var NormalizedPoints = new IPoint[DataPoints.Length];
            for (int i = 0; i < DataPoints.Length; i++)
            {
                NormalizedPoints[i] = NormalizePoint(DataPoints[i]);
            }
            return NormalizedPoints;
        }

        public IPoint NormalizePoint(IPoint Pe)
        {
            var P = (Point)Pe;
            if(P.x==0.0 && P.y == 0.0)
            {
                return P;
            }
            double distance = P.distance();

            Point NormalizedPoint = new Point(P.x / distance, P.y / distance);

            return NormalizedPoint;
        }

        public double distance()
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y);
        }

        public IPoint[] UpdateMeans(IPoint[] normalizedPoint, int[] clustering, IPoint[] mean)
        {
            int numClusters = mean.Length;
            foreach (Point P in mean)
            {
                P.x = 0.0;
                P.y = 0.0;
            }
            int[] clusterCounts = new int[numClusters];

            for (int i = 0; i < normalizedPoint.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
                ((Point)mean[cluster]).x += ((Point)normalizedPoint[i]).x;
                ((Point)mean[cluster]).y += ((Point)normalizedPoint[i]).y;
            }
            for (int k = 0; k < mean.Length; ++k)
            {
                if (clusterCounts[k] != 0)
                {
                    ((Point)mean[k]).x /= clusterCounts[k];
                    ((Point)mean[k]).y /= clusterCounts[k];
                }
            }


            return mean;
        }

        public IPoint CalculCentroid(IPoint[] normalizedPoint, int[] clustering, int cluster, IPoint[] mean)
        {
            Point centroid = new Point(0.0, 0.0);
            double minimunDistance = double.MaxValue;
            for (int i = 0; i < normalizedPoint.Length; ++i) // walk thru each data tuple
            {
                int c = clustering[i];
                if (c != cluster) continue;
                double currentDistance = distancePtoP((Point)normalizedPoint[i], (Point)mean[cluster]);
                if (currentDistance < minimunDistance)
                {
                    minimunDistance = currentDistance;
                    centroid.x = ((Point)normalizedPoint[i]).x;
                    centroid.y = ((Point)normalizedPoint[i]).y;
                }
                
            }
            return centroid;

        }

        private double distancePtoP (Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2));
        }


        public bool IsSameCluster(IPoint[] normalizedPoint, int[] clustering, IPoint[] centroids)
        {
            int numClusters = centroids.Length;
            bool isSame = false;
            double[] distances = new double[numClusters];
            for (int i = 0; i < normalizedPoint.Length; ++i)
            {
                for (int k = 0; k < numClusters; ++k)
                {
                    distances[k] = distancePtoP((Point)normalizedPoint[i], (Point)centroids[k]);
                }
                    
                int newCluster = Minimum(distances);
                if (newCluster != clustering[i])
                {
                    isSame = true;
                    clustering[i] = newCluster;
                }
            }
            return isSame;
        }

        public int Minimum(double[] distances)
        {
            int minumumIndex = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    minumumIndex = k;
                }
            }
            return minumumIndex;
        }

        public IPoint[] CreatePointTable(int length)
        {
            var result = new Point[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = new Point();
            }
            return result;
        }
    }
}
