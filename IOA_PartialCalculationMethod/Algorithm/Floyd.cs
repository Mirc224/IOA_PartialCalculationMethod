using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IOA_PartialCalculationMethod.Algorithm
{
    class Floyd
    {

        public Floyd()
        {

        }

        public double[,] CalculatePath(double[,] edges, int numOfNodes)
        {
            var dist = new double[numOfNodes + 1, numOfNodes + 1];
            var next = new int[numOfNodes + 1, numOfNodes + 1];

           for (int i = 1; i <= numOfNodes; i++)
            {
                for (int j = 1; j <= numOfNodes; j++)
                {
                    dist[i, j] = double.MaxValue;
                    next[i, j] = -1;
                }
                dist[i, i] = 0;
                next[i, i] = i;
            }
            double actualDistance = -1;

            for (int i = 1; i <= numOfNodes; i++)
            {
                for (int j = 1; j <= numOfNodes; j++)
                {
                    actualDistance = edges[i, j];
                    if (actualDistance != -1)
                    {
                        dist[i, j] = actualDistance;
                        next[i, j] = j;
                    }
                }
            }

                    

            for (int k = 1; k <= numOfNodes; k++)
            {
                for (int i = 1; i <= numOfNodes; i++)
                {
                    for (int j = 1; j <= numOfNodes; j++)
                    {
                        if (dist[i, j] > dist[i, k] + dist[k, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                            next[i, j] = next[i, k];
                        }
                    }
                }
            }

            return dist;
        }

    }
}
