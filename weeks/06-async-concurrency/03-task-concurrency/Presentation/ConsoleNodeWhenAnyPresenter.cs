public sealed class ConsoleNodeWhenAnyPresenter
{
    private readonly WhenAnyTasksExperiment _experiment;

    public ConsoleNodeWhenAnyPresenter(WhenAnyTasksExperiment experiment)
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