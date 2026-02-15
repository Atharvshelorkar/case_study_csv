namespace CsvComparison.Core.Models
{
    public class ComparisonResult
    {
        public List<string> MissingInActual { get; } = new();
        public List<string> UnexpectedInActual { get; } = new();
        public List<string> FieldFailures { get; } = new();
    }
}
