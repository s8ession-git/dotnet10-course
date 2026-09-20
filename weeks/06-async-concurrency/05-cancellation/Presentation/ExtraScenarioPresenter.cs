public sealed class ExtraScenarioPresenter
{
    private readonly MainScenario scenarioG;

    public ExtraScenarioPresenter(MainScenario scenario)
    {
        scenarioG = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(CancellationTokenSource cts)
    {
        Task task = scenarioG.ProcessWithCancellationToken(cts.Token);
        await task;
    }
}