using CsvComparison.Core.Models;

namespace CsvComparison.Core.Services
{
    public class CsvStreamParser
    {
        public IEnumerable<CsvRecord> Parse(string filePath)
        {
            using var reader = new StreamReader(filePath);

            var headerLine = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(headerLine))
                throw new Exception("CSV file is empty.");

            var headers = headerLine.Split(',');

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var values = line.Split(',');

                if (values.Length != headers.Length)
                    throw new Exception("Column mismatch detected.");

                var dict = new Dictionary<string, string>();
                for (int i = 0; i < headers.Length; i++)
                    dict[headers[i]] = values[i];

                yield return new CsvRecord(dict);
            }
        }
    }
}
