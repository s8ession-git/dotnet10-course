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
        int maxObservedConcurrencyLevel = 0;
        object syncRoot = new();


        Task<DocumentProcessingResult>[] tasks= documents.Select(async documentFile =>
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
            try
            {
                return await parseDocument.ParseAsync(documentFile, cancellationToken);
            }
            finally
            {
                Interlocked.Decrement(ref activeCount);
                semaphore.Release();
            }
        }).ToArray();

        DocumentProcessingResult[] results = await Task.WhenAll(tasks);

        int completedCount = results.Count(result => result.Status == Status.Completed);
        int failedCount = results.Count(result => result.Status == Status.Failed);

        return new BatchProcessingReport(documents.Count, completedCount, failedCount, maxObservedConcurrencyLevel, results);
    }
}