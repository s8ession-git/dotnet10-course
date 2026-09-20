public sealed record CatalogSource
{
    public string Name { get; init; }
    public int DelayMs { get; init; }
    public bool ShouldFail { get; init; }
    public int TotalItemsCount { get; init; }

    public CatalogSource(string name, int delayMs, bool shouldFail, int totalItemsCount)
    {
        ValidateInitialization(name, delayMs, totalItemsCount);
        Name = name;
        DelayMs = delayMs;
        ShouldFail = shouldFail;
        TotalItemsCount = totalItemsCount;
    }

    private static void ValidateInitialization(string name, int delayMs, int totalItemsCount)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Source name cannot be null or whitespace.", nameof(name));
        }

        if (delayMs < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(delayMs), "Delay cannot be negative.");
        }

        if (totalItemsCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalItemsCount), "Records count cannot be negative.");
        }
    }
}