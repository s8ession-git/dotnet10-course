public interface IParseDocument
{
    Task<DocumentProcessingResult> ParseAsync(DocumentFile documentFile, CancellationToken cancellationToken);
}