using CsvComparison.Core.Interfaces;

namespace CsvComparison.Core.Strategies
{
    public class ComparisonStrategyFactory
    {
        public static IComparisonStrategy GetStrategy(string type, double tolerance)
        {
            return type switch
            {
                "Numeric" => new NumericToleranceStrategy(tolerance),
                _ => new ExactMatchStrategy()
            };
        }
    }
}
