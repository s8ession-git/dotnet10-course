public interface IDocumentIndexer
{
    Task IndexAsync(DocumentFile documentFile, CancellationToken cancellationToken);
}