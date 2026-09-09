public sealed class ReleasePackageService : IReleasePackageService
{
    public async Task<ReleasePackage> DownloadPackageAsync(string version)
    {
        if (string.IsNullOrWhiteSpace(version))
            throw new ArgumentException("Version cannot be null or whitespace.", nameof(version));

        await Task.Delay(1500);
        long packageSize = 150_000_000;
        return new ReleasePackage(version, packageSize);
    }
}