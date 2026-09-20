public sealed class MainImageProcessor : IImageProcessor
{
    public async Task ProcessAsync(ImageFile imageFile, CancellationToken cancellationToken)
    {
        int processingDelay = imageFile.ProcessingTimeMs;
        await Task.Delay(processingDelay, cancellationToken);
    }
}