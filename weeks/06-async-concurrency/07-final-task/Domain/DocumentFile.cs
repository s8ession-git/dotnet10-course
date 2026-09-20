public sealed record DocumentFile
{
    public string Name { get; init; }
    public int ValidationDelayMs { get; init; }
    public int ExtractionDelayMs { get; init; }
    public int IndexingDelayMs { get; init; }
    public bool ShouldFail { get; init; }

    public DocumentFile(string name, int validationDelayMs, int extractionDelayMs, int indexingDelayMs, bool shouldFail = false)
    {
        ValidateInitialization(name, validationDelayMs, extractionDelayMs, indexingDelayMs);
        Name = name;
        ValidationDelayMs = validationDelayMs;
        ExtractionDelayMs = extractionDelayMs;
        IndexingDelayMs = indexingDelayMs;
        ShouldFail = shouldFail;
    }

    private static void ValidateInitialization(string name, int validationDelayMs, int extractionDelayMs, int indexingDelayMs)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Document file name cannot be null or whitespace.", nameof(name));
        }

        if (validationDelayMs < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(validationDelayMs), "Validation delay cannot be negative.");
        }

        if (extractionDelayMs < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(extractionDelayMs), "Extraction delay cannot be negative.");
        }

        if (indexingDelayMs < 0) 
        {
            throw new ArgumentOutOfRangeException(nameof(indexingDelayMs), "Indexing delay cannot be negative.");
        }
    }

}