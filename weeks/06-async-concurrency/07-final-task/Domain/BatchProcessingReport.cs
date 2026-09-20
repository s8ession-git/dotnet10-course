public sealed record BatchProcessingReport
{
    public int TotalCount { get; init; }
    public int CompletedCount { get; init; }
    public int FailedCount { get; init; }
    public int MaxObservedConcurrencyLevel { get; init; }
    public IReadOnlyCollection<DocumentProcessingResult> Results { get; init; }

    public BatchProcessingReport(int totalCount, int completedCount, int failedCount, int maxObservedConcurrencyLevel, IReadOnlyCollection<DocumentProcessingResult> results)
    {
        ValidateInitialization(totalCount, completedCount, failedCount, maxObservedConcurrencyLevel, results);
        TotalCount = totalCount;
        CompletedCount = completedCount;
        FailedCount = failedCount;
        MaxObservedConcurrencyLevel = maxObservedConcurrencyLevel;
        Results = results;
    }

    private static void ValidateInitialization(int totalCount, int completedCount, int failedCount, int maxObservedConcurrencyLevel, IReadOnlyCollection<DocumentProcessingResult> results)
    {
        if (totalCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalCount), "Total count cannot be negative.");
        }

        if (completedCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(completedCount), "Completed count cannot be negative.");
        }

        if (failedCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(failedCount), "Failed count cannot be negative.");
        }

        if (maxObservedConcurrencyLevel < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxObservedConcurrencyLevel), "Max observed concurrency level cannot be negative.");
        }
    }
}