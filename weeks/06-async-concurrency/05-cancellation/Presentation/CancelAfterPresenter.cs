public sealed class CancelAfterPresenter
{
    private readonly MainScenario scenarioE;

    public CancelAfterPresenter(MainScenario scenario)
    {
        scenarioE = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(MediaFile mediaFile, int cancellationTokenTime)
    {
        using CancellationTokenSource cts = new CancellationTokenSource();
        cts.CancelAfter(cancellationTokenTime);

        Task<MediaProcessingResult> task = scenarioE.ProcessAsync(mediaFile, cts.Token);

        try { await task; }
        catch (OperationCanceledException)
        { 
            Console.WriteLine("Operation cancelled.");
        }
    }
}