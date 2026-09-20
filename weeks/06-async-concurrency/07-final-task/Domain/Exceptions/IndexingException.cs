public sealed class IndexingException : Exception
{
    public IndexingException(string message, Exception exception) : base(message, exception)
    {
    }
}