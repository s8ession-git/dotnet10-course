public sealed class MainScenario
{
    private readonly IImageProcessor imageProcessor;

    public MainScenario(IImageProcessor imageProcessor)
    {
        this.imageProcessor = imageProcessor ?? throw new ArgumentNullException(nameof(imageProcessor));
    }

     public async Task<ProcessingReport> RunAllAsync(IReadOnlyCollection<ImageFile> images, int maxConcurrencyLevel, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(images);

        SemaphoreSlim semaphore = new(maxConcurrencyLevel);
        int activeCount = 0;
        int currentActive = 0;
        int processedCount = 0;
        int updatedActive = 0;

        Task[] tasks = images
            .Select(async image =>
            {
                await semaphore.WaitAsync(cancellationToken);

                currentActive = Interlocked.Increment(ref activeCount);

                try
                {
                    await imageProcessor.ProcessAsync(image, cancellationToken);
                }
                finally
                {
                    updatedActive = Interlocked.Decrement(ref activeCount);
                    processedCount++;
                    semaphore.Release();
                }
            })
            .ToArray();

        await Task.WhenAll(tasks);

        return new ProcessingReport(images.Count, processedCount, maxConcurrencyLevel);
    }

}