public sealed class MainThumbnailGenerator : IThumbnailGenerator
{
    public async Task GenerateThumbnailAsync(MediaFile mediaFile, CancellationToken cancellationToken)
    {
        int thumbnailDelay = mediaFile.DurationSeconds * 60;
        await Task.Delay(thumbnailDelay, cancellationToken);
    }
}