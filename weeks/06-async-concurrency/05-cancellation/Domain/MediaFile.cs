public sealed record MediaFile
{
    public string Name { get; init; }
    public int DurationSeconds { get; init; }

    public MediaFile(string name, int durationSeconds)
    {
        ValidateInitialization(name, durationSeconds);
        Name = name;
        DurationSeconds = durationSeconds;
    }

    private void ValidateInitialization(string name, int durationSeconds)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Media file name cannot be null or whitespace.", nameof(name));
        }

        if (durationSeconds < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(durationSeconds), "Duration cannot be negative.");
        }
    }
}