public sealed class WhenAnyTasksExperiment
{
	private readonly INodeService[] _nodeServices;

	public WhenAnyTasksExperiment(INodeService[] nodeServices)
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

		var stopwatch = System.Diagnostics.Stopwatch.StartNew();
		Task<int>[] latencyTasks = nodes
			.Select((node, index) =>
				_nodeServices[index % _nodeServices.Length]
					.CheckNodeLatencyAsync(node))
			.ToArray();

		Task<int> completedTask = await Task.WhenAny(latencyTasks);
		int completedIndex = Array.IndexOf(latencyTasks, completedTask);
		int firstLatency = await completedTask;

		stopwatch.Stop();

		foreach (Node node in nodes)
			Console.WriteLine(node);

		Console.WriteLine(
			$"First completed: {nodes[completedIndex].NodeName} " +
			$"({firstLatency} ms)");
		Console.WriteLine(
			$"Elapsed with WhenAny: {stopwatch.ElapsedMilliseconds} ms");

		return firstLatency;
	}
}