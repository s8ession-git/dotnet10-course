public sealed record HealthCheckResult(
    bool IsHealthy,
    int responseTimeMs);