public sealed class ExtraScenarioPresenter
{
    private readonly MainScenario scenarioG;

    public ExtraScenarioPresenter(MainScenario scenario)
    {
        scenarioG = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(CancellationToken cancellationToken)
    {
        Task task = scenarioG.ProcessWithCancellationToken(cancellationToken);
        await task;
    }
}