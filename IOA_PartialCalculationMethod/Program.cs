using IOA_PartialCalculationMethod.Algorithm;
using System;

namespace IOA_PartialCalculationMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            var alg = new PartialCalculationMethod();
            alg.ReadNodes("C:\\Users\\miros\\source\\repos\\IOA_PartialCalculationMethod\\IOA_PartialCalculationMethod\\Data\\nodes.txt");
            alg.ReadPossibleCenters("C:\\Users\\miros\\source\\repos\\IOA_PartialCalculationMethod\\IOA_PartialCalculationMethod\\Data\\possible_placements.txt");
            alg.ReadEdges("C:\\Users\\miros\\source\\repos\\IOA_PartialCalculationMethod\\IOA_PartialCalculationMethod\\Data\\edges.txt");
            alg.Solve();
        }
    }
}
