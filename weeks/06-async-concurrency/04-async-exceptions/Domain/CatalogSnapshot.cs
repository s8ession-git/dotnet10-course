public sealed record CatalogSnapshot
{
    public string Name { get; init; }
    public int TotalItemsCount { get; init; }

    public CatalogSnapshot(string name, int totalItemsCount)
    {
        ValidateInitialization(name, totalItemsCount);
        Name = name;
        TotalItemsCount = totalItemsCount;
    }

    private void ValidateInitialization(string name, int totalItemsCount)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Source name cannot be null or whitespace.", nameof(name));
        }

        if (totalItemsCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalItemsCount), "Records count cannot be negative.");
        }
    }
}