using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimMetrics.Net.API;
using SimMetrics.Net.Metric;

#pragma warning disable CS1591
namespace Unimpressive.Core
{
    public struct StringDistanceResult
    {
        public double Similarity;
        public double UnnormalizedSimilarity;
        public string SimilarityDescription;
        public AbstractStringMetric MetricAlgorithm;
        public string ShortDescription;
        public string LongDescription;
    }
    public enum DistanceMetric
    {
        Block,
        ChapmanLengthDeviation,
        ChapmanMeanLength,
        CosineSimilarity,
        DiceSimilarity,
        EuclideanDistance,
        JaccardSimilarity,
        Jaro,
        JaroWinkler,
        Levenstein,
        MatchingCoefficient,
        MongeElkan,
        NeedlemanWunch,
        OverlapCoefficient,
        QGramsDistance,
        SmithWaterman
    }

    public static class MetricExtensions
    {
        public static StringDistanceResult MeasureDistance(this AbstractStringMetric metric, string a, string b)
        {
            return new StringDistanceResult
            {
                Similarity = metric.GetSimilarity(a, b),
                UnnormalizedSimilarity = metric.GetUnnormalisedSimilarity(a, b),
                MetricAlgorithm = metric,
                SimilarityDescription = metric.GetSimilarityExplained(a, b),
                ShortDescription = metric.ShortDescriptionString,
                LongDescription = metric.LongDescriptionString
            };
        }
    }
    public class StringDistance
    {
        public StringDistanceResult[] Results { get; protected set; }

        protected Func<AbstractStringMetric>[] Metrics = {
            () => new BlockDistance(),
            () => new ChapmanLengthDeviation(),
            () => new ChapmanMeanLength(),
            () => new CosineSimilarity(),
            () => new DiceSimilarity(),
            () => new EuclideanDistance(),
            () => new JaccardSimilarity(),
            () => new Jaro(),
            () => new JaroWinkler(),
            () => new Levenstein(),
            () => new MatchingCoefficient(),
            () => new MongeElkan(),
            () => new NeedlemanWunch(),
            () => new OverlapCoefficient(),
            () => new QGramsDistance(),
            () => new SmithWaterman()
        };

        public StringDistance(string a, string b, params DistanceMetric[] usedMetrics)
        {
            Results = usedMetrics.Distinct().Select(metric => Metrics[(int)metric].Invoke().MeasureDistance(a, b)).ToArray();
        }
    }
}

#pragma warning restore CS1591