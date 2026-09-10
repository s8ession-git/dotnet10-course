public record Node
{
    public string NodeName { get; }
    public int DelayMs { get; }

    public Node(string nodeName, int delayMs)
    {
        if (string.IsNullOrWhiteSpace(nodeName))
            throw new ArgumentException("Node name cannot be null or whitespace.", nameof(nodeName));

        if (delayMs < 0)
            throw new ArgumentOutOfRangeException(nameof(delayMs), "Delay cannot be negative.");

        NodeName = nodeName;
        DelayMs = delayMs;
    }

    public override string ToString()
    {
        return $"Node {NodeName} -> {DelayMs} ms";
    }
}