public sealed class CatalogImportScenario : ICatalogSourceClient
{
    public async Task<CatalogSnapshot> FetchAsync(CatalogSource source)
    {
        await Task.Delay(source.DelayMs);

        if (source.ShouldFail)
        {
            throw new CatalogSourceException("Catalog snapshot failed.");
        }

        return new CatalogSnapshot(source.Name, source.TotalItemsCount);
    }
}
