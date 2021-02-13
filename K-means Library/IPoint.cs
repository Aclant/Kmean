using System;
using System.Collections.Generic;
using System.Text;

namespace K_means_Library
{
    public interface IPoint
    {
        IPoint NormalizePoint(IPoint P);
        IPoint[] Normalize(IPoint[] RawPoints);
        double distance();
        IPoint[] UpdateMeans(IPoint[] normalizedPoint, int[] clustering, IPoint[] mean);
        IPoint CalculCentroid(IPoint[] normalizedPoint, int[] clustering, int cluster, IPoint[] mean);
        
        bool IsSameCluster(IPoint[] normalizedPoint, int[] clustering, IPoint[] centroids);
        int Minimum(double[] distances);
        IPoint[] CreatePointTable(int length);
    }
}
