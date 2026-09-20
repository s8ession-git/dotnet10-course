public sealed class ConcurrencyLoggingPresenter
{
    private readonly MainImageProcessor imageProcessor;

    public ConcurrencyLoggingPresenter(MainImageProcessor imageProcessor)
    {
        this.imageProcessor = imageProcessor ?? throw new ArgumentNullException(nameof(imageProcessor));
    }

    public async Task ShowAsync(IReadOnlyCollection<ImageFile> images, int maxConcurrencyLevel, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(images);

        SemaphoreSlim semaphore = new(maxConcurrencyLevel);
        int activeCount = 0;

        Task[] tasks = images
            .Select(async image =>
            {
                await semaphore.WaitAsync(cancellationToken);

                int currentActive = Interlocked.Increment(ref activeCount);
                Console.WriteLine($"START {image.Name} | active={currentActive}");

                try
                {
                    await imageProcessor.ProcessAsync(image, cancellationToken);
                }
                finally
                {
                    int updatedActive = Interlocked.Decrement(ref activeCount);
                    Console.WriteLine($"END   {image.Name} | active={updatedActive}");
                    semaphore.Release();
                }
            })
            .ToArray();

        await Task.WhenAll(tasks);
    }
}
