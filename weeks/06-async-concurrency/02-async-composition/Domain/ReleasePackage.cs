public sealed class ReleasePackage
{
    public string Version { get; }
    public long SizeBytes { get; }

    public ReleasePackage(string version, long sizeBytes)
    {
        ValidateInitilState(version, sizeBytes);
        Version = version;
        SizeBytes = sizeBytes;
    }

    private static void ValidateInitilState(string version, long sizeBytes)
    {
        if (string.IsNullOrWhiteSpace(version))
        {
            throw new ArgumentException("Version cannot be null or whitespace.", nameof(version));
        }

        if (sizeBytes < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeBytes), "Size cannot be negative.");
        }
    }
}