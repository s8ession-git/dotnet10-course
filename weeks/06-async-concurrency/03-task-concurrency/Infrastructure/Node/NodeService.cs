public sealed class NodeService : INodeService
{
    public async Task<int> CheckNodeLatencyAsync(Node node)
    {
        await Task.Delay(node.DelayMs);
        return node.DelayMs;
    }
}