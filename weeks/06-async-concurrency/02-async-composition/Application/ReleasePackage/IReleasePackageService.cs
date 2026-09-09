public interface IReleasePackageService
{
    Task<ReleasePackage> DownloadPackageAsync(string version);
}