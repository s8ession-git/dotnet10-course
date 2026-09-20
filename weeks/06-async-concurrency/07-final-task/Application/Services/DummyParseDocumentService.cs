public sealed class DummyParseDocumentService : IParseDocument
{
    private readonly IDocumentValidator validator;
    private readonly IDocumentExtractor extractor;
    private readonly IDocumentIndexer indexer;

    public DummyParseDocumentService(IDocumentValidator validator, IDocumentExtractor extractor, IDocumentIndexer indexer)
    {
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        this.extractor = extractor ?? throw new ArgumentNullException(nameof(extractor));
        this.indexer = indexer ?? throw new ArgumentNullException(nameof(indexer));
    }

    public async Task<DocumentProcessingResult> ParseAsync(DocumentFile documentFile, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(documentFile);

        try
        {
            if (documentFile.ShouldFail) throw new ValidationException("Document validation failed.");
            
        
            await validator.ValidateAsync(documentFile, cancellationToken);
            await extractor.ExtractAsync(documentFile, cancellationToken);
            await indexer.IndexAsync(documentFile, cancellationToken);

            return new DocumentProcessingResult(documentFile.Name, Status.Completed);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            return new DocumentProcessingResult(documentFile.Name, Status.Failed, exception.Message);
        }
    }
}