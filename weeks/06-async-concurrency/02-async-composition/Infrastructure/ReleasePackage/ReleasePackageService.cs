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

    public Task<ReleasePackage> GetPackageAsync(string version)
    {
        return DownloadPackageAsync(version);
    }

    public async Task<ReleasePackage> GetValidatedPackageAsync(string version)
    {
        ReleasePackage package =
            await DownloadPackageAsync(version);

        if (package.SizeBytes <= 0)
            throw new InvalidOperationException("Package size cannot be zero.");

        return package;
    }
}