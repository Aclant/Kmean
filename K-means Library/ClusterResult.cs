using System;
using System.Collections.Generic;
using System.Text;

namespace K_means_Library
{
    public class ClusterResult
    {
        public int[] Clustering { get; set; }
        public IPoint[] Centroids { get; set; }
        public IPoint[] NormalizedPoints { get; set; }
    }
}
