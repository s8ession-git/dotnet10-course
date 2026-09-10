public sealed class ConsoleNodeWhenAllPresenter
{
    private readonly WhenAllTasksExperiment _experiment;

    public ConsoleNodeWhenAllPresenter(WhenAllTasksExperiment experiment)
    {
        _experiment = experiment
            ?? throw new ArgumentNullException(nameof(experiment));
    }

    public async Task ShowAsync()
    {
        var nodes = new[]
        {
            new Node("Alpha", 300),
            new Node("Beta", 700),
            new Node("Gamma", 500)
        };

        await _experiment.RunAsync(nodes);
    }
}
