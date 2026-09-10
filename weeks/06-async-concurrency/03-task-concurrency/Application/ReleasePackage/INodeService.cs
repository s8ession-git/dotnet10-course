public interface INodeService
{
    Task<int> CheckNodeLatencyAsync(Node node);
}