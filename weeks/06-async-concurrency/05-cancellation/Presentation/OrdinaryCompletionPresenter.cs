using System.Diagnostics;

public sealed class OrdinaryCompletionPresenter
{
    private readonly MainScenario experimentA;

    public OrdinaryCompletionPresenter(MainScenario experiment)
    {
        experimentA = experiment ?? throw new ArgumentNullException(nameof(experiment));
    }

    public async Task ShowAsync(MediaFile mediaFile)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        Task<MediaProcessingResult> task = experimentA.ProcessAsync(mediaFile, CancellationToken.None);

        MediaProcessingResult result = await task;
        Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds} ms");
        stopwatch.Stop();
        Console.WriteLine($"Status: {task.Status}");
        Console.WriteLine($"IsCompleted: {task.IsCompleted}");
        Console.WriteLine($"IsCanceled: {task.IsCanceled}");
        Console.WriteLine($"IsFaulted: {task.IsFaulted}");
        Console.WriteLine($"Result: {result.Name} -> {result.IsProcessed}");
    }
}
