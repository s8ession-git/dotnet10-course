using System.Diagnostics;

public sealed class ConsoleResultPresenter
{
    private readonly IDeploymentReceiptService _deployment;
    private readonly ILoadEnvironmentConfigService _environment;
    private readonly IHealthCheckResultService _health;
    private SequencialTasksExperiment _runScenario1;
    //private ConcurrentTasksExperiment _runScenario2;


    public ConsoleResultPresenter(IDeploymentReceiptService deployment, ILoadEnvironmentConfigService environment, IHealthCheckResultService health)
    {
        ArgumentNullException.ThrowIfNull(deployment);
        ArgumentNullException.ThrowIfNull(environment);
        ArgumentNullException.ThrowIfNull(health);
        _deployment = deployment;
        _environment = environment;
        _health = health;
        _runScenario1 = new SequencialTasksExperiment(_deployment, _environment, _health);
    }
        
    public async Task PrintResultAsync(string version)
    {
        var stopwatch = Stopwatch.StartNew();
        string result = await _runScenario1.RunAsync(version);
        Console.WriteLine(result);
        stopwatch.Stop();
        Console.WriteLine($"Total execution time: {stopwatch.ElapsedMilliseconds} ms");
    }
}