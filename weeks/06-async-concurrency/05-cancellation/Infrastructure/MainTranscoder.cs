public sealed class MainTranscoder : ITranscoder
{
    public async Task TranscodeAsync(MediaFile mediaFile, CancellationToken cancellationToken)
    {
        int transcodeDelay = mediaFile.DurationSeconds * 150;
        await Task.Delay(transcodeDelay, cancellationToken);
    }
}