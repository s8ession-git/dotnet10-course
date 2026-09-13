public sealed record CatalogSnapshot
{
    public string SourceName { get; init; } = default!;
    public int ItemsCount { get; init; }

    public CatalogSnapshot(string sourceName, int itemsCount)
    {
        ValidateInitialization(sourceName, itemsCount);
        SourceName = sourceName;
        ItemsCount = itemsCount;
    }

    private static void ValidateInitialization(string sourceName, int itemsCount)
    {
        if (string.IsNullOrWhiteSpace(sourceName))
        {
            throw new ArgumentException("Source name cannot be null or whitespace.", nameof(sourceName));
        }

        if (itemsCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(itemsCount), "Items count cannot be negative.");
        }
    }
}