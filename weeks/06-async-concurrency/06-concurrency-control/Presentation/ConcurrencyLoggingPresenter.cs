public sealed class ConcurrencyLoggingPresenter
{
    private readonly MainScenario scenario;

    public ConcurrencyLoggingPresenter(MainScenario scenario)
    {
        this.scenario = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(IReadOnlyCollection<ImageFile> images, int maxConcurrencyLevel, CancellationToken cancellationToken)
    {
        ProcessingReport report = await scenario.RunAllAsync(images, maxConcurrencyLevel, cancellationToken);

        Console.WriteLine($"Total: {report.TotalCount}");
        Console.WriteLine($"Processed: {report.ProcessedCount}");
        Console.WriteLine($"Max concurrency: {report.MaxObservedConcurrencyLevel}");
    }
}
