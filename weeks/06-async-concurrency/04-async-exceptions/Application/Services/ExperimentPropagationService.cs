public sealed class ExperimentPropagationService : IService
{
    public async Task<CatalogSnapshot> ImportAsync(CatalogSource source)
    {
        return await FetchAsync(source);
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