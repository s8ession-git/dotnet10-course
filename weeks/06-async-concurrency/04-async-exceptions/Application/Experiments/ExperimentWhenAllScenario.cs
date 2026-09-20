using System.Diagnostics;

public sealed class ExperimentWhenAllScenario
{
    private readonly WhenAllPresenter presenter = new();

    public async Task RunAsync()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        Task<CatalogSnapshot> alphaTask = ImportAsync(new CatalogSource("Alpha", 1200, false, 125));
        Task<CatalogSnapshot> betaTask = ImportAsync(new CatalogSource("Beta", 600, true, 100));
        Task<CatalogSnapshot> gammaTask = ImportAsync(new CatalogSource("Gamma", 900, false, 230));

        Task allTask = Task.WhenAll(alphaTask, betaTask, gammaTask);

        try
        {
            await allTask;
        }
        catch
        {
        }

        stopwatch.Stop();
        presenter.Show(alphaTask, betaTask, gammaTask, allTask, stopwatch.ElapsedMilliseconds);
    }

    private static async Task<CatalogSnapshot> ImportAsync(CatalogSource source)
    {
        await Task.Delay(source.DelayMs);

        if (source.ShouldFail)
        {
            throw new CatalogSourceException("Catalog snapshot failed.");
        }

        return new CatalogSnapshot(source.Name, source.TotalItemsCount);
    }
}
