public interface IAnalyzer
{
    Task AnalyzeAsync(MediaFile mediaFile, CancellationToken cancellationToken);
}