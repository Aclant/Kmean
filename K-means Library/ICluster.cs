using System;
using System.Collections.Generic;
using System.Text;

namespace K_means_Library
{
    public interface ICluster
    {
        ClusterResult DoCluster(IPoint[] points, int numberOfCluster, int maxIteration);
    }
}
