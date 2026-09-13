public sealed class ImportPresenter
{
    private readonly CatalogImportScenario scenario;

    public ImportPresenter(CatalogImportScenario scenario)
    {
        this.scenario = scenario ?? throw new ArgumentNullException(nameof(scenario));
    }

    public async Task ShowAsync(IEnumerable<CatalogSource> sources)
    {
        foreach (CatalogSource source in sources)
        {
            try
            {
                CatalogSnapshot snapshot = await scenario.RunAsync(source);
                Console.WriteLine($"{source.SourceName} -> {source.DelayMs} ms -> {snapshot.ItemsCount} records");
            }
            catch (CatalogSourceException exception)
            {
                Console.WriteLine($"Import failed for {source.SourceName}: {exception.Message}");
            }
        }
    }
}