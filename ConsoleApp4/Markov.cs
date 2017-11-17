using ConsoleApp4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovGenerator
{
    class Markov
    {
        enum WinnowingStrategies { Randomly, ByOrder }


        public static Generator MakeGenerator(int order, params (string path, int proportion)[] pathOrderRatios)
        {
            int largestProportion= pathOrderRatios.Max(t => t.Item2);
            var ratioAscendingOrder = pathOrderRatios.OrderBy(t => t.Item2);
            var allExceptTWithLargestProportion = ratioAscendingOrder.Take(ratioAscendingOrder.Count() - 1);
            var tWithLargestProportion = ratioAscendingOrder.Last();
            var los = new List<string>();
            foreach ( (string path, int proportion) t in allExceptTWithLargestProportion)
            {
                var reader = new SentenceReader(t.path);
                List<string> allSentences = reader.ReadAll().ToList();
                los.AddRange(winnow(allSentences, (allSentences.Count() / largestProportion) * t.proportion));
                reader.Close();
            }
            var largestReader = new SentenceReader(tWithLargestProportion.Item1);
            los.AddRange(largestReader.ReadAll());
            return new Generator(los, order);
        }

        private static List<T> winnow<T>(List<T> xs, int nToPreserve)
        {
            return xs.Take(nToPreserve).ToList();
        }
    }
}
