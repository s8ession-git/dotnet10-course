public sealed class CatalogImportException : Exception
{
    public CatalogImportException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}