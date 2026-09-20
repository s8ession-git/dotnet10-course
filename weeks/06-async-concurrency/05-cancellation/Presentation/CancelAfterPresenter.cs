public sealed class CancelAfterPresenter
{
    private int CanciellationTokenTime { get; init; }
    private readonly MainScenario scenarioE;

    public CancelAfterPresenter(MainScenario scenario)
    {
        scenarioE = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(MediaFile mediaFile, int cancellationTokenTime)
    {

        Task<MediaProcessingResult> task = scenarioE.ProcessSomeAsync(mediaFile, cancellationTokenTime);

        try { await task; }
        catch (OperationCanceledException)
        { 
            Console.WriteLine("Operation cancelled.");
        }
    }
}