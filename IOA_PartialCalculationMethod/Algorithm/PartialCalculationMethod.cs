using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IOA_PartialCalculationMethod.Algorithm
{
    class PartialCalculationMethod
    {
        private int[] _possibleCenters;
        private int[] _fixedCost;
        private int[] _requirements;
        private double[,] _edges;
        private double[,] _distanceMatrix;

        private int _numOfCenters;
        private int _numOfNodes;
        public void Solve()
        {
            var setKmin1 = new List<HashSet<int>>();
            var setK = new List<HashSet<int>>();
            var tmpKset = new List<HashSet<int>>();

            var setKCost = new List<double>();
            var setKmin1Cost = new List<double>();
            int k = 1;
            HashSet<int> tmpSol;
            double tmpHUF;
            foreach(var num in _possibleCenters)
            {
                tmpSol = new HashSet<int>();
                tmpSol.Add(num);
                setK.Add(tmpSol);
                setKCost.Add(CalculateCost(tmpSol));
            }
            int i = 0;
            int j = 0;
            while(setK.Count != 0)
            {
                ++k;
                setKmin1 = setK;
                setKmin1Cost = setKCost;
                setK = new List<HashSet<int>>();
                setKCost = new List<double>();
                for (i = 0; i < setKmin1.Count-1; ++i)
                {
                    for(j = i+1; j < setKmin1.Count; ++j)
                    {
                        tmpSol = new HashSet<int>(setKmin1[i]);
                        tmpSol.UnionWith(setKmin1[j]);
                        if(tmpSol.Count == k)
                        {
                            tmpHUF = CalculateCost(tmpSol);
                            if(tmpHUF <= setKmin1Cost[i] && tmpHUF <=setKmin1Cost[j])
                            {
                                setK.Add(tmpSol);
                                setKCost.Add(tmpHUF);
                            }
                            
                        }
                    }
                }
            }
            int smallestIndex = -1;
            double smallestValue = double.MaxValue;
            for(i = 0; i < setKmin1Cost.Count; ++i)
            {
                if(setKmin1Cost[i] < smallestValue)
                {
                    smallestValue = setKmin1Cost[i];
                    smallestIndex = i;
                }
            }

            Console.WriteLine($"Smallest value: {smallestValue}");
            string solution = "[";
            i = 0;
            foreach(var center in setKmin1[smallestIndex])
            {
                solution += center + " ,";
                ++i;
            }
            solution = solution.Remove(solution.Length - 1) + "]";
            Console.WriteLine(solution);

            
        }

        public double CalculateCost(HashSet<int> sol)
        {
            int smallestIndex = -1;
            double smallestValue = -1;
            double result = 0;
            double tmp;

            foreach (var num in sol)
                result += _fixedCost[num-1];

            for(int i = 1; i <= _numOfCenters; ++i)
            {
                smallestValue = double.MaxValue;
                foreach(var num in sol)
                {
                    tmp = _distanceMatrix[num, i] * _requirements[i];
                    if (smallestValue > tmp)
                        smallestValue = tmp;
                }
                result += smallestValue;
            }
            return result;
            
        }

        public void ReadPossibleCenters(string path)
        {
            string line;
            string[] split;
            List<int> possibleCenters = new List<int>();
            List<int> fixedCosts = new List<int>();
            using (var sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    split = line.Split(" ");
                    possibleCenters.Add(int.Parse(split[0]));
                    fixedCosts.Add(int.Parse(split[1]));
                }
            }
            _numOfCenters = possibleCenters.Count;
            _possibleCenters = possibleCenters.ToArray();
            _fixedCost = fixedCosts.ToArray();
        }
        public void ReadNodes(string path)
        {
            string line;
            string[] split;
            List<int> requirements = new List<int>();

            using (var sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    split = line.Split(" ");
                    requirements.Add(int.Parse(split[1]));
                }
            }
            _requirements = requirements.ToArray();
            _numOfNodes = requirements.Count;
        }

        public void ReadEdges(string path)
        {
            _edges = new double[_numOfNodes + 1, _numOfNodes + 1];
            string line;
            string[] split;

            for (int i = 1; i < _numOfNodes + 1; i++)
            {
                for (int j = 1; j < _numOfNodes + 1; j++)
                {
                    _edges[i, j] = -1;
                }
            }

            int node1;
            int node2;
            double distance;
            using (var sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    split = line.Split(" ");
                    node1 = int.Parse(split[0]);
                    node2 = int.Parse(split[1]);
                    distance = double.Parse(split[2]);

                    _edges[node1, node2] = distance;
                    _edges[node2, node1] = distance;
                }
            }
            var floyd = new Floyd();
            _distanceMatrix = floyd.CalculatePath(_edges, _numOfNodes);
        }

    }
}
