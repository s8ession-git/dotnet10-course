public sealed class MainAnalyzer : IAnalyzer
{
    public async Task AnalyzeAsync(MediaFile mediaFile, CancellationToken cancellationToken)
    {
        int analyzeDelay = mediaFile.DurationSeconds * 70;
        await Task.Delay(analyzeDelay, cancellationToken);
    }
}