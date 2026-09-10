public sealed record HealthCheckResult
{
    public bool IsHealthy { get; }
    public int ResponseTimeMs { get; }

    public HealthCheckResult(bool isHealthy, int responseTimeMs)
    {
        if (responseTimeMs < 0)
            throw new ArgumentOutOfRangeException(nameof(responseTimeMs), "Response time cannot be negative.");

        IsHealthy = isHealthy;
        ResponseTimeMs = responseTimeMs;
    }
}