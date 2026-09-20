public sealed record MediaProcessingResult
{
    public string Name { get; init; }
    public bool IsProcessed { get; init; }

    public MediaProcessingResult(string name, bool isProcessed)
    {
        ValidateInitialization(name);
        Name = name;
        IsProcessed = isProcessed;
    }

    private void ValidateInitialization(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Media file name cannot be null or whitespace.", nameof(name));
        }
    }
}