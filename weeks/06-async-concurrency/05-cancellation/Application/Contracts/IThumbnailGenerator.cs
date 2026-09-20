public interface IThumbnailGenerator
{
    Task GenerateThumbnailAsync(MediaFile mediaFile, CancellationToken cancellationToken);
}