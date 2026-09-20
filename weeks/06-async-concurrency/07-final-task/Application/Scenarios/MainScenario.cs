public sealed class MainScenario
{
    private readonly IParseDocument parseDocument;

    public MainScenario(IParseDocument parseDocument)
    {
        this.parseDocument = parseDocument ?? throw new ArgumentNullException(nameof(parseDocument));
    }

    public async Task<BatchProcessingReport> ProcessAllAsync(
        IReadOnlyCollection<DocumentFile> documents, 
        int maxConcurrencyLevel, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(documents);
        if (maxConcurrencyLevel <= 0) throw new ArgumentOutOfRangeException(nameof(maxConcurrencyLevel), "Max concurrency level cannot be less than or equal to zero.");

        using SemaphoreSlim semaphore = new(maxConcurrencyLevel);
        int activeCount = 0;
        int processedCount = 0;
        int maxObservedConcurrencyLevel = 0;
        object syncRoot = new();

        List<DocumentProcessingResult> results = new();

        Task[] tasks = documents
            .Select(async documentFile =>
            {
                await semaphore.WaitAsync(cancellationToken);
                int currentActive = Interlocked.Increment(ref activeCount);

                lock (syncRoot)
                {
                    if (currentActive > maxObservedConcurrencyLevel)
                    {
                        maxObservedConcurrencyLevel = currentActive;
                    }
                }
                Task<DocumentProcessingResult> parseTask = parseDocument.ParseAsync(documentFile, cancellationToken);

                try
                {
                    Interlocked.Increment(ref processedCount);
                }
                finally
                {
                    results.Add(await parseTask);
                    Interlocked.Decrement(ref activeCount);
                    semaphore.Release();
                }
            })
            .ToArray();

        await Task.WhenAll(tasks);
        int completedCount = results.Count(result => result.Status == Status.Completed);
        int failedCount = results.Count(result => result.Status == Status.Failed);

        return new BatchProcessingReport(documents.Count, completedCount, failedCount, maxObservedConcurrencyLevel, results);
    }
}