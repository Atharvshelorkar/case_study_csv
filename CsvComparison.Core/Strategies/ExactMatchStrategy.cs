using CsvComparison.Core.Interfaces;

namespace CsvComparison.Core.Strategies
{
    public class ExactMatchStrategy : IComparisonStrategy
    {
        public bool Compare(string expected, string actual)
        {
            return string.Equals(
                expected?.Trim(),
                actual?.Trim(),
                StringComparison.Ordinal);
        }
    }
}
