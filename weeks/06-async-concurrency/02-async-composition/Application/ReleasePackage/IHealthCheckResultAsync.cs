public interface IHealthCheckResultAsync
{
    Task<HealthCheckResult> CheckHealthAsync();
}