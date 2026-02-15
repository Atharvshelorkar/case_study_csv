namespace CsvComparison.Core.Interfaces
{
    public interface IComparisonStrategy
    {
        bool Compare(string expected, string actual);
    }
}
