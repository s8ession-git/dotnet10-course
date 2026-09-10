using System.Diagnostics;

public sealed class WhenAllTasksExperiment
{
    private readonly INodeService[] _nodeServices;

    public WhenAllTasksExperiment(INodeService[] nodeServices)
    {
        _nodeServices = nodeServices is { Length: > 0 }
            ? nodeServices
            : throw new ArgumentException(
                "At least one node service is required.",
                nameof(nodeServices));
    }

    public async Task<int> RunAsync(Node[] nodes)
    {
        if (nodes is null)
            throw new ArgumentNullException(nameof(nodes));

        if (nodes.Length == 0)
            return 0;

        var stopwatch = Stopwatch.StartNew();
        var latencyTasks = nodes
            .Select((node, index) =>
                _nodeServices[index % _nodeServices.Length]
                    .CheckNodeLatencyAsync(node))
            .ToArray();

        int[] latencies = await Task.WhenAll(latencyTasks);
        stopwatch.Stop();

        foreach (Node node in nodes)
            Console.WriteLine(node);

        Console.WriteLine($"Total DelayMs: {latencies.Sum()} ms");
        Console.WriteLine($"Elapsed with WhenAll: {stopwatch.ElapsedMilliseconds} ms");

        return latencies.Sum();
    }
}