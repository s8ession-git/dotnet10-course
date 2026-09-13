public sealed record CatalogSource
{
    public string SourceName { get; init; } = default!;
    public int DelayMs { get; init; }
    public bool ShouldFail { get; init; }
    public int RecordsCount { get; init; }

    public CatalogSource(string sourceName, int delayMs, bool shouldFail, int recordsCount)
    {
        ValidateInitialization(sourceName, delayMs, recordsCount);
        SourceName = sourceName;
        DelayMs = delayMs;
        ShouldFail = shouldFail;
        RecordsCount = recordsCount;
    }

    private static void ValidateInitialization(string sourceName, int delayMs, int recordsCount)
    {
        if (string.IsNullOrWhiteSpace(sourceName))
        {
            throw new ArgumentException("Source name cannot be null or whitespace.", nameof(sourceName));
        }

        if (delayMs < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(delayMs), "Delay cannot be negative.");
        }

        if (recordsCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(recordsCount), "Records count cannot be negative.");
        }
    }
}