namespace CsvComparison.Core.Models
{
    public class CsvRecord
    {
        public Dictionary<string, string> Fields { get; }

        public CsvRecord(Dictionary<string, string> fields)
        {
            Fields = fields;
        }

        public string GetValue(string column)
        {
            return Fields.ContainsKey(column) ? Fields[column] : string.Empty;
        }
    }
}
