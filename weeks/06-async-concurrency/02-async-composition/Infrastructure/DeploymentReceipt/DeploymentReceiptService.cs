public sealed class DeploymentReceiptService : IDeploymentReceiptService
{
    public async Task<DeploymentReceipt> DeployReleaseAsync(string version)
    {
        ReleasePackage releasePackage = await new ReleasePackageService().DownloadPackageAsync(version);
        if (releasePackage is null)
            throw new ArgumentNullException(nameof(releasePackage));
        
        if (releasePackage.SizeBytes <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(releasePackage.SizeBytes));
        }

        await Task.Delay(1000);

        return new DeploymentReceipt(version, DateTime.Now);
    }
}