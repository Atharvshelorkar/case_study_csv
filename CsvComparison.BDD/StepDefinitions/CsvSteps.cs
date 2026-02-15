using CsvComparison.Core.Services;
using CsvComparison.Core.Strategies;
using CsvComparison.Core.Utilities;
using CsvComparison.Core.Models;
using TechTalk.SpecFlow;
using NUnit.Framework;

[Binding]
public class CsvSteps
{
    private readonly ScenarioContext _scenarioContext;
    private ComparisonResult? _result;

    public CsvSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"I load expected file ""(.*)""")]
    public void GivenExpected(string path)
    {
        _scenarioContext["Expected"] = path;
    }

    [When(@"I compare files")]
    public void WhenCompare()
    {
        var parser = new CsvStreamParser();
        var strategy = new ExactMatchStrategy();
        var engine = new CsvComparisonEngine(strategy);

        _result = engine.Compare(
            parser.Parse((string)_scenarioContext["Expected"]),
            parser.Parse((string)_scenarioContext["Expected"]),
            new[] { "AccountNumber", "Name" });
    }

    [Then(@"comparison should complete")]
    public void ThenVerify()
    {
        Assert.That(_result, Is.Not.Null);
    }
}