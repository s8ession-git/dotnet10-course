public sealed class MainScenarioPresenter
{
    private readonly MainScenario scenario;

    public MainScenarioPresenter(MainScenario scenario)
    {
        this.scenario = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(MediaFile mediaFile, CancellationToken cancellationToken)
    {
        MediaProcessingResult result = await scenario.ProcessAsync(mediaFile, cancellationToken);
        Console.WriteLine($"{result.Name} -> {result.IsProcessed}");
    }

    public async Task<MediaProcessingResult> ProcessAsync(MediaFile mediaFile, CancellationToken cancellationToken)
    {
        return await scenario.ProcessAsync(mediaFile, cancellationToken);
    }
}