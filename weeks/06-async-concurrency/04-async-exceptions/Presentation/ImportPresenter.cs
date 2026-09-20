public sealed class ImportPresenter
{
    private readonly IScenario scenario;

    public ImportPresenter(IScenario scenario)
    {
        this.scenario = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(IEnumerable<CatalogSource> sources)
    {
        foreach (CatalogSource source in sources)
        {
            try
            {
                CatalogSnapshot snapshot = await scenario.ImportAsync(source);
                Console.WriteLine($"{source.Name} -> {source.DelayMs} ms -> {snapshot.TotalItemsCount} records");
            }
            catch (CatalogImportException exception)
            {
                Console.WriteLine($"{exception.GetType().Name}");
                Console.WriteLine($"InnerException: {exception.InnerException?.GetType().Name}");
            }
            catch (CatalogSourceException exception)
            {
                Console.WriteLine($"Import failed for {source.Name}: {exception.Message}");
            }
        }
    }
}
