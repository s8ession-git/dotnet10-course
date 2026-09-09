public static class RunScenario
{
    public static async Task<string> RunAsync(string version)
    {
        var deploymentReceiptService = new DeploymentReceiptService();
        DeploymentReceipt deploymentReceipt = await deploymentReceiptService.DeployReleaseAsync(version);

        var environmentConfigService = new IndependentService();
        string environmentResult = await environmentConfigService.LoadEnvironmentConfigAsync();

        var healthCheckResultService = new HealthCheckResultService();
        HealthCheckResult healthCheckResult = await healthCheckResultService.CheckHealthAsync();

        string deploymentReceiptResult = string.IsNullOrWhiteSpace(deploymentReceipt.Version)
            ? "N/A"
            : $"Successfull (at {deploymentReceipt.DeployedAt})";

        
        string result = 
            $"Version: {version} \n" +
            $"Environment: {environmentResult} \n" +
            $"Deployment: {deploymentReceiptResult} \n" +
            $"Health: {(healthCheckResult.IsHealthy ? "OK" : "Unhealthy")} \n" +
            $"Response Time: {healthCheckResult.responseTimeMs} ms \n";

        return result;
    }
}