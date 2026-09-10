public sealed class DeploymentReceiptService : IDeploymentReceiptService
{
    public async Task<DeploymentReceipt> DeployReleaseAsync(string version, bool isValidated)
    {
        ReleasePackageService releasePackageService = new ReleasePackageService();
        ReleasePackage releasePackage =
            isValidated
                ? await releasePackageService.GetValidatedPackageAsync(version)
                : await releasePackageService.GetPackageAsync(version);

        if (releasePackage is null)
            throw new ArgumentNullException(nameof(releasePackage));

        if (releasePackage.SizeBytes <= 0)
            throw new ArgumentOutOfRangeException(nameof(releasePackage.SizeBytes));

        await Task.Delay(1000);

        return new DeploymentReceipt(version, DateTime.Now);
    }
}