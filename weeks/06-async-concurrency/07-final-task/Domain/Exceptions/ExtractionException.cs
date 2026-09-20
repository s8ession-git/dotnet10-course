public sealed class ExtractionException : Exception
{
    public ExtractionException(string message, Exception exception) : base(message, exception)
    {
    }
}