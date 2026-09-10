public interface IHealthCheckResultService
{
    Task<HealthCheckResult> CheckHealthAsync(DeploymentReceipt deployment);
}