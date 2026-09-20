public sealed class MainScenarioPresenter
{
    private readonly MainScenario scenario;

    public MainScenarioPresenter(MainScenario scenario)
    {
        this.scenario = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(IReadOnlyCollection<DocumentFile> documents, int maxConcurrencyLevel, CancellationToken cancellationToken)
    {
        BatchProcessingReport report = await scenario.ProcessAllAsync(documents, maxConcurrencyLevel, cancellationToken);

        Console.WriteLine($"Total: {report.TotalCount}");
        Console.WriteLine($"Completed: {report.CompletedCount}");
        Console.WriteLine($"Failed: {report.FailedCount}");
        Console.WriteLine($"Max concurrency: {report.MaxObservedConcurrencyLevel}");

        foreach (DocumentProcessingResult result in report.Results)
        {
            Console.WriteLine($"{result.DocumentName} -> {result.Status}");
            if (result.Status == Status.Failed)
            {
                Console.WriteLine($"Error: {result.ErrorMessage}");
            }
        }
    }

    public async Task ShowAsyncNoSemaphore(IReadOnlyCollection<DocumentFile> documents, CancellationToken cancellationToken)
    {
        BatchProcessingReport report = await scenario.ProcessAllNoSemaphoreAsync(documents, cancellationToken);

        Console.WriteLine($"Total: {report.TotalCount}");
        Console.WriteLine($"Completed: {report.CompletedCount}");
        Console.WriteLine($"Failed: {report.FailedCount}");
        Console.WriteLine($"Max concurrency: {report.MaxObservedConcurrencyLevel}");

        foreach (DocumentProcessingResult result in report.Results)
        {
            Console.WriteLine($"{result.DocumentName} -> {result.Status}");
            if (result.Status == Status.Failed)
            {
                Console.WriteLine($"Error: {result.ErrorMessage}");
            }
        }
    }
}