using CsvComparison.Core.Models;

namespace CsvComparison.Core.Utilities
{
    public static class ReportWriter
    {
        public static void Write(string path, ComparisonResult result)
        {
            var directory = Path.GetDirectoryName(path);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using var writer = new StreamWriter(path);

            writer.WriteLine("===== CSV Comparison Report =====");

            writer.WriteLine("\nMissing Records:");
            result.MissingInActual.ForEach(writer.WriteLine);

            writer.WriteLine("\nUnexpected Records:");
            result.UnexpectedInActual.ForEach(writer.WriteLine);

            writer.WriteLine("\nField Mismatches:");
            result.FieldFailures.ForEach(writer.WriteLine);
        }
    }
}
