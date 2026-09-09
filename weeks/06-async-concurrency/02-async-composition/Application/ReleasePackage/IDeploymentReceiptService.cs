public interface IDeploymentReceiptService
{
    Task<DeploymentReceipt> DeployReleaseAsync(string version);
}