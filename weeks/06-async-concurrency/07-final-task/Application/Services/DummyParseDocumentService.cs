public sealed class DummyParseDocumentService : IParseDocument
{
    private readonly IDocumentValidator validator;
    private readonly IDocumentExtractor extractor;
    private readonly IDocumentIndexer indexer;
    private Status status = Status.Initialized;

    public DummyParseDocumentService(IDocumentValidator validator, IDocumentExtractor extractor, IDocumentIndexer indexer)
    {
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        this.extractor = extractor ?? throw new ArgumentNullException(nameof(extractor));
        this.indexer = indexer ?? throw new ArgumentNullException(nameof(indexer));
    }

    public async Task<DocumentProcessingResult> ParseAsync(DocumentFile documentFile, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(documentFile);

        Task validationTask = validator.ValidateAsync(documentFile, cancellationToken);
        Task extractionTask = extractor.ExtractAsync(documentFile, cancellationToken);
        Task indexationTask = indexer.IndexAsync(documentFile, cancellationToken);

        try
        {
            if (documentFile.ShouldFail) throw new ValidationException("Document validation failed.");
            await validationTask;
            await extractionTask;
            await indexationTask;
            status = Status.Completed;
        }
        catch (ValidationException)
        {
            status = Status.Failed;
        }
        catch (OperationCanceledException)
        {
            throw;
        }

        return new DocumentProcessingResult(documentFile.Name, status);
    }
}