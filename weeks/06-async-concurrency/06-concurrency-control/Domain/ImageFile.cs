public sealed record ImageFile
{
    public string Name { get; init; }
    public int ProcessingTimeMs { get; init; }

    public ImageFile(string name, int processingTimeMs)
    {
        ValidateInitialization(name, processingTimeMs);
        Name = name;
        ProcessingTimeMs = processingTimeMs;
    }

    private static void ValidateInitialization(string name, int processingTimeMs)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Image file name cannot be null or whitespace.", nameof(name));
        }

        if (processingTimeMs < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(processingTimeMs), "Processing time cannot be negative.");
        }
    }
}