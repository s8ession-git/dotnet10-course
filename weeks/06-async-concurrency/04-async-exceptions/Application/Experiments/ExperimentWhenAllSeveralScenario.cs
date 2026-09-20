using System.Diagnostics;

public sealed class ExperimentWhenAllSeveralScenario
{
    private readonly WhenAllPresenter presenter = new();

    public async Task RunAsync()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        Task<CatalogSnapshot> alphaTask = ImportAsync(new CatalogSource("Alpha", 1200, true, 125));
        Task<CatalogSnapshot> betaTask = ImportAsync(new CatalogSource("Beta", 600, true, 100));
        Task<CatalogSnapshot> gammaTask = ImportAsync(new CatalogSource("Gamma", 900, false, 230));

        Task<CatalogSnapshot[]> allTask = Task.WhenAll(alphaTask, betaTask, gammaTask);

        try
        {
            await allTask;
        }
        catch
        {

            if (allTask.Exception is not null)
            {
                Console.WriteLine($"Aggregate exceptions count: {allTask.Exception.InnerExceptions.Count}");

                for (int i = 0; i < allTask.Exception.InnerExceptions.Count; i++)
                {
                    Exception innerException = allTask.Exception.InnerExceptions[i];
                    Console.WriteLine($"InnerExceptions[{i}]: {innerException.GetType().Name}: {innerException.Message}");
                }
            }
        }
        finally
        {
            presenter.Show(alphaTask, betaTask, gammaTask, allTask, stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();
        }
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