using System;
using System.Collections.Generic;
using System.Text;

namespace K_means_Library
{
    public class Cluster: ICluster
    {
        private readonly IPoint _point;
        public Cluster(IPoint point)
        {
            _point = point;
        }


        public ClusterResult DoCluster(IPoint[] points, int numberOfCluster, int maxIteration)
        {
            if(points.Length==0 || numberOfCluster == 0 || maxIteration == 0)
            {
                ClusterResult clusterDoneZero = new ClusterResult()
                {
                    Centroids = new Point [0],
                    Clustering = new int[0],
                    NormalizedPoints = new Point[0]
                };
                return clusterDoneZero;
            }
            
            //Point[] normalizedPoints = new Point[points.Length];
            var normalizedPoints = _point.Normalize(points);
            int[] clustering = StartClusturing(normalizedPoints.Length, numberOfCluster);

            var mean = _point.CreatePointTable(numberOfCluster);
            var centroid = _point.CreatePointTable(numberOfCluster);

            mean = _point.UpdateMeans(normalizedPoints, clustering, mean);
            centroid = UpdateCentroid(normalizedPoints, clustering, mean, centroid);

            bool isSame = true;
            int count = 0;
            while (isSame == true && count < maxIteration)
            {
                ++count;
                isSame = _point.IsSameCluster(normalizedPoints, clustering, centroid);
                mean = _point.UpdateMeans(normalizedPoints, clustering, mean);
                centroid = UpdateCentroid(normalizedPoints, clustering, mean, centroid);
            }

            ClusterResult clusterDone = new ClusterResult()
            {
                Centroids = centroid,
                Clustering = clustering,
                NormalizedPoints = normalizedPoints
            };
            return clusterDone;
        }

        private int[] StartClusturing(int numberOfTurple, int numberOfCluster)
        {
            Random rand = new Random(0);
            int[] Clustering = new int[numberOfTurple];
            for (int i = 0; i < numberOfCluster; ++i)
                Clustering[i] = i;
            for (int i = numberOfCluster; i < Clustering.Length; ++i)
                Clustering[i] = rand.Next(0, numberOfCluster);
            return Clustering;
        }

        private IPoint[] UpdateCentroid(IPoint[] normalizedPoint, int[] clustering, IPoint[] mean, IPoint[] centroids)
        {
            for (int k = 0; k < centroids.Length; ++k)
            {
                var centroid = _point.CalculCentroid(normalizedPoint, clustering, k, mean);
                centroids[k] = centroid;
            }
            return centroids;
        }

    }
}
