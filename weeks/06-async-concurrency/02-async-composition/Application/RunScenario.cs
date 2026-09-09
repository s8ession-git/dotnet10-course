public sealed class RunScenario
{
    private readonly IDeploymentReceiptService _deployment;
    private readonly ILoadEnvironmentConfigService _environment;
    private readonly IHealthCheckResultService _health;

    public RunScenario(
        IDeploymentReceiptService deployment,
        ILoadEnvironmentConfigService environment,
        IHealthCheckResultService health)
    {
        _deployment = deployment;
        _environment = environment;
        _health = health;
    }

    public async Task<string> RunAsync(string version)
    {
        var IsValidated = false;
        DeploymentReceipt deploymentReceipt = await _deployment.DeployReleaseAsync(version, IsValidated);

        string environmentResult = await _environment.LoadEnvironmentConfigAsync();

        HealthCheckResult healthCheckResult = await _health.CheckHealthAsync(deploymentReceipt);

        string deploymentReceiptResult = string.IsNullOrWhiteSpace(deploymentReceipt.Version)
            ? "N/A"
            : $"Successful (at {deploymentReceipt.DeployedAt:dd.MM.yyyy HH:mm:ss})";

        string result =
            $"Version: {(string.IsNullOrWhiteSpace(version) ? "N/A" : version)}\n" +
            $"Environment: {(string.IsNullOrWhiteSpace(environmentResult) ? "N/A" : environmentResult)}\n" +
            $"Deployment: {deploymentReceiptResult}\n" +
            $"Health: {(healthCheckResult.IsHealthy ? "OK" : "Unhealthy")}\n" +
            $"Response Time: {healthCheckResult.ResponseTimeMs} ms\n";

        return result;
    }
}