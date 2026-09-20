public sealed class DummyDocumentIndexer : IDocumentIndexer
{
    public async Task IndexAsync(DocumentFile documentFile, CancellationToken cancellationToken)
    {
        int indexingDelay = documentFile.IndexingDelayMs;
        await Task.Delay(indexingDelay, cancellationToken);
    }
}