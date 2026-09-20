public interface ITranscoder
{
    Task TranscodeAsync(MediaFile mediaFile, CancellationToken cancellationToken);
}