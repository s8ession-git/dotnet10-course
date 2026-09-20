public sealed class ManualCancellationStatusPresenter
{
    private readonly MainScenario scenarioD;

    public ManualCancellationStatusPresenter(MainScenario scenario)
    {
        scenarioD = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(MediaFile mediaFile)
    {
        using CancellationTokenSource cts = new CancellationTokenSource();
        Task<MediaProcessingResult> task = scenarioD.ProcessFramesAsync(mediaFile, cts.Token);

        await Task.Delay(300);
        cts.Cancel();

        try
        {
            await task;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation cancelled at ThrowIfCancellationRequested().");
        }

        Console.WriteLine($"Task status: {task.Status}");
        Console.WriteLine($"Task is completed: {task.IsCompleted}");
        Console.WriteLine($"Task is canceled: {task.IsCanceled}");
        Console.WriteLine($"Task is faulted: {task.IsFaulted}");
    }
}
