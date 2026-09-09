public sealed class HealthCheckResultService : IHealthCheckResultAsync
{
    public async Task<HealthCheckResult> CheckHealthAsync()
    {
        bool isHealthy = true;
        int responseTimeMs = 85;

        await Task.Delay(1000);
        
        return new HealthCheckResult(isHealthy, responseTimeMs);
    }
}