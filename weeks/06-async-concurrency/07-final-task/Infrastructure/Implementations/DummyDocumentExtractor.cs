public sealed class DummyDocumentExtractor : IDocumentExtractor
{
    public async Task ExtractAsync(DocumentFile documentFile, CancellationToken cancellationToken)
    {
        int extractionDelay = documentFile.ExtractionDelayMs;
        await Task.Delay(extractionDelay, cancellationToken);
    }
}