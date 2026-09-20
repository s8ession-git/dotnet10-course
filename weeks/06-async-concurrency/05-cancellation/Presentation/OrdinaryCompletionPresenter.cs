public sealed class OrdinaryCompletionPresenter
{
    private readonly OrdinaryCompletionExperiment experimentA;

    public OrdinaryCompletionPresenter(OrdinaryCompletionExperiment experiment)
    {
        experimentA = experiment ?? throw new ArgumentNullException(nameof(experiment));
    }

    public async Task ShowAsync(MediaFile mediaFile)
    {
        MediaProcessingResult[] results = await experimentA.ProcessAsync(mediaFile);

        foreach (MediaProcessingResult result in results)
        {
            Console.WriteLine($"{result.Name} -> {result.IsProcessed}");
        }
    }
}