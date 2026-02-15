using Microsoft.Extensions.Configuration;
using CsvComparison.Core.Services;
using CsvComparison.Core.Strategies;
using CsvComparison.Core.Utilities;
using CsvComparison.Core.Models;

class Program
{
    static void Main()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false) 
            .Build();

        var settings = config.GetSection("CsvSettings");

        var generateData = bool.TryParse(settings?["GenerateTestData"], out var g) && g;
        var recordCount = int.TryParse(settings?["RecordCount"], out var rc) ? rc : 0;

        var parser = new CsvStreamParser();

        var comparisonType = settings?["ComparisonType"] ?? "Exact";
        var numericTolerance = double.TryParse(settings?["NumericTolerance"], out var nt) ? nt : 0.0;

        var strategy = ComparisonStrategyFactory.GetStrategy( // use GetStrategy safely
            comparisonType,
            numericTolerance
        );

        var engine = new CsvComparisonEngine(strategy);

        if (generateData)
        {
            var generator = new LargeDataGenerator();
            var expectedPath = settings?["ExpectedFilePath"];
            var actualPath = settings?["ActualFilePath"];
            if (!string.IsNullOrEmpty(expectedPath)) generator.Generate(expectedPath, recordCount);
            if (!string.IsNullOrEmpty(actualPath)) generator.Generate(actualPath, recordCount);
            Console.WriteLine("Large test data generated.");
        }

        var expectedFile = settings?["ExpectedFilePath"];
        var actualFile = settings?["ActualFilePath"];
        var keyCols = (settings?["KeyColumns"] ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries);

        var result = engine.Compare(
            expectedFile is not null ? parser.Parse(expectedFile) : Enumerable.Empty<CsvComparison.Core.Models.CsvRecord>(),
            actualFile is not null ? parser.Parse(actualFile) : Enumerable.Empty<CsvComparison.Core.Models.CsvRecord>(),
            keyCols
        );

        var outputPath = settings?["OutputPath"] ?? "ComparisonReport.txt";
        ReportWriter.Write(outputPath, result);

        Console.WriteLine("Comparison Completed Successfully.");
    }
}