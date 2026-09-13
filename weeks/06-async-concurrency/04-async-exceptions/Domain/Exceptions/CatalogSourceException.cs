public sealed class CatalogSourceException : Exception
{
    public CatalogSourceException(string message)
        : base(message)
    {
    }

    public CatalogSourceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}