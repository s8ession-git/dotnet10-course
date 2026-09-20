public sealed class UnsafeLoggingPresenter
{
    private readonly MainScenario scenario;

    public UnsafeLoggingPresenter(MainScenario scenario)
    {
        this.scenario = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(IReadOnlyCollection<ImageFile> images, CancellationToken cancellationToken)
    {
        ProcessingUnsafeReport report = await scenario.RunAllUnsafeAsync(images, cancellationToken);

        Console.WriteLine($"Expected: {report.ExpectedCount}");
        Console.WriteLine($"Actual: {report.ActualCount}");
    }
}