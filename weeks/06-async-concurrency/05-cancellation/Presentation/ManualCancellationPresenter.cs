public sealed class ManualCancellationPresenter
{
    private readonly MainScenario ScenarioB;

    public ManualCancellationPresenter(MainScenario scenario)
    {
        ScenarioB = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(MediaFile mediaFile)
    {
        using CancellationTokenSource cts = new CancellationTokenSource();
        Task<MediaProcessingResult> task = ScenarioB.ProcessAsync(mediaFile, cts.Token);

        await Task.Delay(1000);
        cts.Cancel();

        try { await task; }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation cancelled.");
        }
    }
}