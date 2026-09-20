public interface IDocumentExtractor
{
    Task ExtractAsync(DocumentFile documentFile, CancellationToken cancellationToken);
}