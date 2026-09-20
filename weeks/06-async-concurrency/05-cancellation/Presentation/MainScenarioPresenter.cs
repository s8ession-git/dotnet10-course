public sealed class MainScenarioPresenter
{
    private readonly MainScenario scenario;

    public MainScenarioPresenter(MainScenario scenario)
    {
        this.scenario = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(MediaFile mediaFile, CancellationToken cancellationToken)
    {
        MediaProcessingResult result = await this.scenario.ProcessAsync(mediaFile, cancellationToken);
        Console.WriteLine($"{result.Name} -> {result.IsProcessed}");
    }
}