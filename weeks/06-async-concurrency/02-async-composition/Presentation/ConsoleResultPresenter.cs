using System.Diagnostics;

public sealed class ConsoleResultPresenter
{
    private readonly IDeploymentReceiptService _deployment;
    private readonly ILoadEnvironmentConfigService _environment;
    private readonly IHealthCheckResultService _health;
    private RunScenario _runScenario;

    public ConsoleResultPresenter(IDeploymentReceiptService deployment, ILoadEnvironmentConfigService environment, IHealthCheckResultService health)
    {
        ArgumentNullException.ThrowIfNull(deployment);
        ArgumentNullException.ThrowIfNull(environment);
        ArgumentNullException.ThrowIfNull(health);
        _deployment = deployment;
        _environment = environment;
        _health = health;
        _runScenario = new RunScenario(_deployment, _environment, _health);
    }
        
    public async Task PrintResultAsync(string version)
    {
        var stopwatch = Stopwatch.StartNew();
        string result = await _runScenario.RunAsync(version);
        Console.WriteLine(result);
        stopwatch.Stop();
        Console.WriteLine($"Total execution time: {stopwatch.ElapsedMilliseconds} ms");
    }
}