internal static class Program
{
	private static async Task Main()
    {
        var experiment = new WhenAnyTasksExperiment(
            new INodeService[] { new NodeService() });
        var presenter = new ConsoleNodeWhenAnyPresenter(experiment);

        await presenter.ShowAsync();
    }
}