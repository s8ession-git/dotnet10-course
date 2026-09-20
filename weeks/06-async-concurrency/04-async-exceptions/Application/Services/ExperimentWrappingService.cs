public sealed class ExperimentWrappingService : IService
{
    public async Task<CatalogSnapshot> ImportAsync(CatalogSource source)
    {
        try
        {
            return await FetchAsync(source);
        }
        catch (CatalogSourceException exception)
        {
            throw new CatalogImportException("Failed to import catalog", exception);
        }
    }

    private async Task<CatalogSnapshot> FetchAsync(CatalogSource source)
    {
        await Task.Delay(source.DelayMs);

        if (source.ShouldFail)
        {
            throw new CatalogSourceException("Catalog snapshot failed.");
        }

        return new CatalogSnapshot(source.Name, source.TotalItemsCount);
    }
}