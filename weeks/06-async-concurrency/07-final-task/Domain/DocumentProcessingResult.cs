public enum Status { Initialized, Completed, Failed }

public sealed record DocumentProcessingResult
{
    public string DocumentName { get; init; }
    public Status Status { get; init; }
    public string? ErrorMessage { get; init; }

    public DocumentProcessingResult(string documentName, Status status, string? errorMessage = null)
    {   
        ValidateInitialization(documentName);
        DocumentName = documentName;
        Status = status;
        ErrorMessage = errorMessage;
    }   

    private static void ValidateInitialization(string documentName)
    {
        if (string.IsNullOrWhiteSpace(documentName))
        {
            throw new ArgumentException("Document name cannot be null or whitespace.", nameof(documentName));
        }
    }
}