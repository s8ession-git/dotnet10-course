public sealed class PropagationTokenPresenter
{
    private readonly MainScenario scenarioF;

    public PropagationTokenPresenter(MainScenario scenario)
    {
        scenarioF = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(MediaFile mediaFile, CancellationToken cancellationToken)
    {
        Task<MediaProcessingResult> task = scenarioF.ProcessAsync(mediaFile, cancellationToken);

        try { await task; }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation cancelled.");
        }

        Console.WriteLine($"Task status: {task.Status}");
        Console.WriteLine($"Task is completed: {task.IsCompleted}");
        Console.WriteLine($"Task is canceled: {task.IsCanceled}");
        Console.WriteLine($"Task is faulted: {task.IsFaulted}");
    }
}