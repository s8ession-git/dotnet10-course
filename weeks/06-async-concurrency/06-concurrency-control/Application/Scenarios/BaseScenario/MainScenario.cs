public sealed class MainScenario
{
    private readonly IImageProcessor imageProcessor;

    public MainScenario(IImageProcessor imageProcessor)
    {
        this.imageProcessor = imageProcessor ?? throw new ArgumentNullException(nameof(imageProcessor));
    }

    public async Task<ProcessingUnsafeReport> RunAllUnsafeAsync(IReadOnlyCollection<ImageFile> images, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(images);

        int activeCount = 0;
        int processedCount = 0;

        Task[] tasks = images
            .Select(async image =>
            {
                int currentActive = Interlocked.Increment(ref activeCount);

                try
                {
                    await imageProcessor.ProcessAsync(image, cancellationToken);
                    processedCount++;
                }
                finally
                {
                    Interlocked.Decrement(ref activeCount);
                }
            })
            .ToArray();

        await Task.WhenAll(tasks);

        return new ProcessingUnsafeReport(images.Count, processedCount);
    }

    public async Task<ProcessingReport> RunAllAsync(IReadOnlyCollection<ImageFile> images, int maxConcurrencyLevel, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(images);
        if (maxConcurrencyLevel <= 0) throw new ArgumentOutOfRangeException(nameof(maxConcurrencyLevel), "Max concurrency level cannot be less than or equal to zero.");

        using SemaphoreSlim semaphore = new(maxConcurrencyLevel);
        int activeCount = 0;
        int processedCount = 0;
        int maxObservedConcurrencyLevel = 0;
        object syncRoot = new();

        Task[] tasks = images
            .Select(async image =>
            {
                await semaphore.WaitAsync(cancellationToken);
                int currentActive = Interlocked.Increment(ref activeCount);

                lock (syncRoot)
                {
                    if (currentActive > maxObservedConcurrencyLevel)
                    {
                        maxObservedConcurrencyLevel = currentActive;
                    }
                }

                try
                {
                    await imageProcessor.ProcessAsync(image, cancellationToken);
                    Interlocked.Increment(ref processedCount);
                }
                finally
                {
                    Interlocked.Decrement(ref activeCount);
                    semaphore.Release();
                }
            })
            .ToArray();

        await Task.WhenAll(tasks);

        return new ProcessingReport(images.Count, processedCount, maxObservedConcurrencyLevel);
    }
 public async Task<ProcessingReport> RunAllUnboundedAsync(IReadOnlyCollection<ImageFile> images, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(images);

        int activeCount = 0;
        int processedCount = 0;
        int maxObservedConcurrencyLevel = 0;
        object syncRoot = new();

        Task[] tasks = images
            .Select(async image =>
            {
                int currentActive = Interlocked.Increment(ref activeCount);

                lock (syncRoot)
                {
                    if (currentActive > maxObservedConcurrencyLevel)
                    {
                        maxObservedConcurrencyLevel = currentActive;
                    }
                }

                try
                {
                    await imageProcessor.ProcessAsync(image, cancellationToken);
                    Interlocked.Increment(ref processedCount);
                }
                finally
                {
                    Interlocked.Decrement(ref activeCount);
                }
            })
            .ToArray();

        await Task.WhenAll(tasks);

        return new ProcessingReport(images.Count, processedCount, maxObservedConcurrencyLevel);
    }
}