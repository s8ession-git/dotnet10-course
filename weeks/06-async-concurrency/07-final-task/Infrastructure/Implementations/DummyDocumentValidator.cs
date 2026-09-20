public sealed class DummyDocumentValidator : IDocumentValidator
{
    public async Task ValidateAsync(DocumentFile documentFile, CancellationToken cancellationToken)
    {
        int validationDelay = documentFile.ValidationDelayMs;
        await Task.Delay(validationDelay, cancellationToken);
    }
}