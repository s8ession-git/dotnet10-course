public interface IDocumentValidator
{
    Task ValidateAsync(DocumentFile documentFile, CancellationToken cancellationToken);
}