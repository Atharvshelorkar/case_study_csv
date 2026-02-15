using CsvComparison.Core.Interfaces;

namespace CsvComparison.Core.Strategies
{
    public class NumericToleranceStrategy : IComparisonStrategy
    {
        private readonly double _tolerance;

        public NumericToleranceStrategy(double tolerance)
        {
            _tolerance = tolerance;
        }

        public bool Compare(string expected, string actual)
        {
            if (double.TryParse(expected, out var exp) &&
                double.TryParse(actual, out var act))
            {
                return Math.Abs(exp - act) <= _tolerance;
            }

            return string.Equals(expected, actual);
        }
    }
}
