using CsvComparison.Core.Models;
using CsvComparison.Core.Interfaces;

namespace CsvComparison.Core.Services
{
    public class CsvComparisonEngine
    {
        private readonly IComparisonStrategy _strategy;

        public CsvComparisonEngine(IComparisonStrategy strategy)
        {
            _strategy = strategy;
        }

        public ComparisonResult Compare(
            IEnumerable<CsvRecord> expected,
            IEnumerable<CsvRecord> actual,
            string[] keyColumns)
        {
            var result = new ComparisonResult();

            var expectedDict = BuildDictionary(expected, keyColumns);
            var actualDict = BuildDictionary(actual, keyColumns);

            foreach (var key in expectedDict.Keys)
                if (!actualDict.ContainsKey(key))
                    result.MissingInActual.Add(key);

            foreach (var key in actualDict.Keys)
                if (!expectedDict.ContainsKey(key))
                    result.UnexpectedInActual.Add(key);

            foreach (var key in expectedDict.Keys.Intersect(actualDict.Keys))
            {
                var exp = expectedDict[key];
                var act = actualDict[key];

                foreach (var column in exp.Fields.Keys)
                {
                    var expVal = exp.GetValue(column);
                    var actVal = act.GetValue(column);

                    if (!_strategy.Compare(expVal, actVal))
                    {
                        result.FieldFailures.Add(
                            $"Failed Fieldname: {column} | Expected Input Value: \"{expVal}\" | Actual Value: \"{actVal}\" | for record having unique field Name: {exp.GetValue("Name")} with value: \"{exp.GetValue("AccountNumber")}\"");
                    }
                }
            }

            return result;
        }

        private Dictionary<string, CsvRecord> BuildDictionary(
            IEnumerable<CsvRecord> records,
            string[] keyColumns)
        {
            var dict = new Dictionary<string, CsvRecord>();

            foreach (var record in records)
            {
                var key = string.Join("_",
                    keyColumns.Select(k => record.GetValue(k)));

                if (!dict.ContainsKey(key))
                    dict.Add(key, record);
            }

            return dict;
        }
    }
}
