public sealed class HealthCheckResultService : IHealthCheckResultService
{
    public async Task<HealthCheckResult> CheckHealthAsync(DeploymentReceipt deployment)
    {
        if (deployment is null)
            throw new ArgumentNullException(nameof(deployment));

        bool isHealthy = true;
        int responseTimeMs = 85;

        await Task.Delay(1000);
        
        return new HealthCheckResult(isHealthy, responseTimeMs);
    }
}